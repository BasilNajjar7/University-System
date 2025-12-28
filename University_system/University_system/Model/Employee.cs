using System.ComponentModel.DataAnnotations;

namespace University_system.Model
{
    public class Employee : User
    {
        [Required]
        public int Salary { get; set; }
    }
}