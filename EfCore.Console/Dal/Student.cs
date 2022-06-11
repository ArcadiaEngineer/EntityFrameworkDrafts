namespace EfCore.Console.Dal
{
    public class Student : Person
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public PhysicalPersonFeatures PhysicalPersonFeatures { get; set; }
        public List<Teacher> Teachers { get; set; } = new();
    }
}
