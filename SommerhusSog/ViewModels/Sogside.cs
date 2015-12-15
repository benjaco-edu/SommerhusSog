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



        private static ObservableCollection<Hus> _soegteHuse;
        private static string _sogNavn;
        private static int _sogSted = 0;
        private static string _sogAntalVaerelser;
        private static string _sogPris;
        private static string _sogAar;
        private static string _sogUge;

        public Boolean MaaBooke {
            get { return _maaBooke; }
            set { _maaBooke = value; OnPropertyChanged(); }
        }

        public Hus SelectecItem {
            get { return _selectecItem; }
            set { _selectecItem = value;
                Dataholder.SelectedHus = value;
                MaaBooke = (value != null);
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

        public string Sog_Aar {
            get { return _sogAar; }
            set { _sogAar=value;
                Dataholder.SelectedAar = value; OnPropertyChanged(); UpdateCount(); }
        }

        public string Sog_Uge {
            get { return _sogUge; }
            set { _sogUge=value;
                Dataholder.SelectedUge = value; OnPropertyChanged(); UpdateCount(); }
        }

            

        public Sogside() {
            if (_soegteHuse == null) {
                _soegteHuse = new ObservableCollection<Hus>();
            }
            if (Sog_Navn == null)
            {
                Sog_Navn = "";
            }


            UpdateCount();
            Soeg();
        }

        public void Soeg() {
            var huse = KollektionHus.HentAlle().Where(h=>true);
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
            int uge, aar;
            if (Int32.TryParse(Sog_Aar, out aar) && Int32.TryParse(Sog_Uge, out uge))
            {
                if (aar >= Kalender.GetYear() && uge <= Kalender.GetWeeksInYear(aar) && (uge > Kalender.GetWeekOfYear() || aar > Kalender.GetYear())) {
                    huse = huse.Where(h => Kalender.ErLedig(h, uge, aar));
                }
                else {
                    new MessageDialog($"Intast en valid uge").ShowAsync();
                }
            }

            _soegteHuse.Clear();
            foreach (Hus hus in huse) {
                _soegteHuse.Add(hus);
            }
        }

        public void UpdateCount() {
            var huse = KollektionHus.HentAlle().Where(h => true);
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

            int uge, aar;
            if (Int32.TryParse(Sog_Aar, out aar) && Int32.TryParse(Sog_Uge, out uge)) {
                if (aar >= Kalender.GetYear() && ( uge>Kalender.GetWeekOfYear() || aar > Kalender.GetYear() ) ) {
                    huse = huse.Where(h => Kalender.ErLedig(h, uge, aar) );
                }
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