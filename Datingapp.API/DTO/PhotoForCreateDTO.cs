using System;
using Microsoft.AspNetCore.Http;

namespace Datingapp.API.DTO
{
    public class PhotoForCreateDTO
    {
        public string Url { get; set; } 
        public IFormFile File { get; set; }
        public string  Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        public PhotoForCreateDTO()
        {
            DateAdded = DateTime.Now;
        }
    }
}