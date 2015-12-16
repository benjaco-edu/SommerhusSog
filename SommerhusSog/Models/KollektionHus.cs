using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Newtonsoft.Json;

namespace SommerhusSog.Models
{
    // Klasse til Hentning og Gemning af Hus kollektionen. 
    public class KollektionHus
    {
        // Lokation hvor json fil skal gemmes
        private static string _saveFile = ApplicationData.Current.RoamingFolder.Path + "/" + "KollektionHus.json";
        private static ObservableCollection<Hus> Husene = null;

        static string SrcJSON
        {
            get { return _saveFile; }
            set { _saveFile = value; }
        }

        public static ObservableCollection<Hus> HentAlle()
        {

            // Hvis fil ikke kan findes, så indsæt dummy data
            if (!File.Exists(SrcJSON))
            {
                #region Dummy data

                Husene = new ObservableCollection<Hus>();
                Husene.Add(new Hus("Hus #1", "Frankrig", 7, 7999, "blabla"));
                Husene.Add(new Hus("Hus #2", "Frankrig", 4, 4999, "blabla"));
                Husene.Add(new Hus("Hus #4", "Spanien", 3, 3999, "blabla"));
                Husene.Add(new Hus("Hus #5", "Spanien", 5, 5999, "blabla"));
                Husene.Add(new Hus("Hus #6", "Frankrig", 1, 1999, "blabla"));
                Husene.Add(new Hus("Hus #7", "Frankrig", 2, 2999, "blabla"));
                Husene.Add(new Hus("Hus #8", "Frankrig", 2, 2999, "blabla"));
                Husene.Add(new Hus("Hus #9", "Frankrig", 2, 2999, "blabla"));
                Husene.Add(new Hus("Hus #10", "Frankrig", 2, 2999, "blabla"));
                
                GemAlle();
                #endregion
            }
            // Hvis json fil findes, hent derfra
            else if (Husene == null)
            {
                HentFraFil();
            }
            
            return Husene;
        }

        // gem alle hus objekter til json fil!
        public static void GemAlle()
        {
            if (Husene != null)
            {
                string json = JsonConvert.SerializeObject(Husene);
                File.WriteAllText(SrcJSON, json);
            }
        }

        // Hent og konverter alle hus objekter fra fil
        private static void HentFraFil()
        {
            string jsonData = File.ReadAllText(SrcJSON);
            
            Husene = JsonConvert.DeserializeObject<ObservableCollection<Hus>>(jsonData);
        }

    }
}