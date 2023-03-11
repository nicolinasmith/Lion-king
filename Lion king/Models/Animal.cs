using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Controls;

namespace Lion_king.Models
{
    public class Animal
    {
        public int Animal_id { get; set; }

        public string Name { get; set; }

        public Species? Species { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Species}";
        }
    }
}
