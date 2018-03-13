using System.Collections.Generic;
using GenRepo.Client.Model;

namespace GenRepo.Client.ToSupportBlog.AppService
{
    public interface IProductService
    {
        IEnumerable<Product> GetPopularKeyboards();
    }
}