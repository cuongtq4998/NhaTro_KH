using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NhaTroKH.DB;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{
    public class CustomerPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation navigation { get; set; }

        public ICommand ResidentSiteQuickButton { get; private set; }
        public ICommand HometownSiteQuickButton { get; private set; }
        public ICommand PlaceDefaultSiteQuickButton { get; private set; }

        private string _textNhaTro;
        public string textNhaTro
        {
            get
            {
                return _textNhaTro;
            }
            set {
                if (_textNhaTro != value)
                {
                    _textNhaTro = value;
                    onPropertyChanged();
                }
            }
        } 

        public CustomerPageVM(INavigation navigation)
        {
            this.navigation = navigation;

            //ResidentSiteQuickButton = new Command(async () => await chooseResiseQuick() );
            //HometownSiteQuickButton = new Command(async () => await chooseHometownQuick());
            PlaceDefaultSiteQuickButton = new Command(() =>
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.DefaultAddress))
                {
                    _textNhaTro = Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress].ToString();
                }
            });
        }

        void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
