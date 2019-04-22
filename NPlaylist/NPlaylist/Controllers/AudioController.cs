using System;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Models;
using System.Collections.Generic;
using NPlaylist.Infrastructure.Wrappers.DateTimeWrapper;

namespace NPlaylist.Controllers
{
    public class AudioController : Controller
    {
        public IActionResult Index()
        {
            IList<AudioEntryViewModel> entries = new List<AudioEntryViewModel>();
            entries.Add(new AudioEntryViewModel
            {
                Id = Guid.NewGuid(),
                Extension = "mp3",
                FullName = "Foo - Bar",
                Publisher = "Baz",
                Created = DateTime.Now
            });
            return View(entries);
        }
    }
}