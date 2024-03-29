using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    //contstrained for use with actual entity classes
    public class SpecificationEvaluator<TEntitiy> where TEntitiy : BaseEntity
    {
        public static IQueryable<TEntitiy> GetQuery(IQueryable<TEntitiy> inputQuery,
        ISpecification<TEntitiy> spec)
        {
            var query = inputQuery; 

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

             if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.isPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query; 
        }

        
    }
}