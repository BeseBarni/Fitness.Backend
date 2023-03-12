
using Fitness.Backend.Application.DataContracts.Models.Entity.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Extensions
{
    public static class EntityFrameworkExtensions
    {


        public static IQueryable<T> DelFilter<T>(this IQueryable<T> query) where T : IDeleteable
        {
            return query.Where(p => p.Del == 0);
        }

    }
}
