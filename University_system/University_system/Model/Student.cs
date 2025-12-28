using System.ComponentModel.DataAnnotations;

namespace University_system.Model
{
    public class Student : User
    {
        [Required]
        public int Year_of_registration {  get; set; }
        public double GPA { get; set; }
        public bool Is_Grant { get; set; }
        public int Student_Balance { get; set; }
        public ICollection<Material> Materials { get; set; }
    }
}