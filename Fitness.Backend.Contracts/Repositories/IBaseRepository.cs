namespace Fitness.Backend.Application.Contracts.Repositories
{
    /// <summary>
    /// Base repository interface for implementing CRUD functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    {
        public abstract Task<IEnumerable<T>?> GetAll(T? parameters);
        public abstract Task Delete(string id);
        public abstract Task Update(T parameters);
        public abstract Task Add(T parameters);
        public abstract Task<T> GetOne(string id);
    }
}
