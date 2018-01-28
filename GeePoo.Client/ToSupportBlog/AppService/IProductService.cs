using System.Collections.Generic;
using GeePoo.Client.Model;

namespace GeePoo.Client.ToSupportBlog.AppService
{
    public interface IProductService
    {
        IEnumerable<Product> GetPopularKeyboards();
    }
}