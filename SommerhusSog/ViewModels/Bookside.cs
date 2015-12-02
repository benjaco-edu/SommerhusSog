using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
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



        public Hus SelectedHus { get; set; }
        public Bookside() {
            SelectedHus = DataHolder.selected;
        }

        public void Book() {
            new MessageDialog($"Booking\n\n{SelectedHus}\n\n\nNavn:{Navn}\nTelefon nr.{Telefonnr}\nEmail:{Email}\nAdresse{Adresse}").ShowAsync();
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
