using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Business.PlaylistLogic;
using NPlaylist.ViewModels.Audio;
using NPlaylist.ViewModels.Playlist;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Controllers
{
    [Authorize]
    public class PlaylistController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlaylistService _playlistService;

        public PlaylistController(
            IMapper mapper,
            IPlaylistService playlistService)
        {
            _mapper = mapper;
            _playlistService = playlistService;
        }

        public async Task<IActionResult> Index(CancellationToken ct, int page = 1)
        {
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
            const int defaultCount = 10;

            var playlistPaginationDto = await _playlistService.GetPlaylistPaginationAsync(page, defaultCount, ct);
            var paginationViewModel = _mapper.Map<PlaylistPaginatedListViewModel>(playlistPaginationDto);
            return View(paginationViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(Guid? id)
        {
            var dummyPlaylist = new PlaylistDetailsViewModel
            {
                Title = "TestPlaylist",
                Description = "Playlist created just for test",
                OwnerUsername = "John Doe",
                UtcDateTime = DateTime.UtcNow,
                Audios = new List<AudioViewModel>
                {
                    new AudioViewModel
                    {
                        AudioId = Guid.NewGuid(),
                        Path = "dummy path",
                        Meta = new AudioMetaViewModel
                        {
                            Title = "Back in Black",
                            Author = "AC/DC",
                            Album = "Unknown"
                        }
                    },
                    new AudioViewModel
                    {
                        AudioId = Guid.NewGuid(),
                        Path = "dummy path2",
                        Meta = new AudioMetaViewModel
                        {
                            Title = "Thunder",
                            Author = "AC/DC",
                            Album = "Unknown"
                        }
                    }
                }
            };
            return View(dummyPlaylist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaylistCreateViewModel playlistCreateViewModel, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                playlistCreateViewModel.OwnerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var playlistCreateDto = _mapper.Map<PlaylistCreateDto>(playlistCreateViewModel);
                await _playlistService.CreatePlaylist(playlistCreateDto, ct);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}