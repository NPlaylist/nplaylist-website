using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.DTOs;
using NPlaylist.Services.AudioService;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Controllers
{
    [Authorize]
    public class AudioController : Controller
    {
        private readonly IAudioService _audioService;

        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
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
        public async Task<IActionResult> Upload(UploadedFileDto uploadedFileDto, CancellationToken ct)
        {
            uploadedFileDto.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _audioService.Upload(uploadedFileDto, ct);

            return RedirectToAction("Index");
        }
    }
}