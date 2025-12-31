namespace University_system.Model
{
    public class MaterialStudent
    {
        public Guid MaterialId { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Material Material { get; set; }
        public int marks { get; set; }  
    }
}
