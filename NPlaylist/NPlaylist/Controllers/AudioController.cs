using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Models;
using NPlaylist.Models.Audio;
using NPlaylist.Services.AudioService;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Controllers
{
    public class AudioController : Controller
    {
        private readonly IAudioService _audioService;

        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var entries = await _audioService.GetAudioEntriesAsync(ct);
            return View(entries);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var audioViewModel = new AudioViewModel
            {
                AudioId = id,
                UtcCreatedTime = DateTime.Now,
                Path = "Foo/Bar",
                Meta = new AudioMetaViewModel
                {
                    Title = "John",
                    Author = "Doe"
                }
            };
            return View(audioViewModel);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(Guid id, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View("Delete");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Upload(AudioUploadViewModel uploadedFile)
        {
            if (ModelState.IsValid)
            {
                uploadedFile.PublisherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return View();
        }
    }
}