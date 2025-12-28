using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace University_system.Model
{
    public class User : IdentityUser
    {
        [Required,MaxLength(50)]
        public string First_Name {  get; set; }
        [Required,MaxLength(50)]
        public string Last_Name { get; set; }
        [Required,MaxLength(20)]
        public string Gender { get; set; } 
    }
}