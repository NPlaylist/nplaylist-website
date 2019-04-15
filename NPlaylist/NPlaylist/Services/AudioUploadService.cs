using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NPlaylist.Data;
using NPlaylist.Models.Entity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NPlaylist.Services
{
    public class AudioUploadService : IAudioUploadService
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;

        public AudioUploadService(IHostingEnvironment env, ApplicationDbContext context)
        {
            _appEnvironment = env;
            _context = context;
        }

        public async Task Upload(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                throw new ArgumentNullException("Uploaded file is null");
            }

            string path = "/Files/" + uploadedFile.FileName;
            _context.Audios.Add(new Audio
            {
                Path = path,
                UserId = new Guid(),
                Meta = new AudioMeta
                {
                    Album = "test",
                    Title = "title",
                    Author = " Emil"
                }
            }
            );
            
            _context.SaveChanges();
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

        }
    }
}
