using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NhaTroKH.DB;
using NhaTroKH.model;
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewmodel
{
    public class CustomerPageVM : INotifyPropertyChanged
    {
        public List<AddressSettingModel> Items { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation navigation { get; set; }

        //public ICommand ResidentSiteQuickButton { get; private set; }
        //public ICommand HometownSiteQuickButton { get; private set; }
        public ICommand PlaceDefaultSiteQuickButton { get; private set; }

        private string _textNhaTro;
        public string textNhaTro
        {
            get
            {
                return _textNhaTro;
            }
            set
            {
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
            this.dbAddressSetting();

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

        private List<AddressSettingModel> dbAddressSetting()
        {
            List<AddressSettingModel> list = new List<AddressSettingModel>();
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.ResidentSiteSetting)) {
                string address = Application.Current.Properties[KeyCustomerViewEnumeration.ResidentSiteSetting].ToString();
                list.Add(new AddressSettingModel { IsSelected = false, TitleAddress = address , ValueIsAddress = ValueIsAddress.isResident });
            }

            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.HometownSiteSetting)) {
                string address = Application.Current.Properties[KeyCustomerViewEnumeration.HometownSiteSetting].ToString();
                list.Add(new AddressSettingModel { IsSelected = false, TitleAddress = address , ValueIsAddress = ValueIsAddress.isAccomodation });
            }

            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.DefaultAddress))
            {
                string address = Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress].ToString();
                list.Add(new AddressSettingModel { IsSelected = false, TitleAddress = address, ValueIsAddress = ValueIsAddress.isCurrentSite });
            }

            Items = list;

            return list;
        }

    }
}
