using System;
using System.ComponentModel.DataAnnotations;

namespace Datingapp.API.DTO
{
    public class UserForRegisterDTO
    {
        [Required]
        public string Username  { get; set; }
        [Required]
        [StringLength(8, MinimumLength= 4,ErrorMessage = "Password between 4 adn 8 Chars")]
        public string Password { get; set; }
        
         [Required]
         public string Gender { get; set; }
         [Required]
         public string KnownAs { get; set; }
         [Required]
         public DateTime DateOfBirth { get; set; }
         [Required]
         public string City { get; set; }
         [Required]
         public string Country { get; set; }
         
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDTO(){
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}