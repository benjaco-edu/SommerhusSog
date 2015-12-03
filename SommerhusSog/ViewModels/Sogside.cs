using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using SommerhusSog.Annotations;
using SommerhusSog.Common;
using SommerhusSog.Models;

namespace SommerhusSog.ViewModels
{
    public class Sogside : INotifyPropertyChanged
    {



        public ObservableCollection<Hus> AlleHuse;
        private static ObservableCollection<Hus> _soegteHuse;
        private static string _sogNavn;
        private static int _sogSted = 0;
        private static string _sogAntalVaerelser;
        private static string _sogPris;

        public Boolean MaaBooke {
            get { return _maaBooke; }
            set { _maaBooke = value; OnPropertyChanged(); }
        }

        public Hus SelectecItem {
            get { return _selectecItem; }
            set { _selectecItem = value;
                MaaBooke = (value != null);
                DataHolder.selected = value;
                OnPropertyChanged(); }
        }

        public string AntalReultater {
            get { return _antalReultater; }
            set { _antalReultater = value; OnPropertyChanged(); }
        }

        private string[] lande = new string[] {"", "Frankrig", "Spanien" };
        private static string _antalReultater;
        private static Hus _selectecItem;
        private static bool _maaBooke;

        public ObservableCollection<Hus> SoegteHuse {
            get { return _soegteHuse;  }
        } 

        public string Sog_Navn {
            get { return _sogNavn; }
            set { _sogNavn = value; OnPropertyChanged(); UpdateCount(); }
        }

        public int Sog_Sted {
            get { return _sogSted; }
            set { _sogSted = value; OnPropertyChanged(); UpdateCount(); }
        }

        public string Sog_AntalVaerelser {
            get { return _sogAntalVaerelser; }
            set { _sogAntalVaerelser = value; OnPropertyChanged(); UpdateCount();}
        }

        public string Sog_Pris {
            get { return _sogPris; }
            set { _sogPris = value; OnPropertyChanged(); UpdateCount(); }
        }

        public Sogside() {
            AlleHuse = new ObservableCollection<Hus>();
            if (_soegteHuse == null) {
                _soegteHuse = new ObservableCollection<Hus>();
            }
            if (Sog_Navn == null)
            {
                Sog_Navn = "";
            }
            #region dummydata
            AlleHuse.Add(new Hus("Hus #1", "Frankrig", 7, 7999, "blabla"));
            AlleHuse.Add(new Hus("Hus #2", "Frankrig", 4, 4999, "blabla"));
            AlleHuse.Add(new Hus("Hus #4", "Spanien", 3, 3999, "blabla"));
            AlleHuse.Add(new Hus("Hus #5", "Spanien", 5, 5999, "blabla"));
            AlleHuse.Add(new Hus("Hus #6", "Frankrig", 1, 1999, "blabla"));
            AlleHuse.Add(new Hus("Hus #7", "Frankrig", 2, 2999, "blabla"));
            #endregion

            UpdateCount();
        }

        public void Soeg() {
            var huse = AlleHuse.Where(h=>true);
            if (Sog_Sted != 0) {
                huse = huse.Where(
                    h =>
                        h.Land.Equals(lande[Sog_Sted])
                    );
            }

            int maxpris;
            if (Int32.TryParse(Sog_Pris, out maxpris) ) {
                huse = huse.Where(
                    h => h.Pris <=  maxpris
                    );
            }
            int minvaerelser;
            if (Int32.TryParse(Sog_AntalVaerelser, out minvaerelser) ) {
                huse = huse.Where(
                    h => h.AntalVaerelser >= minvaerelser
                    );
            }
            if (!Sog_Navn.Equals("")) {
                huse = huse.Where(h => h.Navn.Contains(Sog_Navn));
            }

            _soegteHuse.Clear();
            foreach (Hus hus in huse) {
                _soegteHuse.Add(hus);
            }
        }

        public void UpdateCount() {
            var huse = AlleHuse.Where(h => true);
            if (Sog_Sted != 0)
            {
                huse = huse.Where(
                    h =>
                        h.Land.Equals(lande[Sog_Sted])
                    );
            }

            int maxpris;
            if (Int32.TryParse(Sog_Pris, out maxpris))
            {
                huse = huse.Where(
                    h => h.Pris <= maxpris
                    );
            }
            int minvaerelser;
            if (Int32.TryParse(Sog_AntalVaerelser, out minvaerelser))
            {
                huse = huse.Where(
                    h => h.AntalVaerelser >= minvaerelser
                    );
            }
            if (!Sog_Navn.Equals(""))
            {
                huse = huse.Where(h => h.Navn.Contains(Sog_Navn));
            }

            AntalReultater = huse.Count()+" resultater";
        }

        

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}