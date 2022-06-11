namespace EfCore.Console.Dal
{
    public class Teacher : Person
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public PhysicalPersonFeatures PhysicalPersonFeatures { get; set; }
        public List<Student> Students { get; set; } = new();
    }
}
