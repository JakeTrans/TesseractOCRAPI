using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
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
                //https://stackoverflow.com/questions/70272542/nuget-system-drawing-common-net-6-ca1416-this-call-site-is-reachable-on-all-pla
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