using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS_72210454.Data
{
    public class GroupedCourse : ObservableCollection<Courses>
    {
        public string CategoryName { get; set; }

        public GroupedCourse(string categoryName, IEnumerable<Courses> courses) : base(courses)
        {
            CategoryName = categoryName;
        }
    }
}
