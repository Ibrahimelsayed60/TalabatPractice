using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {

        // _dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType)

        // Signature for property for where condition [ Where(P => P.Id == id) ]
        public Expression<Func<T,bool>> Criteria { get; set; }

        // Signature for Property for List of Includes 
        public List<Expression<Func<T,object>>> Includes { get; set; }

        // Signature for Property [OrderBy(P => P.name)]
        public Expression<Func<T, object>> OrderBy { get; set; }

        //Signature for property [OrderByDesc(P => P.name)]
        public Expression<Func<T,object>> OrderByDescending { get; set; }

        public int SKip { get; set; }

        public int Take { get; set; }

        public bool IsPaginationEnabled { get; set; }

    }
}
