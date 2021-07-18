using Services.Common.Models;

namespace Catalog.API.PL.Models.Params
{
    public class CategoryParams : PagingParams
    {
        public string CategoryName { get; set; }
    }
}
