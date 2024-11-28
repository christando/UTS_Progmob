using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UTS_72210454.Data
{
    [Table("Categories")]
    public class Categories
    {
        [PrimaryKey, AutoIncrement]
        public int categoryId { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        [MaxLength(250)]
        public string description { get; set; }
    }
}
