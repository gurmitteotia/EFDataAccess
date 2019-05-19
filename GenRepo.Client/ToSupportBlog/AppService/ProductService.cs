using System.Collections.Generic;
using GenRepo.Client.Model;

namespace GenRepo.Client.ToSupportBlog.AppService
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
            var keyboard = Filter<Product>.Create(product=>product.Category==ProductCategory.KeyBoard);
            var popular =  Filter<Product>.Create(p=>p.AverageRatings>4);
            var popularKeyboard = popular.And(keyboard);
            return _repository.Get(popularKeyboard);
        }
    }
}