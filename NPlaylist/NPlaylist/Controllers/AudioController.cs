using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Models;
using NPlaylist.Models.Audio;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;

namespace NPlaylist.Controllers
{
    public class AudioController : Controller
    {
        public IActionResult Index()
        {
            IList<AudioEntryViewModel> entries = new List<AudioEntryViewModel>
            {
                new AudioEntryViewModel
                {
                    Id = Guid.NewGuid(),
                    Extension = "mp3",
                    FullName = "Foo - Bar",
                    Publisher = "Baz",
                    UtcCreatedTime = DateTime.Now
                }
            };
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