using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class HomePageUI : ContentPage
    {
        public HomePageUI()
        {
            InitializeComponent();
        }

        private void btnTTKT_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerPageUI());
        }

        private void btnDIENNUOC_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WaveInformationChargePageUI());
        }

        private void btnTTTP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoomInformationChartPageUI());
        }

        private void btnThoat_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnphanhoi_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FeedbackPageUI());
        }

        void btnSetting_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SettingPageUI());
        }
    }
}
