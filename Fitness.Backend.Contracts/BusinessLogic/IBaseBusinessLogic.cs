using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Contracts.BusinessLogic
{
    public interface IBaseBusinessLogic<T>
    {
        public abstract Task<IEnumerable<T>?> GetAll(T? parameters);
        public abstract Task Delete(string id);
        public abstract Task Update(T parameters);
        public abstract Task Add(T parameters);
        public abstract Task<T> GetOne(string id);
    }
}
