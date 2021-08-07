using Catalog.API.BL.Services.CloudinaryService;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Catalog.UnitTests.Shared.Services
{
    public static class PhotoServiceTestData
    {
        public static PhotoUploadResult CreatePhotoUploadResult() => new
            (PublicId: Guid.NewGuid().ToString(), Url: Guid.NewGuid().ToString());

        public static FormFile CreateFakeFormFile()
        {
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            return new(stream, 0, stream.Length, "id_from_form", fileName);
        }
      
    }
}
