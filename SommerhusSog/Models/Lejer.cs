using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerhusSog.Models
{
    public class Lejer
    {
        public string Navn { get; set; }
        public string Telefonnr { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }

        // Konstruktor til at indeholde lejer information. 
        public Lejer(string navn, string telefonnr, string email, string adresse) {
            Navn = navn;
            Telefonnr = telefonnr;
            Email = email;
            Adresse = adresse;
        }
        
        
    }
}
