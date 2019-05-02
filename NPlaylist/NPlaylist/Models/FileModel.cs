using Microsoft.AspNetCore.Http;
using NPlaylist.Business.Audio;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Models
{
    public class FileModel : IFile
    {
        private readonly IFormFile _formFile;

        public FileModel(IFormFile formFile)
        {
            _formFile = formFile;
        }

        public string ContentDisposition => _formFile.ContentDisposition;
        public string ContentType => _formFile.ContentType;
        public string FileName => _formFile.FileName;
        public long Length => _formFile.Length;
        public string Name => _formFile.Name;
        public void CopyTo(Stream target) => _formFile.CopyTo(target);

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken)) =>
            _formFile.CopyToAsync(target, cancellationToken);

        public Stream OpenReadStream() => _formFile.OpenReadStream();
    }
}