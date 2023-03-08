using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion_king.Models
{
    public class Species
    {
        public int species_id { get; set; }

        public string common_name { get; set; }

        public string latin_name { get; set; }

        public Class Class { get; set; }
    }
}
