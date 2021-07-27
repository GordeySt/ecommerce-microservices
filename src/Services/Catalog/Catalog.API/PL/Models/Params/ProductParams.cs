using Catalog.API.BL.Enums;
using Services.Common.Models;

namespace Catalog.API.PL.Models.Params
{
    public class ProductsParams : PagingParams
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int MinimumAge { get; set; }
        public PriceOrderType? PriceOrderType { get; set; }
        public RatingOrderType? RatingOrderType { get; set; }
    }
}
