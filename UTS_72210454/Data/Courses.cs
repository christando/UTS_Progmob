using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS_72210454.Data
{
    public class Courses
    {
        public int courseId { get; set; }
        public string name { get; set; }
        public string imageName { get; set; }
        public int duration { get; set; }
        public string description { get; set; }

        public Categories Category { get; set; }
    }
}
