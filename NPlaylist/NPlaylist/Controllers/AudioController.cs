using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Authorization;
using NPlaylist.Business.AudioLogic;
using NPlaylist.Models;
using NPlaylist.Persistence.DbModels;
using NPlaylist.ViewModels.Audio;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Controllers
{
    public class AudioController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IAudioService _audioService;
        private readonly IMapper _mapper;

        public AudioController(
            IAuthorizationService authorizationService,
            IAudioService audioService,
            IMapper mapper)
        {
            _authorizationService = authorizationService;
            _audioService = audioService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken ct, int page = 1)
        {
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
            const int defaultCount = 1;

            var audioPaginationDto = await _audioService.GetAudioPaginationAsync(page, defaultCount, ct);
            var paginationViewModel = _mapper.Map<AudioPaginatedListViewModel>(audioPaginationDto);
            return View(paginationViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(CancellationToken ct, Guid id)
        {
            var audio = await _audioService.GetAudioAsync(id, ct);
            if (audio == null)
            {
                return AudioNotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, audio, Operations.Delete);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var audioViewModel = _mapper.Map<AudioViewModel>(audio);
            return View(audioViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _audioService.DeleteAudioAsync(id, ct);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, CancellationToken ct)
        {
            var audio = await _audioService.GetAudioAsync(id, ct);
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
                await _audioService.UpdateAudioAsync(audio, ct);
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
                await _audioService.UploadAudioAsync(dto, ct);
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