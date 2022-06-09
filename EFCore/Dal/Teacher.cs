using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Dal
{
    public class Teacher : Person
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public List<Student> Students { get; set; } = new();
    }
}
