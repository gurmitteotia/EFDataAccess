using System.Collections.Generic;
using EFDataAccess.Model;

namespace EFDataAccess.ToSupportBlog.AppService
{
    public interface IProductService
    {
        IEnumerable<Product> GetPopularKeyboards();
    }
}