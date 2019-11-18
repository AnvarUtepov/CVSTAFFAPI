using CVCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CVCore.Extensions
{
    public static class QueryableExtensions
    {
         public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, TableMetaData queryObj, string defaultSortField, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj.sortField) || !columnsMap.ContainsKey(queryObj.sortField))
                return query.OrderByDescending(columnsMap[defaultSortField]);

            if (queryObj.sortOrder == 0)
                return query.OrderBy(columnsMap[queryObj.sortField]);
            else
                return query.OrderByDescending(columnsMap[queryObj.sortField]);
        }
        public static async Task<IEnumerable<T>> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            return await Task.WhenAll(tasks);
        }
        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, string queryObj, Dictionary<string, Expression<Func<T, bool>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj) || !columnsMap.ContainsKey(queryObj))
                return query;
            return query.Where(columnsMap[queryObj]);
        }
    }
}
