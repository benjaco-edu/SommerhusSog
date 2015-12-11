using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace  SommerhusSog.Models 
{

    
    public class Hus
    {
        public string Navn { get; set; }
        public string Land { get; set; }
        public int AntalVaerelser { get; set; }
        public int Pris { get; set; }
        public string Addresse { get; set; }

        public Dictionary<string, Lejer> HusKalender {get;set;} 



        public Hus(string navn, string land, int antalVaerelser, int pris, string addresse) {
            Navn = navn;
            Land = land;
            AntalVaerelser = antalVaerelser;
            Pris = pris;
            Addresse = addresse;
            HusKalender = new Dictionary<string, Lejer>();
        }


        public override string ToString() {
            return Navn+", "+Land+", "+AntalVaerelser+", "+Pris+", "+Addresse;
        }
    }
}