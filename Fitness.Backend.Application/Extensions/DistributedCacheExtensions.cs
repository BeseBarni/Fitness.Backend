using Fitness.Backend.Application.DataContracts.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.Extensions
{
    public static class DistributedCacheExtensions
    {

        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;
            try
            {
                var jsonData = JsonSerializer.Serialize(data);
                await cache.SetStringAsync(recordId, jsonData, options);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task<IEnumerable<T>?> GetListAsync<T>(this IDistributedCache cache, string recordId, Func<T?,Task<IEnumerable<T>?>> action, T? p)
        {

            string? jsonData;
            try
            {
                jsonData = await cache.GetStringAsync(recordId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during cache retrieval");
                Console.WriteLine(e.Message);
                return await action.Invoke(p);
            }

            if (jsonData is null)
            {
                var result = await action.Invoke(p);
                await cache.SetRecordAsync(recordId, result);
                return result;
            }

            Console.WriteLine("Getting data from cache");

            return JsonSerializer.Deserialize<IEnumerable<T>>(jsonData);
        }

        public static async Task<IEnumerable<T>> GetListAsync<T>(this IDistributedCache cache, string recordId, Func<string, Task<IEnumerable<T>>> action, string p)
        {
            string? jsonData;
            try
            {
                jsonData = await cache.GetStringAsync(recordId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during cache retrieval");
                Console.WriteLine(e.Message);
                return await action.Invoke(p);
            }


            if (jsonData is null)
            {
                var result = await action.Invoke(p);
                await cache.SetRecordAsync(recordId, result);
                return result;
            }

            Console.WriteLine("Getting data from cache");

            return JsonSerializer.Deserialize<IEnumerable<T>>(jsonData);

        }

        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string recordId, Func<string, Task<T>> action, string id)
        {
            string? jsonData;
            try
            {
                jsonData = await cache.GetStringAsync(recordId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during cache retrieval");
                Console.WriteLine(e.Message);
                return await action.Invoke(id);
            }

            if (jsonData is null)
            {
                var result = await action.Invoke(id);
                await cache.SetRecordAsync(recordId, result);
                return result;
            }


            Console.WriteLine("Getting data from cache");

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
