using Doctorla.Core.InternalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Doctorla.Dto;

namespace Doctorla.Business.Helpers
{
    /// <summary>
    /// Static helper methods for paged results.
    /// </summary>
    public static class PagingExtensions
    {
        private static AppSettings _appSettings;

        /// <summary>
        /// Since this class is static, it needs method based DI. Do not call this anywhere other than Startup.cs
        /// </summary>
        public static void InjectAppSettings(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Applies given paging properties to the result set.
        /// </summary>
        /// <typeparam name="T">Type of the object that will be returned</typeparam>
        /// <param name="queryable">Data source</param>
        /// <param name="pageNumber">The index of the page to be returned, starting from 0</param>
        public static async Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber = 0)
        {
            // TODO reactivate pagination
            //var rowsToSkip = pageNumber * _appSettings.PageSize;
            //queryable = queryable.Skip(rowsToSkip).Take(_appSettings.PageSize).AsQueryable();
            return await queryable.ToListAsync();
        }

        /// <summary>
        /// Returns the paging output, given the data source and paging options
        /// </summary>
        /// <typeparam name="TEntity">Type of the resulting entity</typeparam>
        /// <param name="query">Data source</param>
        /// <returns>A PagingOutput object, containing information about results</returns>
        public static async Task<PagingOutput> ToPagingProperties<TEntity>(this IQueryable<TEntity> query, int pageNumber = 0)
        {
            return null;
            // TODO reactivate pagination
            var count = await query.CountAsync();
            var totalPages = (count / _appSettings.PageSize) + 1;

            return new PagingOutput
            {
                RecordCount = count,
                PageCount = totalPages,
                PageIndex = pageNumber
            };
        }
    }
}
