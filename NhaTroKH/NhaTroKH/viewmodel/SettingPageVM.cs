using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NhaTroKH.viewUI;
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewmodel
{
    /// <summary>
    /// View Model Setting
    /// </summary>
    public class SettingPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation navigation { get; set; }
        public ICommand ResidentButton { get; private set; }
        public ICommand HometownButton { get; private set; }
        public ICommand DefaultAddressButton { get; private set; }



        public SettingPageVM(INavigation navigation)
        {
            this.navigation = navigation; 

            ResidentButton = new Command(async () => await goToPageAddress_Resident());
            HometownButton = new Command(async () => await goToPageAddress_Hometown());
            DefaultAddressButton = new Command(async () => await goToPageAddress_DefaultAddress());
        }

        private async Task goToPageAddress_Resident()
        {
            var view = new AddAddressPageUI(ETypePageAddAddress.EDefaultAddress, (int)ESetting.ResidentSetting);
            await navigation.PushAsync(view);
        }

        private async Task goToPageAddress_Hometown()
        {
            var view = new AddAddressPageUI(ETypePageAddAddress.EDefaultAddress, (int)ESetting.HometownSetting);
            await navigation.PushAsync(view);
        }

        private async Task goToPageAddress_DefaultAddress()
        {
            var view = new AddAddressPageUI(ETypePageAddAddress.EDefaultAddress, (int)ESetting.DefaultAddress);
            await navigation.PushAsync(view);
        }
    }
}
