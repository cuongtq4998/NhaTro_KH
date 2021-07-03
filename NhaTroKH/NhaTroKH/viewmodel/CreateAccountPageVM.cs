using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NhaTroKH.service;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{
    public class CreateAccountPageVM: INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        restAPI rest = new restAPI();
        public INavigation navigation { get; set; }

        public CreateAccountPageVM()
        {
        } 
        public CreateAccountPageVM(INavigation navigation)
        {
            this.navigation = navigation; 
        }
    }
}
