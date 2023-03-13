using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Application.DataContracts.Extensions;
using Fitness.Backend.Application.DataContracts.Models.Entity;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task Add(User parameters)
        {
            if (context.Clients.DelFilter().Where(p => p.Name == parameters.Name).Count() > 0)
                throw new Exception();

            context.Add(parameters);

            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var user = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (user == null)
                throw new ResourceNotFoundException();

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
                throw new ResourceNotFoundException();

            return result;

        }

        public async Task Update(User parameters)
        {
            var user = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == parameters.Id);
            if (user == null)
                throw new ResourceNotFoundException();

            user.Name = parameters.Name ?? user.Name;
            user.ImageId = parameters.ImageId ?? user.ImageId;

            await context.SaveChangesAsync();
        }

        public async Task AddLesson(string lessonId, string userId)
        {
            var result = await context.Lessons.DelFilter().FirstOrDefaultAsync(p => p.Id == lessonId);
            if (result == null)
                throw new ResourceNotFoundException();

            var user = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == userId);

            if (user == null)
                throw new ResourceNotFoundException();

            user.Lessons = user.Lessons ?? new List<Lesson>();
            user.Lessons.Add(result);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lesson>> GetLessons(string userId)
        {
            var result = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == userId);

            if(result == null) throw new ResourceNotFoundException();

            context.Entry(result)
                .Collection(b => b.Lessons)
                .Load();

            result.Lessons = result.Lessons ?? new List<Lesson>(); 
            return result.Lessons.Where(p => p.Del == 0);
        }

        public async Task AddImage(string userId, IFormFile image)
        {
            var result = await context.Clients.DelFilter().FirstOrDefaultAsync(p => p.Id == userId);
            if (result == null) throw new ResourceNotFoundException();

            var img = new Image
            {
                Name = string.Format("{0}_{1}",userId,image.Name),
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
            if (result == null) throw new ResourceNotFoundException();

            var imgs = await context.ProfilePictures.ToListAsync();

            if(result.Image == null) throw new ResourceNotFoundException();

            return result.Image;

        }
    }
}
