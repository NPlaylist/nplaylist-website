using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Business.Audio;
using NPlaylist.Models;
using NPlaylist.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Models.Audio;
using NPlaylist.Authorization;
using AutoMapper;
using NPlaylist.Persistence.DbModels;
using Microsoft.AspNetCore.Http;

namespace NPlaylist.Controllers
{
    public class AudioController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly Services.AudioService.IAudioService _audioServicePl;
        private readonly IAudioService _audioService;
        private readonly IMapper _mapper;

        public AudioController(
            IAuthorizationService authorizationService,
            Services.AudioService.IAudioService audioServicePl,
            IAudioService audioService,
            IMapper mapper)
        {
            _authorizationService = authorizationService;
            _audioServicePl = audioServicePl;
            _audioService = audioService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken ct, int page = 1)
        {
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
            const int defaultCount = 10;

            var entries = await _audioServicePl.GetAudioEntriesRangeAsync(page, defaultCount, ct);
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

        [HttpPost]
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
        public async Task<IActionResult> Edit(Guid id, CancellationToken ct)
        {
            var audio = await _audioServicePl.GetAudioAsync(id, ct);
            if (audio == null)
            {
                return AudioNotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, audio, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var audioViewModel = _mapper.Map<AudioViewModel>(audio);
            return View(audioViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] AudioViewModel audioViewModel, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(audioViewModel);
            }

            var audio = _mapper.Map<Audio>(audioViewModel);

            var authResult = await _authorizationService.AuthorizeAsync(User, audio, Operations.Update);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            try
            {
                await _audioServicePl.UpdateAudioAsync(audio, ct);
            }
            catch (KeyNotFoundException)
            {
                return AudioNotFound();
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

        private IActionResult AudioNotFound()
        {
            return new ViewResult
            {
                ViewName = "AudioNotFound",
                StatusCode = StatusCodes.Status404NotFound
            };
        }
    }
}