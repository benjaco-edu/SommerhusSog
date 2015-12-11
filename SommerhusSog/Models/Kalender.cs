using System;

namespace SommerhusSog.Models
{
    public class Kalender
    {
        public static Boolean ErLedig(Hus hus, int uge, int aar)
        {
            return hus.HusKalender.ContainsKey(aar + "/" + uge);
        }
    }
}