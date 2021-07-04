using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NhaTroKH.extension;
using NhaTroKH.model;
using NhaTroKH.service;
using Xamarin.Forms;

namespace NhaTroKH.viewmodel
{ 
    public class RoomInformationChartPageVM : INotifyPropertyChanged
    {
        restAPI restApi;
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand BackButton { get; set; }
        public ICommand SeeInformationButton { get; set; }
        public INavigation navigation { get; set; }

        private string yearsValue = DateTime.Now.toDateWithFormat("yyyy");
        public string YearsValue
        {
            get => yearsValue;
            set
            {
                if (yearsValue == value) return;
                yearsValue = value;
                OnPropertyChanged("YearsValue");
            }
        }

        private string monthValue = DateTime.Now.toDateWithFormat("MM");
        public string MonthValue
        {
            get => monthValue;
            set
            {
                if (monthValue == value) return;
                monthValue = value;
                OnPropertyChanged("MonthValue");
            }
        }
        private int roomEntryValue = 0;
        public int RoomEntryValue
        {
            get => roomEntryValue;
            set
            {
                if (roomEntryValue == value) return;
                roomEntryValue = value;
                OnPropertyChanged("RoomEntryValue");
            }
        }
        private string amountRoom = "0";
        public string AmountRoom
        {
            get => amountRoom;
            set
            {
                if (amountRoom == value) return;
                amountRoom = value;
                OnPropertyChanged("AmountRoom");
            }
        }
        private string amountWater = "0";
        public string AmountWater
        {
            get => amountWater;
            set
            {
                if (amountWater == value) return;
                amountWater = value;
                OnPropertyChanged("AmountWater");
            }
        }
        private string amountElectic = "0";
        public string AmountElectic
        {
            get => amountElectic;
            set
            {
                if (amountElectic == value) return;
                amountElectic = value;
                OnPropertyChanged("AmountElectic");
            }
        }
        private string numMotoBikeValue = "0";
        public string NumMotoBikeValue
        {
            get => numMotoBikeValue;
            set
            {
                if (numMotoBikeValue == value) return;
                numMotoBikeValue = value;
                OnPropertyChanged("NumMotoBikeValue");
            }
        }
        private string numBikeValue = "0";
        public string NumBikeValue
        {
            get => numBikeValue;
            set
            {
                if (numBikeValue == value) return;
                numBikeValue = value;
                OnPropertyChanged("NumBikeValue");
            }
        }
        private string amountService = "0";
        public string AmountService
        {
            get => amountService;
            set
            {
                if (amountService == value) return;
                amountService = value;
                OnPropertyChanged("AmountService");
            }
        }
        private string totalAmount = "0";
        public string TotalAmount
        {
            get => totalAmount;
            set
            {
                if (totalAmount == value) return;
                totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        private bool isVisibleList = false;
        public bool IsVisibleList
        {
            get => isVisibleList;
            set
            {
                if (isVisibleList == value) return;
                isVisibleList = value;
                OnPropertyChanged("IsVisibleList");
            }
        }


        /// <summary>
        ///  datasource to ListView
        /// </summary>
        private ObservableCollection<serviceDetailModel> itemSourceList = null;
        public ObservableCollection<serviceDetailModel> ItemSourceList
        {
            get => itemSourceList;
            set
            {
                if (itemSourceList == value) return;
                itemSourceList = value;
                OnPropertyChanged("ItemSourceList");
            }
        }

        /// <summary>
        /// loadding layout
        /// </summary> 
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

        public RoomInformationChartPageVM()
        { }

        public RoomInformationChartPageVM(INavigation navigation)
        {
            this.navigation = navigation;
            this.restApi = new restAPI();

            this.BackButton = new Command(() => this.backAction());
            this.SeeInformationButton = new Command(async () => await this.seeInformationAction());
        }

        private async Task seeInformationAction()
        {
            this.showLoadding();
            if (isValidateOption())
            {
                var inforRoom = await this.restApi.getInforRoom(
                    years: this.yearsValue,
                    month: this.monthValue,
                    idCard: UserData.shared.IDCard,
                    numRoom: this.roomEntryValue.ToString());
                if(inforRoom != null)
                {   
                    Console.WriteLine(inforRoom);
                    this.AmountElectic = inforRoom.tienDien.toAmountVN();
                    this.AmountService = inforRoom.tienDichVu.toAmountVN();
                    this.AmountWater = inforRoom.tienNuoc.toAmountVN();
                    this.TotalAmount = inforRoom.tongTien.toAmountVN();
                    this.NumBikeValue = inforRoom.tienXeTayGa.toAmountVN();
                    this.NumMotoBikeValue = inforRoom.tienXeSo.toAmountVN();

                    if(inforRoom.chitietdichvu != null && inforRoom.chitietdichvu != "")
                    {
                        var detailListService = inforRoom.chitietdichvu.Trim();
                        if (detailListService != null && !detailListService.isNullOrEmpty())
                        {
                            string[] array = detailListService.Split(',');
                            if (array.Length > 0)
                            {
                                this.IsVisibleList = true;
                                this.ItemSourceList = new ObservableCollection<serviceDetailModel>();
                                foreach (string item in array)
                                {
                                    this.ItemSourceList.Add(new serviceDetailModel(item));
                                }
                            }
                        }
                    }
                    else
                    {

                        this.IsVisibleList = false;
                    }
                    this.showPopup("Mời bản kiểm tra thông tin phòng");
                }
                else
                {
                    this.showPopup("Hiện tại phòng chưa có thông tin nào!");
                }
            }
            else
            {
                this.showPopup("Bạn nhập số phòng chưa hợp lệ!");
            }
        }

        private void backAction()
        {
            this.navigation.PopAsync();
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
            if (RoomEntryValue != 0)
            {
                if (!RoomEntryValue.ToString().isNullOrEmpty())
                {
                    return true;
                }
            }
            return false;
        }
    }
}

public class serviceDetailModel
{
    public serviceDetailModel(string detail)
    {
        detaiItem = detail;
    }
    public string detaiItem { get; set; }
}
