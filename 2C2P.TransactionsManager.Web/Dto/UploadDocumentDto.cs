using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using _2C2P.TransactionsManager.Infrastructure;
using _2C2P.TransactionsManager.Web.Attributes;
using Microsoft.AspNetCore.Http;

namespace _2C2P.TransactionsManager.Web.Dto
{
    public class UploadDocumentDto
    {
        [Required]
        [AllowedFileExtension(new FileExtension[]{FileExtension.Csv, FileExtension.Xml})]
        [AllowedFileSize(1024*1024)]
        public IFormFile FormFile { get; set; }

        public FileExtension GetExtension()
        {
            var extension = FormFile?.FileName?.Split('.').Last();
            if (Enum.TryParse(typeof(FileExtension), extension, true, out object ext))
            {
                return (FileExtension) ext;
            }

            throw new Exception($"{extension} extension is not supported.");
        }
    }
}