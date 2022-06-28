using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Brand); 

        }

        public ProductsWithTypesAndBrandsSpecification(int id) 
            : base(x=> x.ProductTypeId == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Brand); 
        }                                    
    }
}