using Fitness.Backend.Application.DataContracts.Models.Entity.Interfaces;

namespace Fitness.Backend.Domain.Extensions
{
    public static class EntityFrameworkExtensions
    {

        /// <summary>
        /// Gets all the objects of a given entity in the database where the Del flag is set to 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> DelFilter<T>(this IQueryable<T> query) where T : IDeleteable
        {
            return query.Where(p => p.Del == 0);
        }

    }
}
