using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input; 
using NhaTroKH.model; 
using NhaTroKH.service; 
using NhaTroKH.viewUI;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{
    public class LoginPageVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand sendLogin { get; set; }
        public ICommand createAccount { get; set; }
        public ICommand exitApp { get; set; }
        public INavigation navigation { get; set; }
        bool isHaveInSqlite = false;
        restAPI rest = new restAPI();

        public string idCard = "";
        public string IDCard
        {
            get => idCard;
            set
            {
                if (idCard == value) return;
                idCard = value;
                this.OnPropertyChanged("IDCard");
            }
        }

        public string passAccount = "";
        public string PassAccount
        {
            get => passAccount;
            set
            {
                if (passAccount == value) return;
                passAccount = value;
                this.OnPropertyChanged("PassAccount");
            }
        }

        /// <summary>
        /// check is  save password
        /// </summary>
        public bool isCheckPass = false;
        public bool IsCheckPass
        {
            get => isCheckPass;
            set
            {
                if (isCheckPass == value) return;
                isCheckPass = value; 
                this.OnPropertyChanged("IsCheckPass");
            }
        }

        public bool selected = true;
        public bool EntryIsFocused
        {
            get => selected;
            set
            {
                if (selected == value) return;
                selected = value;
                this.OnPropertyChanged("EntryIsFocused");
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

        public bool layerLoadLoginIsHidden = false;
        public bool LayerLoadLoginIsHidden
        {
            get => layerLoadLoginIsHidden;
            set
            {
                if (layerLoadLoginIsHidden == value) return;
                layerLoadLoginIsHidden = value;
                this.OnPropertyChanged("LayerLoadLoginIsHidden");
            }
        }

        public bool layerBaseLoginIsHidden = true;
        public bool LayerBaseLoginIsHidden
        {
            get => layerBaseLoginIsHidden;
            set
            {
                if (layerBaseLoginIsHidden == value) return;
                layerBaseLoginIsHidden = value;
                this.OnPropertyChanged("LayerBaseLoginIsHidden");
            }
        }

        public LoginPageVM()
        {

        }

        public LoginPageVM(INavigation navigation)
        {
            this.navigation = navigation; 
            this.getInforAccount();

            this.createAccount = new Command(async () => await this.naviCreateAccount());
            this.exitApp = new Command(() => this.naviExit());
            this.sendLogin = new Command(async () => await this.sendLoginPageAsync());

        }

        private async Task naviCreateAccount() {
            if (this.EntryIsFocused)
            {
                this.EntryIsFocused = false;
            }
            await this.navigation.PushAsync(new CreateAccountPageUI());
        } 

        private void naviExit() { 
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

        private async void getInforAccount()
        {
            var infor = await App.Database.GetPeopleAsync();
            var inforModel = new InformationAccountModel();

            foreach (var item in infor)
            {
                if(item.IdCard != null && item.IdCard != "")
                {
                    inforModel = item;
                } 
            }

            if(inforModel.IdCard != null && inforModel.IdCard != "")
            {
                this.IDCard = inforModel.IdCard;
                this.PassAccount = inforModel.PassWord;
                this.IsCheckPass = true;
                this.isHaveInSqlite = true;
            }
        }

        private async Task sendLoginPageAsync()
        {
            try
            {
                if (this.IDCard != null && this.PassAccount != null)
                {
                    if (this.IDCard.Length == 12 || this.IDCard.Length == 9)
                    {
                        IsBusy = true;
                        LayerLoadLoginIsHidden = true;
                        var getLogin = await rest.LoginAPI(IDCard.ToString());
                        if (getLogin != null)
                        {
                            if (this.PassAccount == getLogin.MATKHAU_TK)
                            {
                                if (this.IsCheckPass)
                                {
                                    await App.Database.SavePersonAsync(new InformationAccountModel
                                    {
                                        IdCard = this.IDCard,
                                        PassWord = this.PassAccount,
                                        IsSaveAccount = this.IsCheckPass
                                    });
                                }
                                else
                                {
                                    if (this.isHaveInSqlite && !this.IsCheckPass)
                                    {
                                        await App.Database.DeleteItems(this.IDCard);
                                    }
                                    this.PassAccount = string.Empty;
                                    this.IDCard = string.Empty;
                                }

                                IsBusy = false;
                                LayerLoadLoginIsHidden = false;
                                LayerBaseLoginIsHidden = true;
                                await this.navigation.PushAsync(new HomePageUI());
                                UserData.shared.IDCard = this.IDCard;
                            }
                            else
                            {
                                this.showPopup("Kiểm tra lại mật khẩu");
                            }
                        }
                        else
                        {
                            this.showPopup("Tài khoản không tồn tại");
                        }
                    }
                    else
                    {
                        this.showPopup("Số CMND/căn cước không hợp lệ");
                    }
                }
                else
                {
                    this.showPopup("Mời bạn nhập đầy đủ thông tin");
                }
            }
            catch
            {
                LayerLoadLoginIsHidden = false;
                LayerBaseLoginIsHidden = true;
            }
        }

        private void showPopup(string contentMessage)
        {
            _ = App.Current.MainPage.DisplayAlert("Thông báo", contentMessage, "OK");
            IsBusy = false;
            LayerLoadLoginIsHidden = false;
            LayerBaseLoginIsHidden = true;
        }
    }
}
