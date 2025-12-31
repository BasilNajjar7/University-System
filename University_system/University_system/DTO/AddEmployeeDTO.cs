using System.ComponentModel.DataAnnotations;

namespace University_system.DTO
{
    public class AddEmployeeDTO
    {
        public Guid Id { get; set; }
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
        public string Job_Title { get; set; }
        [Required]
        public int Salary { get; set; }
    }
}
