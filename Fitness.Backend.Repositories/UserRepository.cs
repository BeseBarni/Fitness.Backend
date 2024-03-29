﻿using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Domain.DbContexts;
using Fitness.Backend.Domain.Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventSource;

namespace Fitness.Backend.Repositories
{
    /// <summary>
    /// Repository for accessing, updating and deleting user related data in the database
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task Add(User parameters)
        {

            context.Add(parameters);

            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var user = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (user == null)
                throw new ResourceNotFoundException($"user:{id}");

            user.Del = 1;
            if (user.Image != null)
                user.Image.Del = 1;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll(User? parameters)
        {
            return await context.Clients.DelFilter().ToListAsync();
        }

        public async Task<User> GetOne(string id)
        {
            var result = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (result == null)
                throw new ResourceNotFoundException($"user:{id}");

            return result;

        }

        public async Task Update(User parameters)
        {
            var user = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.Id);
            if (user == null)
                throw new ResourceNotFoundException($"user:{parameters.Id}");

            user.Name = parameters.Name ?? user.Name;
            user.ImageId = parameters.ImageId ?? user.ImageId;

            await context.SaveChangesAsync();
        }

        public async Task AddLesson(string lessonId, string userId)
        {
            var result = await context.Lessons.DelFilter().FirstOrDefaultAsync(p => p.Id == lessonId);
            if (result == null)
                throw new ResourceNotFoundException($"lesson:{lessonId}");

            var user = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == userId);

            if (user == null)
                throw new ResourceNotFoundException($"user:{userId}");

            user.Lessons = user.Lessons ?? new List<Lesson>();
            user.Lessons.Add(result);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lesson>> GetLessons(string userId)
        {
            var result = await context.Clients.Include(u => u.Lessons).DelFilter().FirstOrDefaultAsync(p => p.Id == userId);

            if(result == null) throw new ResourceNotFoundException($"user:{userId}");

            result.Lessons = result.Lessons ?? new List<Lesson>(); 
            return result.Lessons.Where(p => p.Del == 0);
        }

        public async Task AddImage(string userId, IFormFile image)
        {
            var result = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == userId);
            if (result == null) throw new ResourceNotFoundException($"user:{userId}");

            var img = new Image
            {
                Name = string.Format("{0}_{1}.{2}", userId, image.Name, image.ContentType.Split('/')[1]),
                ContentType= image.ContentType
            };
            
            MemoryStream memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            img.ImageData = memoryStream.ToArray();
            result.Image = img;
            await context.SaveChangesAsync();
        }

        public async Task<Image> GetImage(string userId)
        {
            var result = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == userId);
            if (result == null) throw new ResourceNotFoundException($"user:{userId}");

            var imgs = await context.ProfilePictures.ToListAsync();

            if(result.Image == null) throw new ResourceNotFoundException($"user:{userId}_image");
            
            return result.Image;

        }
    }
}
