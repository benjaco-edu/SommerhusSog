using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactions.Core;
using SommerhusSog.Annotations;
using SommerhusSog.Common;
using SommerhusSog.Models;

namespace SommerhusSog.ViewModels
{
    class Bookside : INotifyPropertyChanged
    {
        public string Navn { get; set; }
        public string Telefonnr { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }

        public string SynligStatus
        {
            get { return _synligStatus; }
            set { _synligStatus = value; OnPropertyChanged();}
        }

        public string Aar
        {
            get { return _aar; }
            set { _aar = value; TestBookAvalible(); }
        }

        public string Uge
        {
            get { return _uge; }
            set { _uge = value; TestBookAvalible(); }
        }


        public Boolean MaaBooke
        {
            get { return _maaBooke; }
            set
            {
                // Workaround til convert True/false til Xaml kode
                SynligStatus = (!value ? "Visible" : "Collapsed");
                _maaBooke = value; OnPropertyChanged();
            }
        }
        private bool _maaBooke;
        private string _aar;
        private string _uge;
        private string _synligStatus = "Collapsed";

        public Hus SelectedHus { get; set; }
        public Bookside()
        {
            SelectedHus = Dataholder.SelectedHus;
            Aar = Dataholder.SelectedAar;
            Uge = Dataholder.SelectedUge;

            TestBookAvalible();
            
        }

        public void TestBookAvalible()
        {
            int uge, aar;
            if (Int32.TryParse(Aar, out aar) && Int32.TryParse(Uge, out uge))
            {
                if (aar >= Kalender.GetYear() && uge <= Kalender.GetWeeksInYear(aar) && (uge > Kalender.GetWeekOfYear() || aar > Kalender.GetYear()))
                {
                    MaaBooke = Kalender.ErLedig(SelectedHus, uge, aar);
                }
                else
                {
                    MaaBooke = false;
                }
            }
            else
            {
                MaaBooke = false;
            }
        }

        public void Book() {
            int uge, aar;
            if (Int32.TryParse(Aar, out aar) && Int32.TryParse(Uge, out uge))
            {
                if (aar >= Kalender.GetYear() && uge <= Kalender.GetWeeksInYear(aar) && (uge > Kalender.GetWeekOfYear() || aar > Kalender.GetYear()))
                {
                    Boolean bookede = Kalender.Book(SelectedHus, uge, aar, new Lejer(Navn, Telefonnr, Email, Adresse));
                    if (bookede)
                    {
                        new MessageDialog(
                            $"Booking\n\n{SelectedHus}\n\n\nNavn:{Navn}\nTelefon nr.{Telefonnr}\nEmail:{Email}\nAdresse{Adresse}\n\n\nÅr:{Aar}\nUge:{Uge}")
                            .ShowAsync();
                        MaaBooke = false;
                    }
                }
            }

        }

        public void Back() {
            ((Frame)Window.Current.Content).GoBack();
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
