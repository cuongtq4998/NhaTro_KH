using System;
using System.Collections.Generic;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.DB;
using NhaTroKH.Model;
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewUI
{
    public partial class AddAddressPageUI : ContentPage
    {
        
        public AddAddressPageUI(ETypePageAddAddress eTypePage, int inforPositon)
        {
            InitializeComponent();
            //khoi tao fire client
            firebaseClient = new FireSharp.FirebaseClient(config);
            //khoi tao picker thanh pho
            ProvincialPicker.ItemsSource = provincialList;
            AddAddressButton.IsEnabled = false;

            DisttrictPicker.IsEnabled = false;
            WardPicker.IsEnabled = false;
            StreesEntry.IsEnabled = false;
            ViewInputFromKeyboard.IsVisible = false;

            // recive type page and thông tin page
            this.eTypePage = eTypePage;
            this.inforPositon = inforPositon;
        }
        // Khoi tao page
        //CustomerProcess customerProcess = new CustomerProcess();

        //config 
        static IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "mJh3ttHegM5JEw4I8KKbreCWxcmVjIrcM5I9fhjx",
            BasePath = "https://nhatro-271ce.firebaseio.com/"

        };

        //property
        IFirebaseClient firebaseClient;
        List<string> wardList = new List<string>();
        List<string> districtList = new List<string>();
        private string provincialSelect = "";
        private string districtValue = "";
        private string wardValue = "";
        private bool flagCheckHidden = true;
        private ETypePageAddAddress eTypePage;
        private int inforPositon;
        private string[] listInforAddress = { "", "", "", "" };

        //danh sach tinh
        public static List<string> provincialList = new List<string>()
            {
                "Thành phố Hà Nội",
                "Tỉnh Hà Giang",
                "Tỉnh Cao Bằng",
                "Tỉnh Bắc Cạn",
                "Tỉnh Tuyên Quang",
                "Tỉnh Lào Cai",
                "Tỉnh Điện Biên",
                "Tỉnh Lai Châu",
                "Tỉnh Sơn La",
                "Tỉnh Yên Bái",
                "Tỉnh Hoà Bình",
                "Tỉnh Thái Nguyên",
                "Tỉnh Lạng Sơn",
                "Tỉnh Quảng Ninh",
                "Tỉnh Bắc Giang",
                "Tỉnh Phú Thọ",
                "Tỉnh Vĩnh Phúc",
                "Tỉnh Bắc Ninh",
                "Tỉnh Hải Dương",
                "Thành phố Hải Phòng",
                "Tỉnh Hưng Yên",
                "Tỉnh Thái Bình",
                "Tỉnh Hà Nam",
                "Tỉnh Nam Định",
                "Tỉnh Ninh Bình",
                "Tỉnh Thanh Hóa",
                "Tỉnh Nghệ An",
                "Tỉnh Hà Tĩnh",
                "Tỉnh Quảng Bình",
                 "Tỉnh Quảng Trị",
                "Tỉnh Thừa Thiên Huế",
                "Thành Phố Đà Nẵng",
                "Tỉnh Quảng Nam",
                "Tỉnh Quảng Ngãi",
                "Tỉnh Bình Định",
                 "Tỉnh Phú Yên",
                "Tỉnh Khánh Hòa",
                "Tỉnh Ninh Thuận",
                "Tỉnh Bình Thuận",
                "Tỉnh Kon Tum",
                "Tỉnh Gia Lai",
                "Tỉnh Đắk Lắk",
                "Tỉnh Đắk Nông",
                "Tỉnh Lâm Đồng",
                "Tỉnh Bình Phước",
                "Tỉnh Tây Ninh",
                "Tỉnh Bình Dương",
                 "Tỉnh Đồng Nai",
                 "Tỉnh Bà Rịa - Vũng Tàu",
                "Thành phố Hồ Chí Minh",
                "Tỉnh Long An",
                "Tỉnh Tiền Giang",
                "Tỉnh Bến Tre",
                "Tỉnh Trà Vinh",
                "Tỉnh Đồng Tháp",
                "Tỉnh Vĩnh Long",
                "Tỉnh An Giang",
                "Tỉnh Kiên Giang",
                "Thành phố Cần Thơ",
                 "Tỉnh Hậu Giang",
                "Tỉnh Sóc Trăng",
                "Tỉnh Bạc Liêu",
                "Tỉnh Cà Mau"
            };
        public static int BOOL(string chuoi)
        {
            int i;
            for (i = 0; i < provincialList.Count; i++)
            {
                if (chuoi == provincialList[i].ToString())
                {
                    break;
                }
            }
            return i + 1;

        }

        


        void AddAddressButton_Clicked(System.Object sender, System.EventArgs e)
        {
            // Một số vấn đề có thể xảy ra -----:
            // 1.Trả về dữ liệu về page nào, chỗ nào trên view
            // vì trên view có thể có nhiều chỗ cần nhập địa chỉ
            // Giải quyết:

            // 2.Thêm từ view picker chọn hay từ view nhập
            // Giải quyết:

            // 3.Hiện thị thông tin địa chỉ đã chọn vào view picker và view nhập( Có thể làm sau.)
            var getTextAddressFromUI = (valueUser.Text != null) ? valueUser.Text : "Chưa có dữ liệu địa chỉ!";

            // check neu la page thong tin gia dinh
            if (this.eTypePage == ETypePageAddAddress.EFamilyRelative)
            {
                Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyCustomerFamily] = getTextAddressFromUI;
                Application.Current.SavePropertiesAsync();
                Navigation.PopAsync();
            }

            // check neu la page qua trinh ban than
            if (this.eTypePage == ETypePageAddAddress.EProcessUser)
            {

                if (this.inforPositon == (int)EProcessUser.Hometown)
                {
                    Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessUserCurrentSite] = getTextAddressFromUI;
                }
                if (this.inforPositon == (int)EProcessUser.JobSite)
                {
                    Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessUserJobSite] = getTextAddressFromUI;
                }
                Application.Current.SavePropertiesAsync();
                Navigation.PopAsync();
            }

            // check neu la page thong tin khach hang
            if (this.eTypePage == ETypePageAddAddress.EInforCustomer)
            {
                switch (this.inforPositon)
                {
                    case (int)EInforCustomer.Hometown:
                        Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforHometown] = getTextAddressFromUI;
                        break;
                    case (int)EInforCustomer.JobSite:
                        Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforJobSite] = getTextAddressFromUI;
                        break;
                    case (int)EInforCustomer.CurrentSite:
                        Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCurrentSite] = getTextAddressFromUI;
                        break;
                    case (int)EInforCustomer.ResidentSite:
                        Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforResidentSite] = getTextAddressFromUI;
                        break;
                }
                Application.Current.SavePropertiesAsync();
                Navigation.PopAsync();
            }

            if (this.eTypePage == ETypePageAddAddress.EDefaultAddress)
            {
                Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress] = getTextAddressFromUI;
                Application.Current.SavePropertiesAsync();
                Navigation.PopAsync();
            }
        }

        #region ADDRESS GET VALUE 
        // get data from firebase
        static public int indexDistrict = 0;
        // get quan huyen
        private async void getDistrict()
        {
            int runDistrict = 0;
            bool checkRun = true;
            DisttrictPicker.IsEnabled = false;
            WardPicker.IsEnabled = false;
            StreesEntry.IsEnabled = false;
            LoadingDistrict.IsRunning = true;
            while (checkRun)
            {
                try
                {
                    runDistrict++;
                    IFirebaseClient clientKT = new FireSharp.FirebaseClient(config);
                    FirebaseResponse districtRespone = await clientKT.GetAsync("TinhThanh/" + BOOL(provincialSelect) + "/QuanHuyen/" + runDistrict + "/");
                    QuanHuyen districtResult = districtRespone.ResultAs<QuanHuyen>();
                    if (districtRespone.Body == "null")
                    {
                        checkRun = false;
                        LoadingDistrict.IsRunning = false;
                        DisttrictPicker.IsEnabled = true;
                        break;
                    }
                    districtList.Add(districtResult.TenQuanHuyen);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.ToString());
                    break;
                }
            }
            DisttrictPicker.ItemsSource = districtList;
        }

        //get phuong xa
        int indexWard = 0;
        private async void getWard(string districValue)
        {
            WardPicker.IsEnabled = false;
            StreesEntry.IsEnabled = false;
            LoadingWard.IsRunning = true;
            bool checkRun = true;
            while (checkRun)
            {
                try
                {
                    indexWard++;
                    IFirebaseClient clientKT = new FireSharp.FirebaseClient(config);
                    FirebaseResponse wardRespone = await clientKT.GetAsync("TinhThanh/" + BOOL(provincialSelect) + "/QuanHuyen/" + indexWard + "/");
                    QuanHuyen _xa1 = wardRespone.ResultAs<QuanHuyen>();
                    if (districValue == _xa1.TenQuanHuyen)
                    {
                        bool kiemmtra2 = true;
                        int run = 0;
                        while (kiemmtra2)
                        {
                            try
                            {
                                run++;
                                FirebaseResponse wardResponeEntry = await firebaseClient.GetAsync("TinhThanh/" + BOOL(provincialSelect) + "/QuanHuyen/" + indexWard + "/XaPhuong/" + run + "/");
                                PhuongXa wardResult = wardResponeEntry.ResultAs<PhuongXa>();
                                if (wardResponeEntry.Body == "null")
                                {
                                    checkRun = false;
                                    kiemmtra2 = false;
                                    LoadingWard.IsRunning = false;
                                    WardPicker.IsEnabled = true;
                                    break;
                                }
                                wardList.Add(wardResult.TenXaPhuong);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.ToString());
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.ToString());
                    break;
                }
            }

            WardPicker.ItemsSource = wardList;

        }

        void ProvincialPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            // check picker huyện nếu khác null thì
            // clear danh sách huyện
            // set pick huyện null
            // index tinh bằng = 0
            if (DisttrictPicker.ItemsSource != null)
            {
                districtList.Clear();
                DisttrictPicker.ItemsSource = null;
            }

            // check nếu picker xa khác null thi
            // clear danh sách xã
            // set picker xa null
            // set index huyện = 0
            if (WardPicker.ItemsSource != null)
            {
                wardList.Clear();
                WardPicker.ItemsSource = null;
                indexDistrict = 0;
            }

            // get from sender data picker
            Picker pickerValue = sender as Picker;
            var selectedItem = pickerValue.SelectedItem;

            // conver data thành data Tỉnh
            // set text
            // check nếu picker tỉnh khác -1 thì get data huyện
            provincialSelect = (Convert.ToString(selectedItem));
            valueUser.Text = " , " + provincialSelect;
            if (ProvincialPicker.SelectedIndex != -1)
            {
                getDistrict();
            }
        }
        void DisttrictPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            // check nếu picker xa khác null thì
            // clear danh sách xã
            // set picker xa thành null
            // set index huyện thành 0
            if (WardPicker.ItemsSource != null)
            {
                wardList.Clear();
                WardPicker.ItemsSource = null;
                indexDistrict = 0;
            }

            // get data từ  sender picker
            Picker trinhdo = sender as Picker;
            var selectedItem = trinhdo.SelectedItem;

            // convert data sáng huyện
            // set text
            // check nếu pick xa khác -1 thì get xã truyền vào tham số huyện vừa convert
            districtValue = (Convert.ToString(selectedItem));
            valueUser.Text = " , " + districtValue + " , " + provincialSelect;
            if (DisttrictPicker.SelectedIndex != -1)
            {
                getWard(districtValue);
            }
        }
        void WardPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            // get data từ sender picker xa
            Picker trinhdo = sender as Picker;
            var selectedItem = trinhdo.SelectedItem;

            // convert data sang xa
            // check nếu picker xa có index khác -1 thì set text và hiện textbox nhập đường vào
            wardValue = (Convert.ToString(selectedItem));
            if (WardPicker.SelectedIndex != -1)
            {
                valueUser.Text = " , " + wardValue + " , " + districtValue + " , " + provincialSelect;
                StreesEntry.IsEnabled = true;
            }
        }
        void StreesEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            // set text
            // hiển thi button thêm địa chỉ
            valueUser.Text = StreesEntry.Text + " , " + wardValue + " , " + districtValue + " , " + provincialSelect;
            AddAddressButton.IsEnabled = true;
        }
        #endregion

        // nếu set TRUE -> view a hiện view  b ẩn
        // ngược lại FALSE -> view a ẩn view b hiện
        void setHidenView(bool hiden)
        {
            flagCheckHidden = !hiden;
            ViewInputFromKeyboard.IsVisible = hiden; // view a
            ViewChooseAddress.IsVisible = !hiden; // view b
        }

        void CheckTypeInput_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            setHidenView(flagCheckHidden);

            AddAddressButton.IsEnabled = (ProvincialEntry.Text == string.Empty ? true : false)
                && (DistrictEntry.Text == string.Empty ? true : false)
                && (StreesEntry.Text == string.Empty ? true : false);

            _ = provincialSelect != string.Empty ? (ProvincialEntry.Text = provincialSelect) : (ProvincialEntry.Text = string.Empty);
            _ = districtValue != string.Empty ? (DistrictEntry.Text = districtValue) : (DistrictEntry.Text = string.Empty);
            _ = wardValue != string.Empty ? (WardEntry.Text = wardValue) : (WardEntry.Text = string.Empty);
            _ = StreesEntry.Text != string.Empty ? (StreesEntryUser.Text = StreesEntry.Text) : (StreesEntryUser.Text = string.Empty);

        }

        void ProvincialEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var provincialEntry = (Entry)sender;
            string value = provincialEntry.Text;
            Console.WriteLine(value);
            listInforAddress[3] = value;
            valueUser.Text = String.Join(", ", listInforAddress);

            // check hiden button
            AddAddressButton.IsEnabled = (listInforAddress[0] != string.Empty ? true : false)
                && (listInforAddress[2] != string.Empty ? true : false)
                && (listInforAddress[3] != string.Empty ? true : false);
        }

        void DistrictEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var districtEntry = (Entry)sender;
            string value = districtEntry.Text;
            listInforAddress[2] = value;
            valueUser.Text = String.Join(", ", listInforAddress);

            // check hiden button
            AddAddressButton.IsEnabled = (listInforAddress[0] != string.Empty ? true : false)
                && (listInforAddress[2] != string.Empty ? true : false)
                && (listInforAddress[3] != string.Empty ? true : false);
        }

        void WardEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var wardEntry = (Entry)sender;
            string value = wardEntry.Text;
            listInforAddress[1] = value;
            valueUser.Text = String.Join(", ", listInforAddress);

            // check hiden button
            AddAddressButton.IsEnabled = (listInforAddress[0] != string.Empty ? true : false)
                && (listInforAddress[2] != string.Empty ? true : false)
                && (listInforAddress[3] != string.Empty ? true : false);
        }

        void StreesEntryUser_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var streesEntry = (Entry)sender;
            string value = streesEntry.Text;
            listInforAddress[0] = value;
            valueUser.Text = String.Join(", ", listInforAddress);

            // check hiden button
            AddAddressButton.IsEnabled = (listInforAddress[0] != string.Empty ? true : false)
                && (listInforAddress[2] != string.Empty ? true : false)
                && (listInforAddress[3] != string.Empty ? true : false);
        }
    }
}
