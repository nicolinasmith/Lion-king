using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lion_king.Models
{
    public class Class
    {
        public int Class_id { get; set; }

        public string? Class_name { get; set; }

        public override string ToString()
        {
            return $"{Class_name}";
        }
    }
}
