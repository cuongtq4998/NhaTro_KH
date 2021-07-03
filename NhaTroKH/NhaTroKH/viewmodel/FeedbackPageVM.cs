using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NhaTroKH.extension;
using NhaTroKH.model;
using NhaTroKH.Models;
using NhaTroKH.service;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{
    public class FeedbackPageVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand BackButton { get; set; }
        public ICommand AddFeedback { get; set; }
        public INavigation navigation { get; set; } 
        restAPI rest = new restAPI();


        public int NoRoomEntry { get; set; }
        public string ContentFeedback { get; set; }

        public bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value) return;
                isBusy = value;
                this.OnPropertyChanged("IsBusy");
            }
        }

        public bool layerLoadIsHidden = false;
        public bool LayerLoadIsHidden
        {
            get => layerLoadIsHidden;
            set
            {
                if (layerLoadIsHidden == value) return;
                layerLoadIsHidden = value;
                this.OnPropertyChanged("LayerLoadIsHidden");
            }
        } 

        public bool layerBaseIsHidden = true;
        public bool LayerBaseIsHidden
        {
            get => layerBaseIsHidden;
            set
            {
                if (layerBaseIsHidden == value) return;
                layerBaseIsHidden = value;
                this.OnPropertyChanged("LayerBaseIsHidden");
            }
        }

        public FeedbackPageVM()
        {
        }

        public FeedbackPageVM(INavigation navigation)
        { 
            this.navigation = navigation;
            
            this.BackButton = new Command(() => this.popPage());
            this.AddFeedback = new Command(async () => await this.addFeedback());
        }

        public void popPage()
        {
            this.navigation.PopAsync();
        }

        public async Task addFeedback()
        {
            this.showLoadding();

            if (this.isValidateOption())
            {
                var data = new FEEDBACK
                {
                    CMND = UserData.shared.IDCard,
                    Ngay = DateTime.Now.toDateWithFormat("yyyy-MM-dd HH:mm:ss"),
                    Phong = this.NoRoomEntry.ToString(),
                    STT = "",
                    TrangThai = "0",
                    PhanHoi = this.ContentFeedback
                };

                var result = await this.rest.insertFeedback(this.NoRoomEntry.ToString(), data);
                if (result)
                {
                    this.showPopup("Thêm thành công");
                }
                else
                {
                    this.showPopup("Thất bại");
                }
            }
            else
            {
                this.showPopup("Giá trị không hợp lệ, mời bạn kiểm tra lại nhé!");
            }
        }

        private void showPopup(string contentMessage)
        {
            _ = App.Current.MainPage.DisplayAlert("Thông báo", contentMessage, "OK");
            this.hiddenLoadding();
        }

        private void showLoadding()
        {
            this.IsBusy = true;
            this.LayerLoadIsHidden = true;
        }

        private void hiddenLoadding()
        {
            this.LayerLoadIsHidden = false;
            this.LayerBaseIsHidden = true;
            this.IsBusy = false; 
        }

        private bool isValidateOption()
        {  
            if (this.NoRoomEntry != 0 &&
                this.NoRoomEntry.ToString().isNullOrEmpty() &&
                this.ContentFeedback.isNullOrEmpty())
            {
                return true;
            }
            return false;
        }
    }
}
