using System;
using GeePoo.Client.EFDbContext;
using GeePoo.Client.Model;
using NUnit.Framework;

namespace GeePo.Tests.RepositoryUsage
{
    [TestFixture]
    public class RepositoryUsage
    {
        private IRepository _repository;

        [SetUp]
        public void Setup()
        {
            var scalableDbContext = new ScalableDbContext("EFDataAccess.dll", "EFDataAccess");
            var dataContext = new DataContext(scalableDbContext);
            _repository = new GenericRepository(dataContext);
        }

        [Test][Ignore("ignored")]
        public void AddDataToRepository()
        {
            var dell = new Brand() {Name = BrandName.Dell};

            var samsung = new Brand() { Name = BrandName.Samsung };

            var product = new Product() { Name = "Mini keyboard", Category = ProductCategory.KeyBoard, AverageRatings = 5, Price = 30, Brand = samsung, CreatedOn = DateTime.Now };
            _repository.Add(product);
            product = new Product() { Name = "keyboard", Category = ProductCategory.KeyBoard, AverageRatings = 5, Price = 50, Brand = samsung, CreatedOn = DateTime.UtcNow };
            _repository.Add(product);

            product = new Product() { Name = "Mini keyboard", Category = ProductCategory.KeyBoard, AverageRatings = 5, Price = 31, Brand = dell, CreatedOn = DateTime.UtcNow };
            _repository.Add(product);
            product = new Product() { Name = "keyboard", Category = ProductCategory.KeyBoard, AverageRatings = 5, Price = 60, Brand = dell, CreatedOn = DateTime.UtcNow };
            _repository.Add(product);
            product = new Product() { Name = "Good mouse", Category = ProductCategory.Mouse, AverageRatings = 5, Price = 10, Brand = samsung, CreatedOn = DateTime.UtcNow };
            _repository.Add(product);

            _repository.Save();
        }

        [Test]
        public void ScratchPadTest()
        {
            //repository.Get(keyboard, o=>o.InAscOrderOf(p=>p.AverageRatings));

            //var keyboard = new ProductCategorySpecification(ProductCategory.KeyBoard);

            //1. Following example is achieving the same result as above one but using DynamicSpecification.

            //var keyboard = new DynamicSpecification<Product>("Category", OperationType.EqualTo, "Keyboard");
            //var popular = new DynamicSpecification<Product>("AverageRatings",OperationType.GreaterThanEqualTo,  "4.5");

            //var popularKeyboard = popular.And(keyboard);
            //var popularKeyboards = repository.Get(popularKeyboard);

            //2. Order following example we're combinding DynamicSpecification with GenericSpecification.

            //var keyboard = new GenericSpecification<Product>(p=>p.Category==ProductCategory.KeyBoard);
            //var popular = new DynamicSpecification<Product>("AverageRatings", OperationType.GreaterThanEqualTo, "4.5");

            //var popularKeyboard = popular.And(keyboard);
            //var popularKeyboards = repository.Get(popularKeyboard);


            //var popular = new PopularProductSpecification();
            //var keyboard = new CategorySpecification(ProductCategory.KeyBoard);
            //var popularKeyboard = popular.And(keyboard);
            
            //var popularKeyboards = _repository.Get(popularKeyboard);

            string userSuppliedProperty = "AverageRatings";
            OperationType userSuppliedOperationType = OperationType.GreaterThan;
            var userSuppliedValue = 4.5;

            var userFilter = new DynamicSpecification<Product>(userSuppliedProperty, userSuppliedOperationType, userSuppliedValue);
            var filteredProducts = _repository.Get(userFilter);

            //and dynamic ordering
            string userSuppliedOrderingProperty = "Category";
            OrderType userSuppliedOrderType = OrderType.Ascending;
            var sortedFilteredProduct = _repository.Get(userFilter, o => o.InOrderOf(userSuppliedOrderingProperty, userSuppliedOrderType));
        }
         
    }
}