using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        //Where criteria for generic queries
        Expression<Func<T, bool>> Criteria {get; }

        //code to handle includes function
        List<Expression<Func<T, object>>> Includes {get; }
        
    }
}