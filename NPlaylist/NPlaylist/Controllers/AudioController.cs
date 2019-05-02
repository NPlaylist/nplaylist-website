using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Business.Audio;
using NPlaylist.Models;
using NPlaylist.ViewModels;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Models.Audio;

namespace NPlaylist.Controllers
{
    public class AudioController : Controller
    {
        private readonly Services.AudioService.IAudioService _audioServicePl;
        private readonly IAudioService _audioService;

        public AudioController(
            Services.AudioService.IAudioService audioServicePl,
            IAudioService audioService)
        {
            _audioServicePl = audioServicePl;
            _audioService = audioService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var entries = await _audioServicePl.GetAudioEntriesAsync(ct);
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

        [Authorize]
        public IActionResult Edit(Guid id, CancellationToken ct)
        {
            var audioViewModel = new AudioViewModel
            {
                AudioId = id,
                Meta = new AudioMetaViewModel
                {
                    Title = "Foo",
                    Album = "Kek",
                    Author = "Bar"
                }
            };

            return View(audioViewModel);
        }

        [HttpPut]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [FromForm] AudioViewModel audioViewModel, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(audioViewModel);
            }

            if (id != audioViewModel.AudioId)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload(AudioUploadViewModel uploadedFile, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                uploadedFile.PublisherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var dto = new AudioUploadDto
                {
                    File = new FileModel(uploadedFile.File),
                    PublisherId = uploadedFile.PublisherId
                };
                await _audioService.UploadAsync(dto, ct);
            }

            return View();
        }
    }
}