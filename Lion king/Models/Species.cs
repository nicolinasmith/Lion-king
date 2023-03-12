using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lion_king.Models
{
    public class Species
    {
        public int Species_id { get; set; }

        public string? Common_name { get; set; }

        public string? Latin_name { get; set; }

        public Class? Class { get; set; }

        public override string ToString()
        {
            return $"{Common_name} {Latin_name} {Class}";
        }

    }
}
