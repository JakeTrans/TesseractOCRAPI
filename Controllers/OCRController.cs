using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using System.Runtime.Intrinsics.X86;
using System.Text;
using TesseractOCRPlugin;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net;
using System.Web.Http;
using TesseractOCRAPI.APIKeyFunctions;

namespace TesseractOCRAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class OCRController : ControllerBase
    {
        private readonly ILogger<OCRController> _logger;

        private readonly IApiKeyValidation _apiKeyValidation;

        public OCRController(ILogger<OCRController> logger, IApiKeyValidation apiKeyValidation)
        {
            _logger = logger;
            _apiKeyValidation = apiKeyValidation;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string apiKey)
        {
            bool isValid = _apiKeyValidation.IsValidApiKey(apiKey);
            if (!isValid)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            string text = "";
            TesseractOCR TessOCR = new TesseractOCR("eng", TesseractOCR.Quality.High);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                //Reset Stream
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (Image img = Image.Load(memoryStream))
                {
                    // TODO: ResizeImage(img, 100, 100);
                    text = TessOCR.OCRimage(img);
                }
            }

            return Ok(text);
        }
    }
}