using Microsoft.AspNetCore.Http;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface IPhotoService
    {
        Task<ServiceResult> AddPhotoAsync(IFormFile mainImage, Guid productId);
    }
}
