using Services.Common.Models;

namespace Basket.Tests.Shared.Services
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
