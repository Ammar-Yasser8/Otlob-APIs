using Otlob.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otlob.Core.Specification.ProductSpecifications
{
    public class ProductWithBrandandCategory : BaseSpecification<Product>
    {
        public ProductWithBrandandCategory(string sort) :base()
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p=> p.ProductCategory);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
                AddOrderBy(p => p.Name);
        }
        public ProductWithBrandandCategory(int id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductCategory);
        }

    }
}
