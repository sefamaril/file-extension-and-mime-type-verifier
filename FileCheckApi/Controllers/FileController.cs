using FileExtensionandMimeTypeVerifier.File;
using Microsoft.AspNetCore.Mvc;

namespace FileExtensionandMimeTypeVerifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("CheckMimeType")]
        public IActionResult CheckMimeType(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or not specified");

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileByte = memoryStream.ToArray();
                return CheckFile.CheckMimeType(fileByte, file.FileName) != "application/octet-stream" ? Ok() : BadRequest("File mime type not supported");
            }
        }

        [HttpPost("CheckExtension")]
        public IActionResult CheckExtension(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or not specified");

            var extension = Path.GetExtension(file.FileName).TrimStart('.');
            return CheckFile.CheckExtension(extension) == true ? Ok() : BadRequest("File extension not supported");
        }
    }
}
