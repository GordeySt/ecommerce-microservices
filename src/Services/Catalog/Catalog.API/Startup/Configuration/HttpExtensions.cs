using Catalog.API.PL.Constants;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Catalog.API.Startup.Configuration
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage,
            int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages
            };

            response.Headers.Add(HeaderConstants.PaginationHeader, JsonSerializer.Serialize(paginationHeader));
        }
    }
}
