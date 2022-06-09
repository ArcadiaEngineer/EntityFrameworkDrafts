using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Dal
{
    public class Student : Person
    {
        public int Id { get; set; }

        public string StudentNumber { get; set; }

        public List<Teacher> Teachers { get; set; } = new();
    }
}
