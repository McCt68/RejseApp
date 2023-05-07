using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejseApp.Models
{
    public class Rejse
    {

        // Encapsulation
        private string _destination;

        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        // Auto Property, Compiler genererer selv et private field behind the scenes
        public decimal Pris { get; set; }

        public DateTime Dato { get; set; }

    }
}
