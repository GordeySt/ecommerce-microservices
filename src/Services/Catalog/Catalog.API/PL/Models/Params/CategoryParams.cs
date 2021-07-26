using Services.Common.Models;

namespace Catalog.API.PL.Models.Params
{
    public class ProductsParams : PagingParams
    {
        public string ProductName { get; set; }
    }
}
