using System;
using System.Collections.Generic;
using NhaTroKH.DB;
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewUI
{
    public partial class SettingPageUI : ContentPage
    {
        public SettingPageUI()
        {
            InitializeComponent();
        }
        void buttonSetAddress_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddAddressPageUI(ETypePageAddAddress.EDefaultAddress, 0));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.DefaultAddress))
            {
                buttonSetAddress.Text = Convert.ToString(Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress]);
                buttonSetAddress.TextColor = Color.Green;
            }
            else
            {
                buttonSetAddress.Text = "Chưa được thiết lập. Bạn có muốn thiết lập?";
                buttonSetAddress.TextColor = Color.Black;
            }
        }
    }
}
