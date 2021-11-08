using Domain.Core.ModelFilter;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public class ExpressionEvaluator<T> where T : class
    {
        /// <summary>
        /// Ordena dinamicamente de acuerdo al IQueryable y valores recibidos
        /// </summary>
        /// <param name="inputQuery"></param>
        /// <param name="oBaseFilter">columnOrderBy: columna a ordenar, desc: true o false</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy(IQueryable<T> inputQuery, BaseFilter oBaseFilter)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var propertyInfo = typeof(T).GetProperty(oBaseFilter.columnOrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            // this is the part p.SortColumn
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);

            // this is the part p =&gt; p.SortColumn
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string command = oBaseFilter.desc ? "OrderByDescending" : "OrderBy";

            // finally, call the "OrderBy" / "OrderByDescending" method with the order by
            // lamba expression
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { typeof(T), propertyInfo.PropertyType },
                     inputQuery.Expression, Expression.Quote(orderByExpression));

            return inputQuery.Provider.CreateQuery<T>(resultExpression);
        }
        /// <summary>
        /// Realiza la paginación dinamicamente de acuerdo al IQueryable y valores recibidos
        /// </summary>
        /// <param name="inputQuery"></param>
        /// <param name="oBaseFilter">skip: número de página, take: número de registros que debe tomar</param>
        /// <returns></returns>
        public static IQueryable<T> Paging(IQueryable<T> inputQuery, BaseFilter oBaseFilter)
        {
            // Apply paging if enabled
            if (oBaseFilter.take > 0)
            {
                inputQuery = inputQuery.Skip(oBaseFilter.skip * oBaseFilter.take)
                                         .Take(oBaseFilter.take);
            }

            return inputQuery;
        }
    }
}
