using Catalog.API.BL.Services.CloudinaryService;
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
