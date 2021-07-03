using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NhaTroKH.viewUI;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{
    public class HomePageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation navigation { get; set; }

        public ICommand PageCustomerButton { get; private set; }
        public ICommand PageCostWaterButton { get; private set; }
        public ICommand PageCostRoomButton { get; private set; }
        public ICommand PageFeedbackButton { get; private set; }
        public ICommand PageSettingButton { get; private set; }
        public ICommand ExitButton { get; private set; }

        public HomePageVM(INavigation navigation)
        {
            this.navigation = navigation;
            PageCustomerButton = new Command(async () => await goToPageCustomer());
            PageCostWaterButton = new Command(async () => await goToPageCostWater());
            PageCostRoomButton = new Command(async () => await goToPageCostRoom());
            PageFeedbackButton = new Command(async () => await goToPageFeedback());
            PageSettingButton = new Command(async () => await goToPageSetting());
            ExitButton = new Command(async () => await eventExit());
        }

        private async Task goToPageCustomer()
        {
            var view = new CustomerPageUI();
            await navigation.PushAsync(view);
        }

        private async Task goToPageCostWater()
        {
            var view = new WaveInformationChargePageUI();
            await navigation.PushAsync(view);
        }

        private async Task goToPageCostRoom()
        {
            var view = new RoomInformationChartPageUI();
            await navigation.PushAsync(view);
        }

        private async Task goToPageFeedback()
        {
            var view = new FeedbackPageUI();
            await navigation.PushAsync(view);
        }

        private async Task goToPageSetting()
        {
            var view = new SettingPageUI();
            await navigation.PushAsync(view);
        }

        private async Task eventExit()
        { 
            bool notify = await App.Current.MainPage.DisplayAlert(
                        "Thông báo", // title
                        "Bạn có muốn thoát không?", // message
                        "Có", // true
                        "Không"); // false

            if (notify)
            {
                await navigation.PopAsync();
            } 
        }

    }
}
