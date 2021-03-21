using System; 
using NhaTroKH.DB;
using NhaTroKH.viewmodel;
using Xamarin.Forms; 

namespace NhaTroKH.viewUI
{
    public partial class SettingPageUI : ContentPage
    { 
        public SettingPageUI()
        {
            InitializeComponent();
            BindingContext = new SettingPageVM(Navigation);
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


            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.ResidentSiteSetting))
            {
                ResidentButton.Text = Convert.ToString(Application.Current.Properties[KeyCustomerViewEnumeration.ResidentSiteSetting]);
                ResidentButton.TextColor = Color.Green;
            }
            else
            {
                ResidentButton.Text = "Chưa được thiết lập. Bạn có muốn thiết lập?";
                ResidentButton.TextColor = Color.Black;
            }

            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.HometownSiteSetting))
            {
                HometownButton.Text = Convert.ToString(Application.Current.Properties[KeyCustomerViewEnumeration.HometownSiteSetting]);
                HometownButton.TextColor = Color.Green;
            }
            else
            {
                HometownButton.Text = "Chưa được thiết lập. Bạn có muốn thiết lập?";
                HometownButton.TextColor = Color.Black;
            }
        }
    } 
}

