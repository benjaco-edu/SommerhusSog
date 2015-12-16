using System;
using SommerhusSog.Models;

namespace SommerhusSog.Common
{
    public class Dataholder
    {
        // gemmer variabler i statiske variabler så man kan sætte dem i den ene VM og hente dem i dan anden VM

        public static Hus SelectedHus;
        public static String SelectedUge;
        public static String SelectedAar;

    }
}