using Services.Common.Models;

namespace Basket.UnitTests.Shared.Services
{
    public static class CommonTestData
    {
        public static PagingParams CreatePagingParams() => new()
        {
            PageNumber = 1,
            PageSize = 2
        };
    }
}
