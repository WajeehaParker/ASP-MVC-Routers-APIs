using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z\d])\S{8,20}$", ErrorMessage = "Password should be between 8-20 letters and must contain letter number and special character.")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Department { get; set; }
    }
}
