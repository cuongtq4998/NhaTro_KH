using System;
using System.Collections.Generic;
using System.Windows.Input;
using NhaTroKH.DB;
using NhaTroKH.model;
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewmodel
{
    public class CustomerProgessPageVM
    {
        public List<AddressSettingModel> Items { set; get; }
        public ICommand backButton { get; set; }
        public INavigation navigation { get; set; }

        public CustomerProgessPageVM(INavigation navigation)
        {
            this.navigation = navigation;
            dbAddressSetting(); 
            this.backButton = new Command(() => navigateBack());
        }

        /// <summary>
        /// Load data thông tin địa chỉ
        /// </summary>
        /// <returns></returns>
        private List<AddressSettingModel> dbAddressSetting()
        {
            List<AddressSettingModel> list = new List<AddressSettingModel>();
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.ResidentSiteSetting))
            {
                string address = Application.Current.Properties[KeyCustomerViewEnumeration.ResidentSiteSetting].ToString();
                list.Add(new AddressSettingModel { IsSelected = false, TitleAddress = address, ValueIsAddress = ValueIsAddress.isResident });
            }

            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.HometownSiteSetting))
            {
                string address = Application.Current.Properties[KeyCustomerViewEnumeration.HometownSiteSetting].ToString();
                list.Add(new AddressSettingModel { IsSelected = false, TitleAddress = address, ValueIsAddress = ValueIsAddress.isAccomodation });
            }

            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.DefaultAddress))
            {
                string address = Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress].ToString();
                list.Add(new AddressSettingModel { IsSelected = false, TitleAddress = address, ValueIsAddress = ValueIsAddress.isCurrentSite });
            }

            Items = list;

            return list;
        }


        private void navigateBack()
        {
            this.navigation.PopAsync();
        }
    }
}
