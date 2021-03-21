using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.DB;
using NhaTroKH.Model;
using NhaTroKH.Models;
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewUI
{
    public partial class CustomerFamilyPageUI : ContentPage
    {
        public CustomerFamilyPageUI()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            getgiadinh(CMND_);
            getAddressResident();
            lst.ItemSelected += Lst_ItemSelected;
            BIRTDATE_.DateSelected += (senderr, er) => {
                dateNgaySinh = BIRTDATE_.Date;
            };

            CLICKNAVIGATEPAGEADDADDRESS.Text = KeyCustomerViewEnumeration.CustomerFamilyPlaceholder_Current;

            SiteCurentFamilyButton.Clicked += async (sender, e) =>
            {

                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.DefaultAddress))
                {
                    string address = Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress].ToString();
                    bool dialog = await DisplayAlert("Thông báo", "Bạn sẽ chọn địa chỉ: " + address, "OK", "Không");
                    if (dialog)
                    {
                        CLICKNAVIGATEPAGEADDADDRESS.Text = Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress].ToString();
                    }
                }
                else { this.navigatePageSetting(); }
            };
        }


        static IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "mJh3ttHegM5JEw4I8KKbreCWxcmVjIrcM5I9fhjx",
            BasePath = "https://nhatro-271ce.firebaseio.com/"

        };
        private DateTime dateNgaySinh = DateTime.Now;

        private int IDThongTinKhachHangGD = 0;
        IFirebaseClient client;
        ObservableCollection<listgiadinh> employees = new ObservableCollection<listgiadinh>();
        public ObservableCollection<listgiadinh> Employees { get { return employees; } }
        string CMND_ = LoginPageUI.SOCMND;

        public string gioitinh = "";
        public string tinh = "";
        public string huyen = "";
        public string xa = "";
        public string quanhe;


        #region DELETELISTVIEW
        //[0]   LIST1
        //[1]   LIST2  ==>  Xóa vị trí này ==> Vị trí thứ [1] =>  vi trí i = 2
        //[2]   LIST3

        //Tạo mảng vitricuoi hứng thông tin cuối                        ==> OK
        //Chạy lấy vị trí cuối củng trước                               ==> OK
        //Add thông tin lấy cuối cùng vào class lưu trữ                 ==> OK            
        //Lấy vị trí phần tử vừa xóa                                    ==> 
        //Add thông tin luu ở class lưu trữ  vào trí trí vừa xóa        ==>  
        //Get thông tin về                                              ==> 


        private async void navigatePageSetting()
        {
            bool dialog = await DisplayAlert("Thông báo", "Bạn chưa có địa chỉ mặc định. Vào thiết lập thông tin ngay! ", "OK", "Không");
            if (dialog)
            {
                _ = Navigation.PushAsync(new SettingPageUI());
            }
        }

        private void Lst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var foo = e.SelectedItem as listgiadinh;
            if (foo == null) return;
            check(foo);
        }

        async void check(listgiadinh foo)
        {
            string action1 = await DisplayActionSheet("Chọn thao tác của bạn?", "Huỷ", null, "Chỉnh Sửa", "Xoá");

            if (action1 == "Xoá")
            {
                getthongtincuoi(CMND_, foo.id);
            }
            if (action1 == "Chỉnh Sửa")
            {
                editThongTin(CMND_, foo);
                getgiadinh(CMND_);
            }
            if (action1 == "Huỷ")
            {
                getgiadinh(CMND_);
            }
        }

        public async void getthongtincuoi(string CMND, int vitrivuaxoa)
        {
            var action = await DisplayAlert("Thông báo", "Bạn có chắc xóa thông tin này chứ?", "Đồng Ý", "Hủy");
            if (action)
            {

                if (employees.Count == 1) //Nếu chỉ còn 1 phần tử  thì xóa ngay
                {
                    FirebaseResponse xoahet = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + vitrivuaxoa + "/");
                    getgiadinh(CMND_);
                }
                else // Ngược lại
                {
                    //Tiến hành lấy vị trí cuối
                    var mangvitricuoi = new List<listgiadinh>();
                    int i = 0;
                    bool check = true;
                    while (check)
                    {
                        i++;
                        try
                        {
                            FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + i + "/");
                            if (tk.Body == "null")
                            {
                                //Tiến hành duyệt mảng đến null thì vào đây làm bước tiếp theo  
                                int cuoi = i - 1;
                                FirebaseResponse tk1 = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + cuoi + "/");
                                KHACH_THUE_GIA_DINH GD = tk1.ResultAs<KHACH_THUE_GIA_DINH>();
                                mangvitricuoi.Add(
                                    new listgiadinh(
                                        GD.ID,
                                        GD.HOTEN,
                                        GD.NGAYSINH,
                                        GD.GIOITINH,
                                        GD.QUANHE,
                                        GD.NGHENGHIEP,
                                        GD.DIACHICHOOHIENNAY
                                        )
                                    );// Lưu thông tin cuối mảng vào 1 mảng
                                //Tiến hành hành động xóa 
                                if (vitrivuaxoa == cuoi) //Nếu vị trí vừa chọn là cuối thì tiến hành xóa ngay
                                {
                                    FirebaseResponse xoahet = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + vitrivuaxoa + "/");
                                }
                                else //Nếu vị trí vừa chọn là ngẫu nhiên thì tiến hành xóa vị trí hiện tại và xóa vị trí cuối
                                {
                                    FirebaseResponse xoa = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + vitrivuaxoa + "/");
                                    FirebaseResponse xoacuoi = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + cuoi + "/");
                                    //Gán giá trị lưu ở mảng trên vào vị trí vừa xóa
                                    foreach (var item in mangvitricuoi)
                                    {
                                        var data = new KHACH_THUE_GIA_DINH
                                        {
                                            ID = vitrivuaxoa,
                                            HOTEN = item.hoten,
                                            GIOITINH = item.gioitinh,
                                            NGAYSINH = item.ngaysinh,
                                            DIACHICHOOHIENNAY = item.choo,
                                            NGHENGHIEP = item.nghenghiep,
                                            QUANHE = item.quanhe
                                        };
                                        SetResponse response = await client.SetAsync("khachthue/" + CMND_ + "/KHACHTHUEGIADINH/" + vitrivuaxoa + "/", data);
                                    }
                                }
                                //Cuối cũng tiến hành clear mảng ==> gét thông tin về ==> thoát While
                                employees.Clear();
                                getgiadinh(CMND_);
                                check = false;
                            }
                        }
                        catch { }
                    }
                }
            }
            else
            {
                getgiadinh(CMND_);
            }
        }

        public async void editThongTin(string CMND, listgiadinh list)
        {
            tabView.SelectedItem = tabView.Items[0];
            Debug.WriteLine(list.id);
            FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + list.id + "/");
            KHACH_THUE_GIA_DINH GD = tk.ResultAs<KHACH_THUE_GIA_DINH>();
            Debug.WriteLine(GD.HOTEN);

            if (GD != null)
            {
                IDThongTinKhachHangGD = GD.ID;
                NAME_.Text = GD.HOTEN;
                NGHENGHIEP_.Text = GD.NGHENGHIEP;
                BIRTDATE_.Date = GD.NGAYSINH != null ? GD.NGAYSINH.Value : DateTime.Now;

                //set Data picker
                for (int i = 0; i < Sex_.Items.Count; i++)
                {
                    try
                    {
                        if (GD.GIOITINH == Sex_.Items[i].ToString())
                        {
                            Sex_.SelectedIndex = i;
                        }
                    }
                    catch { Sex_.SelectedIndex = 0; }

                }

                for (int i = 0; i < QUANHE_.Items.Count; i++)
                {
                    try
                    {
                        if (GD.QUANHE == QUANHE_.Items[i].ToString())
                        {
                            QUANHE_.SelectedIndex = i;
                        }
                    }
                    catch { QUANHE_.SelectedIndex = 0; }

                }
                // Địa chỉ 
                CLICKNAVIGATEPAGEADDADDRESS.Text = GD.DIACHICHOOHIENNAY;


            }
        }

        #endregion 

        #region GET_AND_ADD_THONGTIN
        public async void getgiadinh(string CMND)
        {
            var manggiadinh = new List<listgiadinh>();
            int i = 0;
            bool dem = true;
            while (dem)
            {
                i++;
                try
                {
                    FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEGIADINH/" + i + "/");
                    KHACH_THUE_GIA_DINH GD = tk.ResultAs<KHACH_THUE_GIA_DINH>();
                    manggiadinh.Add(
                        new listgiadinh(
                            GD.ID,
                            GD.HOTEN,
                            GD.NGAYSINH,
                            GD.GIOITINH,
                            GD.QUANHE,
                            GD.NGHENGHIEP,
                            GD.DIACHICHOOHIENNAY
                            )
                        );
                }
                catch { break; }
            }
            if (employees.Count > 0)
            {
                employees.Clear();
            }
            for (int ii = 0; ii < manggiadinh.Count; ii++)
            {
                lst.ItemsSource = employees;
                employees.Add(new listgiadinh(
                    manggiadinh[ii].id,
                    manggiadinh[ii].hoten,
                    manggiadinh[ii].ngaysinh,
                    manggiadinh[ii].gioitinh,
                    manggiadinh[ii].quanhe,
                    manggiadinh[ii].nghenghiep,
                    manggiadinh[ii].choo
                    ));
            }
        }
        public async Task addkhachgiadinhAsync()
        {
            int dem = 0;
            int i = 1;
            bool co = true;
            while (co)
            {
                try
                {
                    FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND_ + "/KHACHTHUEGIADINH/" + i.ToString() + "/");
                    if (tk.Body == "null") { co = false; break; }
                    i++;
                    dem++;
                }
                catch { co = false; }
            }
            if (checknull())
            {
                var data = new KHACH_THUE_GIA_DINH
                {
                    ID = (IDThongTinKhachHangGD == 0) ? (dem + 1) : IDThongTinKhachHangGD,
                    HOTEN = NAME_.Text,
                    GIOITINH = gioitinh,
                    NGAYSINH = BIRTDATE_.Date,
                    DIACHICHOOHIENNAY = CLICKNAVIGATEPAGEADDADDRESS.Text,
                    NGHENGHIEP = NGHENGHIEP_.Text,
                    QUANHE = quanhe
                };
                SetResponse response = await client.SetAsync("khachthue/" + CMND_ + "/KHACHTHUEGIADINH/" + ((IDThongTinKhachHangGD == 0) ? (dem + 1) : IDThongTinKhachHangGD).ToString() + "/", data);
                KHACH_THUE_GIA_DINH data1 = response.ResultAs<KHACH_THUE_GIA_DINH>();

                getgiadinh(CMND_);
                if (IDThongTinKhachHangGD == 0)
                {
                    _ = DisplayAlert("Thông báo", "Thêm thành công", "OK");
                }
                else
                {
                    _ = DisplayAlert("Thông báo", "Chỉnh sửa thông tin thành công", "OK");
                }

            }
        }
        #endregion

        public bool checknull()
        {
            if (NAME_.Text == null
                || NGHENGHIEP_.Text == null
                || Sex_.SelectedIndex == -1
                || QUANHE_.SelectedIndex == -1
                || CLICKNAVIGATEPAGEADDADDRESS.Text == KeyCustomerViewEnumeration.CustomerFamilyPlaceholder_Current)
            {
                _ = DisplayAlert("Thông báo", "Mời bạn nhập đầy đủ thông tin!", "OK");
                checkFocus();
                return false;
            }
            else return true;
        }

        private void checkFocus()
        {
            List<Entry> listInforFamily = new List<Entry> {
                NAME_,
                NGHENGHIEP_
            };
            for (int i = 0; i <= listInforFamily.Count; i++)
            {
                if (listInforFamily[i].Text == null)
                {
                    listInforFamily[i].Focus();
                    break;
                }
            }
        }
        private void btnlammoi_Clicked(object sender, EventArgs e)
        {
            CLICKNAVIGATEPAGEADDADDRESS.Text = KeyCustomerViewEnumeration.CustomerFamilyPlaceholder_Current;
            NAME_.Text = string.Empty;
            NGHENGHIEP_.Text = string.Empty;
            BIRTDATE_.Date = DateTime.Now;
        }
        private void QUANHE__SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker QUANHE = sender as Picker;
            var selectedItem = QUANHE.SelectedItem;
            quanhe = Convert.ToString(selectedItem);
        }
        private void Sex__SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker sex = sender as Picker;
            var selectedItem = sex.SelectedItem;
            gioitinh = Convert.ToString(selectedItem);
        }
        private void Themmoi_Clicked(object sender, EventArgs e)
        {
            _ = addkhachgiadinhAsync();
            // clear data save when add
            Application.Current.Properties.Clear();

        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                SaveKey();
                Navigation.PushAsync(new AddAddressPageUI(
                ETypePageAddAddress.EFamilyRelative,
                (int)EFamilyRelative.Hometown
                ));
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RemoveKey(
                new List<string> {
                    KeyCustomerViewEnumeration.CustomerFamilyDBgioitinh,
                    KeyCustomerViewEnumeration.CustomerFamilyDBNAME_,
                    KeyCustomerViewEnumeration.CustomerFamilyDBDateNgaySinh,
                    KeyCustomerViewEnumeration.CustomerFamilyDBquanhe,
                    KeyCustomerViewEnumeration.CustomerFamilyCustomerFamily
                });
        }

        public void SaveKey()
        {
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBNAME_] = NAME_.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBgioitinh] = gioitinh;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBquanhe] = quanhe;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBDateNgaySinh] = dateNgaySinh;
            Application.Current.SavePropertiesAsync();
        }

        private async void RemoveKey(List<string> listKey)
        {
            try
            {
                foreach (var key in listKey)
                {
                    if (Application.Current.Properties.ContainsKey(key))
                    {
                        App.Current.Properties.Remove(key);
                    }
                }
                await App.Current.SavePropertiesAsync();
            }
            catch { }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ///
            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerFamilyDBgioitinh))
                {
                    for (int i = 0; i < Sex_.Items.Count; i++)
                    {
                        try
                        {
                            if (Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBgioitinh].ToString() == Sex_.Items[i].ToString())
                            {
                                Sex_.SelectedIndex = i;
                            }
                        }
                        catch { Sex_.SelectedIndex = 0; }
                    }
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerFamilyDBNAME_))
                {
                    NAME_.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBNAME_].ToString();
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerFamilyDBDateNgaySinh))
                {
                    BIRTDATE_.Date = Convert.ToDateTime(Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBDateNgaySinh]);
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerFamilyDBquanhe))
                {
                    for (int i = 0; i < QUANHE_.Items.Count; i++)
                    {
                        try
                        {
                            if (Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyDBquanhe].ToString() == QUANHE_.Items[i].ToString())
                            {
                                QUANHE_.SelectedIndex = i;
                            }
                        }
                        catch { QUANHE_.SelectedIndex = 0; }
                    }
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerFamilyCustomerFamily))
                {
                    CLICKNAVIGATEPAGEADDADDRESS.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyCustomerFamily].ToString();
                }
            }
            catch { }
        }

        void HomeFamily_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        private async void getAddressResident()
        {
            FirebaseResponse res = await client.GetAsync("khachthue/" + CMND_ + "/KHACHTHUE/");
            Data thongtinkhachthue = res.ResultAs<Data>();
            if (res.Body == "null")
            {
                return;
            }
            if (thongtinkhachthue.NOITHUONGTHU != "")
            {
                Application.Current.Properties[KeyCustomerViewEnumeration.CustomerFamilyPlaceholder_Current] = thongtinkhachthue.NOITHUONGTHU;
                CLICKNAVIGATEPAGEADDADDRESS.Text = thongtinkhachthue.NOITHUONGTHU;
                _ = Application.Current.SavePropertiesAsync();
            }
        }
    }
}

class DataAddress
{
    public string Provincial;
    public string District;
    public string Ward;
    public string Strees;

    public DataAddress(string Provincial, string District, string Ward, string Strees)
    {
        this.Provincial = Provincial;
        this.District = District;
        this.Ward = Ward;
        this.Strees = Strees;
    }

}