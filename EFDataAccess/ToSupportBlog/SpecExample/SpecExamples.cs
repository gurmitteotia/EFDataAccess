using System;
using System.Linq.Expressions;
using EFDataAccess.Model;
using EFDataAccess.Spec;

namespace EFDataAccess.ToSupportBlog.SpecExample
{
    public class PopularProduct : Specification<Product>
    {
        protected override Expression<Func<Product, bool>> ProvideExpression()
        {
            return p => p.AverageRatings >= 4;
        }
    }

    public class BrandSpecification : Specification<Product>
    {
        private readonly string _brandName;
        public BrandSpecification(string brandName)
        {
            _brandName = brandName;
        }

        protected override Expression<Func<Product, bool>> ProvideExpression()
        {
            return p => p.Brand.Name.Equals(_brandName);
        }
    }
}