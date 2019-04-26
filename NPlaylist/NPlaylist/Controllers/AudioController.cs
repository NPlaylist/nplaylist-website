using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Models;
using NPlaylist.Models.Audio;
using System;
using System.Collections.Generic;
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
    }
}