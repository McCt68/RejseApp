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
        private string _destination = ""; 

        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        // Auto Property, Compiler genererer selv et private field behind the scenes
        public decimal Pris { get; set; } // 128 bit mere præcis end double - Default OM - vil ses som 0
        public DateTime Dato { get; set; } // Default DateTime.MinValue - vil ses som 1/1/0001 12:00:00 AM

        // Denne klasse har ikke en Constructor -
        // Hvis Constructor er udeladt så laver C# -
        // selv en constructor uden parametre, og initialiserer fields til deres default værdi.        

    }
}
