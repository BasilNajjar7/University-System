using System.ComponentModel.DataAnnotations;

namespace University_system.Model
{
    public class Material
    {
        public string MaterialId {  get; set; }
        [Required,MaxLength(50)]
        public string Name {  get; set; }
        [Required]
        public int Number_of_hour { get; set; }
        public string Completion_requires { get; set;  }
        [Required]
        public string Subject_professor { get; set; }
        public ICollection<Student>Students { get; set; }
    }
}