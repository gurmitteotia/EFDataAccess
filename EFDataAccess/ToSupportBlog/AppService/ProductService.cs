using System.Collections.Generic;
using EFDataAccess.Model;
using EFDataAccess.Spec;
using EFDataAccess.ToSupportBlog.SpecRepository;

namespace EFDataAccess.ToSupportBlog.AppService
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        
        public ProductService(IRepository repository)
        {
            _repository = repository;
            
        }

        public IEnumerable<Product> GetPopularKeyboards()
        {
            var keyboard = new GenericSpecification<Product>(product=>product.Category==ProductCategory.KeyBoard);
            var popular = new GenericSpecification<Product>(p=>p.AverageRatings>4);
            var popularKeyboard = popular.And(keyboard);

            return _repository.Get(popularKeyboard);
        }
    }
}