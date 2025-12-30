using System.ComponentModel.DataAnnotations;

namespace University_system.DTO
{
    public class AddStudentDTO
    {
        [Required, MaxLength(50)]
        public string First_Name { get; set; }
        [Required, MaxLength(50)]
        public string Last_Name { get; set; }
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(20)]
        public string Gender { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [Required]
        public string Phone_Number { get; set; }
        [Required]
        public int Year_of_registration { get; set; }
        public double GPA { get; set; }
        public bool Is_Grant { get; set; }
        public int Student_Balance { get; set; }
    }
}