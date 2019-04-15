using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Services;
using System.Threading.Tasks;

namespace NPlaylist.Controllers
{
    public class AudioController : Controller
    {
        private readonly IAudioUploadService audioUploadService;

        public AudioController(IAudioUploadService audioUploadService)
        {
            this.audioUploadService = audioUploadService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            await audioUploadService.Upload(uploadedFile);

            return Redirect("");
        }
    }
}