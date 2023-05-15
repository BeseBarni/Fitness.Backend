using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend.Domain.DbContexts;
using Fitness.Backend.Domain.Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Backend.Repositories
{
    /// <summary>
    /// Repository for accessing, updating and deleting sport related data in the database
    /// </summary>
    public class SportRepository : BaseRepository, ISportRepository
    {
        public SportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task Add(Sport parameters)
        {
            if (context.Sports.DelFilter().Where(p => p.Name == parameters.Name).Count() > 0)
                throw new ResourceAlreadyExistsException(string.Format("sport:{0}",parameters.Id));

            context.Add(parameters);

            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var sport = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if (sport == null)
                throw new ResourceNotFoundException(string.Format("sport:{0}", id));

            sport.Del = 1;
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Sport>?> GetAll(Sport? parameters)
        {
            return await context.Sports.DelFilter().ToListAsync();

        }

        public async Task<Sport> GetOne(string id)
        {
            var result = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == id);
            if(result == null)
                throw new ResourceNotFoundException(string.Format("sport:{0}", id));

            return result;
        }

        public async Task Update(Sport parameters)
        {
            var sport = context.Sports.DelFilter().FirstOrDefault(p => p.Id == parameters.Id);

            if (sport == null)
                throw new ResourceNotFoundException(string.Format("sport:{0}", parameters.Id));

            sport.Name = parameters.Name;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instructor>?> GetInstructors(string sportId)
        {
            var sport = await context.Sports.Include(s => s.Instructors).DelFilter().FirstOrDefaultAsync(p => p.Id == sportId);
            if (sport == null) throw new ResourceNotFoundException(string.Format("sport:{0}", sportId));

            return sport.Instructors; 

        }

        public async Task AddImage(string sportId, IFormFile image)
        {
            var result = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == sportId);
            if (result == null) throw new ResourceNotFoundException($"sportId:{sportId}");

            var img = new Image
            {
                Name = string.Format("{0}_sport_{1}.{2}", sportId, image.Name, image.ContentType.Split('/')[1]),
                ContentType = image.ContentType
            };

            MemoryStream memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            img.ImageData = memoryStream.ToArray();
            result.Image = img;
            await context.SaveChangesAsync();
        }

        public async Task<Image> GetImage(string sportId)
        {
            var result = await context.Sports.DelFilter().FirstOrDefaultAsync(p => p.Id == sportId);
            if (result == null) throw new ResourceNotFoundException($"sportId:{sportId}");

            var imgs = await context.ProfilePictures.ToListAsync();

            if (result.Image == null) throw new ResourceNotFoundException($"sportId:{sportId}_image");

            return result.Image;

        }
    }
}
