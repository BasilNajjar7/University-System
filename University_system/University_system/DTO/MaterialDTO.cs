using System.ComponentModel.DataAnnotations;

namespace University_system.DTO
{
    public class MaterialDTO
    {
        public Guid MaterialId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Number_of_hour { get; set; }
        public string Completion_requires { get; set; }
        [Required]
        public string Subject_professor { get; set; }
    }
}
