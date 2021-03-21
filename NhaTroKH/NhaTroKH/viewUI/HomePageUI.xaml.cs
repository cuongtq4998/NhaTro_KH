using System;
using NhaTroKH.DB;
using NhaTroKH.viewmodel;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class HomePageUI : ContentPage
    {
        private bool cancelInputSetting = true;

        public HomePageUI()
        {
            InitializeComponent();
            BindingContext = new HomePageVM(Navigation);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.DefaultAddress) ||
                !Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.HometownSiteSetting) ||
                !Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.ResidentSiteSetting) 
                )
            {
                if (cancelInputSetting)
                {
                    bool notify = await DisplayAlert(
                        "Thông báo", // title
                        "Thành công, Bạn có muốn thiết lập thông tin địa chỉ? ", // message
                        "Thiết lập", // true
                        "Không, Tiếp tục"); // false
                    
                    if (notify)
                    {
                        await Navigation.PushAsync(new SettingPageUI());
                        cancelInputSetting = false;
                    }
                    else { cancelInputSetting = false; }
                }
            }
        }
         
    } 
}
