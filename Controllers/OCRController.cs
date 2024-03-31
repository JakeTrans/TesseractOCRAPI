using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using System.Runtime.Intrinsics.X86;
using System.Text;
using TesseractOCRPlugin;

namespace TesseractOCRAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OCRController : ControllerBase
    {
        private readonly ILogger<OCRController> _logger;

        public OCRController(ILogger<OCRController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOCRResults")]
        public IEnumerable<OCResults> Get(string text)
        {
            return Enumerable.Range(1, 1).Select(index => new OCResults
            {
                Text = text
            }).ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
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