using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Newtonsoft.Json;

namespace SommerhusSog.Models
{
    public class KollektionHus
    {
        private static string _saveFile = ApplicationData.Current.RoamingFolder.Path + "/" + "KollektionHus.json";
        private static ObservableCollection<Hus> Husene = null;

        static string SrcJSON
        {
            get { return _saveFile; }
            set { _saveFile = value; }
        }

        public static ObservableCollection<Hus> HentAlle()
        {


            // Hvis der ingen fil findes ved filestream

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
                return Husene;
            }
            else
            {
                return HentFraFil();
            }
        }
        public static void GemAlle()
        {
            if (Husene != null)
            {
                string json = JsonConvert.SerializeObject(Husene);
                File.WriteAllText(SrcJSON, json);

            }
        }

        private static ObservableCollection<Hus> HentFraFil()
        {
            string her = File.ReadAllText(SrcJSON);
            ObservableCollection<Hus> huse = JsonConvert.DeserializeObject<ObservableCollection<Hus>>(her);
            return huse;
        }

    }
}