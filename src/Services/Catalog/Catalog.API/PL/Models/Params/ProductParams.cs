using Catalog.API.BL.Enums;
using Catalog.API.DAL.Enums;
using Services.Common.Models;

namespace Catalog.API.PL.Models.Params
{
    public class ProductsParams : PagingParams
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public AgeRating MinimumAge { get; set; }
        public PriceOrderType? PriceOrderType { get; set; }
        public RatingOrderType? RatingOrderType { get; set; }
    }
}
