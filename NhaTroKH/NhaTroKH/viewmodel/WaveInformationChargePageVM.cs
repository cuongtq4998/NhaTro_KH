using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using NhaTroKH.service;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{
    public class WaveInformationChargePageVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        restAPI rest = new restAPI();

        public INavigation navigation { get; set; }
        private string _entryYear = DateTime.Now.Year.ToString();
        private int timeSeconds = 1; 
        public string EntryYear
        {
            get { return _entryYear; }
            set
            {
                _entryYear = value;
                TextChangedCommand.Execute(_entryYear);
                OnPropertyChanged();
            }
        }

        private string _entryMonth = DateTime.Now.Month.ToString(); 
        public string EntryMonth
        {
            get { return _entryMonth; }
            set
            {
                _entryMonth = value;
                MonthTextChangedCommand.Execute(_entryMonth);
                OnPropertyChanged();
            }
        }

        private string _entryRoom = null;
        public string EntryRoom
        {
            get { return _entryRoom; }
            set
            {
                _entryRoom = value;
                RoomTextChangedCommand.Execute(_entryRoom);
                OnPropertyChanged();
            }
        }

        private string _pagramElectric = null;
        public string PagramElectric
        {
            get => _pagramElectric;
            set
            {
                _pagramElectric = value;
                OnPropertyChanged();
            }
        }

        private string _pagramWater = null;
        public string PagramWater
        {
            get => _pagramWater;
            set
            {
                _pagramWater = value;
                OnPropertyChanged();
            }
        }

        private string _pagramMotobikeNumber = null;
        public string PagramMotobikeNumber
        {
            get => _pagramMotobikeNumber;
            set
            {
                _pagramMotobikeNumber = value;
                OnPropertyChanged();
            }
        }

        private string _pagramMotobike = null;
        public string PagramMotobike
        {
            get => _pagramMotobike;
            set
            {
                _pagramMotobike = value;
                OnPropertyChanged();
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

        public Command TextChangedCommand => new Command<string>(async (_entryYear) => await YearTextChanged(_entryYear));
        public Command MonthTextChangedCommand => new Command<string>(async (_entryMonth) => await MonthTextChanged(_entryMonth));
        public Command RoomTextChangedCommand => new Command<string>(async (_entryRoom) => await RoomTextChanged(_entryRoom));
        public ICommand BackCommand { get; set; }
        public ICommand AddAndLookInforCommand { get; set; } 

        public WaveInformationChargePageVM()
        {

        }

        public WaveInformationChargePageVM(INavigation navigation)
        {
            this.navigation = navigation;
            this.BackCommand = new Command( () =>  this.navBack());
            this.AddAndLookInforCommand = new Command(async () => await AddAndLookInforAcction());
        }

        private async Task YearTextChanged(string p)
        {
            if (!string.IsNullOrEmpty(_entryMonth) &&
                !string.IsNullOrEmpty(_entryRoom) &&
                !string.IsNullOrEmpty(_entryYear))
            {
                // look infor
                if(Convert.ToInt32(_entryYear) != 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(timeSeconds))
                    .ContinueWith(async task => {
                        await LookInforAction();
                    });
                } 
            }
        }

        private async Task MonthTextChanged(string p)
        {
            if (!string.IsNullOrEmpty(_entryYear) &&
                !string.IsNullOrEmpty(_entryRoom) &&
                !string.IsNullOrEmpty(_entryMonth))
            {
                // look infor
                if(Convert.ToInt32(_entryMonth) <= 12 &&
                    Convert.ToInt32(_entryMonth) != 0)
                {
                     await Task.Delay(TimeSpan.FromSeconds(timeSeconds))
                        .ContinueWith(async task => { 
                         await LookInforAction();
                     }); 
                } 
            }
        }

        private async Task RoomTextChanged(string p)
        {
            if (!string.IsNullOrEmpty(_entryMonth) &&
                !string.IsNullOrEmpty(_entryYear) &&
                !string.IsNullOrEmpty(_entryRoom))
            {
                // look infor
                if (Convert.ToInt32(_entryRoom) != 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(timeSeconds))
                        .ContinueWith(async task => {
                            await LookInforAction();
                        });
                } 
            }
        }

        private void navBack()
        {
            this.navigation.PopAsync();
        }

        /// <summary>
        /// insert and update infor electric, water
        /// </summary>
        /// <returns></returns>
        private async Task AddAndLookInforAcction()
        {
            LayerLoadLoginIsHidden = true;
            try
            {
                if (checknull())
                {
                    await Task.Run(async () => {
                        var chargeElectric = this.rest.InsertWaterInfor(
                            pagramElectric: _pagramElectric,
                             pagramWater: _pagramWater,
                             pagramMotobikeNumber: _pagramMotobikeNumber,
                             pagramMotobike: _pagramMotobike,
                             years: _entryYear,
                             month: _entryMonth,
                             room: _entryRoom
                             ); 
                        if (chargeElectric.Result)
                        {
                            LayerLoadLoginIsHidden = false;
                            LayerBaseLoginIsHidden = true;
                            await UserDialogs.Instance.AlertAsync("Ghi nhận thành công", "Thông báo", "OK");
                            return;
                        }
                        else
                        {
                            LayerLoadLoginIsHidden = false; 
                            return;
                        }
                    });
                }
                else
                {
                    LayerLoadLoginIsHidden = false;
                    await UserDialogs.Instance.AlertAsync("Mời bạn nhập đầy đủ thông tin!", "Thông báo", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Thất bại", "Thông báo", "OK");
                return;
            }
        }

        /// <summary>
        /// get infor electric, water
        /// </summary>
        /// <returns></returns>
        private async Task LookInforAction()
        {
            LayerLoadLoginIsHidden = true;
            await Task.Run(async () => {
                var getElectricWater = await this.rest.getWaterInfor(
                    years: _entryYear,
                    month: _entryMonth,
                    room: _entryRoom);

                if (getElectricWater != null)
                {
                    LayerLoadLoginIsHidden = false;
                    LayerBaseLoginIsHidden = true;
                    this.PagramElectric = getElectricWater.SO_DIEN;
                    this.PagramWater = getElectricWater.SO_NUOC;
                    this.PagramMotobike = getElectricWater.SO_XE_TAY_GA_KHACH;
                    this.PagramMotobikeNumber = getElectricWater.SO_XE_SO_KHACH;
                }
                else
                {
                    LayerLoadLoginIsHidden = false;
                    LayerBaseLoginIsHidden = true;
                    this.PagramElectric = "0";
                    this.PagramWater = "0";
                    this.PagramMotobike = "0";
                    this.PagramMotobikeNumber = "0";
                }
            }); 
        }

        public bool checknull()
        {
            if (!string.IsNullOrWhiteSpace(_pagramElectric)
                && !string.IsNullOrWhiteSpace(_pagramWater)
                && !string.IsNullOrWhiteSpace(_pagramMotobike)
                && !string.IsNullOrWhiteSpace(_pagramMotobikeNumber)
                && !string.IsNullOrWhiteSpace(_entryYear)
                && !string.IsNullOrWhiteSpace(_entryMonth)
                && !string.IsNullOrWhiteSpace(_entryRoom))
            {
                return true;
            }
            else return false;
        }
    }
}
