using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion_king.Models
{
    public class Animal
    {
        public int animal_id { get; set; }

        public string name { get; set; }

        public Species Species { get; set; } //species_id? testar ändra
    }
}
