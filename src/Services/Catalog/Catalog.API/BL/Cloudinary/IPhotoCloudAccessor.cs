using Catalog.API.BL.Services;
using Catalog.API.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface IPhotoCloudAccessor
    {
        Task<PhotoUploadResult> AddPhotoToCloudAsync(IFormFile file);
        Task<string> DeletePhotoFromCloudAsync(string publicId);
    }
}
