using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery (IQueryable<T> inputQuery, ISpecifications<T> Spec)
        {

            var Query = inputQuery;

            if(Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }

            Query = Spec.Includes.Aggregate(Query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}
