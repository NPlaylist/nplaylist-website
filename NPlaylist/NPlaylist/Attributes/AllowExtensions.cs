using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NPlaylist.Attributes
{
    public class AllowExtensions : ValidationAttribute
    {
        public string Extensions { get; set; } = "mp3,wav";

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            bool isValid = true;

            List<string> allowedExtensions = Extensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (file != null)
            {
                var fileName = file.FileName.ToLower();

                isValid = allowedExtensions.Any(y => fileName.EndsWith(y));
            }

            return isValid;
        }
    }
}