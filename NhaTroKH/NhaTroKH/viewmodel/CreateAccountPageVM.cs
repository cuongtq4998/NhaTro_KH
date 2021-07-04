using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NhaTroKH.extension;
using NhaTroKH.service;
using NhaTroKH.viewUI;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{
    public class CreateAccountPageVM: INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        restAPI rest = new restAPI();
        public INavigation navigation { get; set; }
        public ICommand BackButton { get; set; }
        public ICommand CreateUser { get; set; }
        public string PasswordUser { get; set; } 

        private double iDCard = 0;
        public double IDCard
        {
            get { return iDCard; }
            set
            {
                iDCard = value;
                IDCardTextChangedCommand.Execute(IDCard);
                OnPropertyChanged();
            }
        }

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

        public Command IDCardTextChangedCommand => new Command<double>((IDCard) => IdCardChanged(IDCard));


        public CreateAccountPageVM()
        {
        }
        public CreateAccountPageVM(INavigation navigation)
        {
            this.navigation = navigation;

            this.BackButton = new Command(() => this.backAction());
            this.CreateUser = new Command(async () => await this.createUserAction());
        }

        private void backAction()
        {
            this.navigation.PopAsync();
        }

        private async Task createUserAction()
        {
            this.showLoadding();
            if (this.isValidate())
            {
                bool result = await this.rest.createUser(this.iDCard.ToString(), this.PasswordUser);
                if (result)
                {
                    //success
                    this.showPopup("Thành công");
                    _ = navigation.PushAsync(new LoginPageUI());
                    this.iDCard = 0;
                    this.PasswordUser = string.Empty;
                }
                else
                {
                    // failed
                    this.showPopup("Tạo tài khoản thất bại");
                }
            }
            else
            {
                this.showPopup("Giá trị không hợp lệ: Kiểm tra CMND(9 số)/ CCCD(12 số), Mật khẩu không được bỏ trống!");
            }
        }

        private void IdCardChanged(double _idCard)
        {
            if (_idCard.ToString().Length > 12)
            {
                Console.WriteLine("");
                string entryMax = _idCard.ToString(); 
                entryMax = entryMax.Remove(entryMax.Length - 1); 
                IDCard = Double.Parse(entryMax);
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

        private bool isValidate()
        { 
            if (this.iDCard.ToString().Length == 12 || this.iDCard.ToString().Length == 9)
            {
                if (this.PasswordUser != null)
                {
                    if(!this.PasswordUser.ToString().isNullOrEmpty())
                    {
                        return true;
                    } 
                }
                else { return false; }
            }  
            return false;
        }
    }
}
