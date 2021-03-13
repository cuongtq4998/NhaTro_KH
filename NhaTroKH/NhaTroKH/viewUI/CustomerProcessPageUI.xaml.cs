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
    public partial class CustomerProcessPageUI : ContentPage
    {
        public CustomerProcessPageUI()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            getquatrinh(LoginPageUI.SOCMND);
            lst.ItemSelected += Lst_ItemSelected1;

            DENNGAY_.DateSelected += (senderr, er) =>
            {
                dateDenNgay = DENNGAY_.Date;
            };
            TUNGAY_.DateSelected += (senderr, er) =>
            {
                dateTuNgay = TUNGAY_.Date;
            };

            JobSiteLabelNavigatePage.Text = this.jobSiteLabelNavigatePage;
            AddressSiteLabelNavigatePage.Text = this.addressSiteLabelNavigatePage;
        }

        private int IDThongTinKhachHangGD = 0;
        private DateTime dateLastIndex = new DateTime();

        private DateTime dateTuNgay = DateTime.Now;
        private DateTime dateDenNgay = DateTime.Now;

        string CMND_ = LoginPageUI.SOCMND;
        private string jobSiteLabelNavigatePage = KeyCustomerViewEnumeration.CustomerProcessPlaceholder_JobSite;
        private string addressSiteLabelNavigatePage = KeyCustomerViewEnumeration.CustomerProcessPlaceholder_Current;

        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "mJh3ttHegM5JEw4I8KKbreCWxcmVjIrcM5I9fhjx",
            BasePath = "https://nhatro-271ce.firebaseio.com/"

        };

        ObservableCollection<listquatrinh> employees = new ObservableCollection<listquatrinh>();
        public ObservableCollection<listquatrinh> Employees { get { return employees; } }
        IFirebaseClient client;
         

        private void Lst_ItemSelected1(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var foo = e.SelectedItem as listquatrinh;
            if (foo == null) return;

            check(foo);
        }

        async void check(listquatrinh foo)
        {
            string action1 = await DisplayActionSheet("Chọn thao tác của bạn?", "Huỷ", null, "Chỉnh Sửa", "Xoá");
            Debug.WriteLine("Action: " + action1);
            if (action1 == "Xoá")
            {
                getthongtincuoi(CMND_, foo.ID);
            }
            if (action1 == "Chỉnh Sửa")
            {
                editThongTin(CMND_, foo);
                getquatrinh(LoginPageUI.SOCMND);
            }
            if (action1 == "Huỷ")
            {
                getquatrinh(LoginPageUI.SOCMND);
            }
        }

        public async void editThongTin(string CMND, listquatrinh list)
        {
            tabView.SelectedItem = tabView.Items[0];
            Debug.WriteLine(list.ID);
            FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + list.ID + "/");
            KHACH_THUE_QUA_TRINH GD = tk.ResultAs<KHACH_THUE_QUA_TRINH>();
            Debug.WriteLine(GD.NGHENGHIEP);
            try
            {
                if (GD != null)
                {
                    IDThongTinKhachHangGD = GD.ID;
                    NGHENGHIEP_.Text = GD.NGHENGHIEP;
                    AddressSiteLabelNavigatePage.Text = GD.CHO_O;
                    TUNGAY_.Date = GD.TUTHANGNAM.Value;
                    DENNGAY_.Date = GD.DENTHANGNAM.Value;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessUserCurrentSite))
                {
                    AddressSiteLabelNavigatePage.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessUserCurrentSite].ToString();
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessUserJobSite))
                {
                    JobSiteLabelNavigatePage.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessUserJobSite].ToString();
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessDBJob))
                {
                    NGHENGHIEP_.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessDBJob].ToString();
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessDBDateTuNgay))
                {
                    TUNGAY_.Date = Convert.ToDateTime(Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessDBDateTuNgay));
                }
            }
            catch { }

            try
            {
                if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessDBDateDenNgay))
                {
                    DENNGAY_.Date = Convert.ToDateTime(Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessDBDateDenNgay));
                }
            }
            catch { }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RemoveKey(
                new List<string>
                {
                    KeyCustomerViewEnumeration.CustomerProcessUserCurrentSite,
                    KeyCustomerViewEnumeration.CustomerProcessUserJobSite,
                    KeyCustomerViewEnumeration.CustomerProcessDBJob,
                    KeyCustomerViewEnumeration.CustomerProcessDBDateTuNgay,
                    KeyCustomerViewEnumeration.CustomerProcessDBDateDenNgay,
                });
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


        public async void getthongtincuoi(string CMND, int vitrivuaxoa)
        {
            try
            {
                var action = await DisplayAlert("Thông báo", "Bạn có chắc xóa thông tin này chứ?", "Đồng Ý", "Hủy");
                if (action)
                {

                    if (employees.Count == 1) //Nếu chỉ còn 1 phần tử  thì xóa ngay
                    {
                        FirebaseResponse xoahet = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + vitrivuaxoa + "/");
                        getquatrinh(CMND_);
                    }
                    else // Ngược lại
                    {
                        //Tiến hành lấy vị trí cuối
                        var mangvitricuoi = new List<listquatrinh>();
                        client = new FireSharp.FirebaseClient(config);
                        int i = 0;
                        bool check = true;
                        while (check)
                        {
                            i++;
                            try
                            {
                                FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + i + "/");
                                if (tk.Body == "null")
                                {
                                    //Tiến hành duyệt mảng đến null thì vào đây làm bước tiếp theo  
                                    int cuoi = i - 1;
                                    FirebaseResponse tk1 = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + cuoi + "/");
                                    KHACH_THUE_QUA_TRINH QT = tk1.ResultAs<KHACH_THUE_QUA_TRINH>();
                                    mangvitricuoi.Add(
                                        new listquatrinh(
                                            QT.ID,
                                            QT.NGHENGHIEP,
                                            QT.NOILAMVIEC,
                                            QT.CHO_O,
                                            QT.TUTHANGNAM,
                                            QT.DENTHANGNAM
                                            )
                                        );// Lưu thông tin cuối mảng vào 1 mảng
                                          //Tiến hành hành động xóa 
                                    if (vitrivuaxoa == cuoi) //Nếu vị trí vừa chọn là cuối thì tiến hành xóa ngay
                                    {
                                        FirebaseResponse xoahet = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + vitrivuaxoa + "/");
                                    }
                                    else //Nếu vị trí vừa chọn là ngẫu nhiên thì tiến hành xóa vị trí hiện tại và xóa vị trí cuối
                                    {
                                        FirebaseResponse xoa = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + vitrivuaxoa + "/");
                                        FirebaseResponse xoacuoi = await client.DeleteAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + cuoi + "/");
                                        //Gán giá trị lưu ở mảng trên vào vị trí vừa xóa
                                        foreach (var item in mangvitricuoi)
                                        {
                                            var data = new KHACH_THUE_QUA_TRINH
                                            {
                                                ID = vitrivuaxoa,
                                                CHO_O = item.choo,
                                                TUTHANGNAM = item.tungay,
                                                DENTHANGNAM = item.denngay,
                                                NGHENGHIEP = item.nghenghiep,
                                                NOILAMVIEC = item.noilamviec
                                            };
                                            SetResponse response = await client.SetAsync("khachthue/" + CMND_ + "/KHACHTHUEQUATRINH/" + vitrivuaxoa + "/", data);
                                        }
                                    }
                                    //Cuối cũng tiến hành clear mảng ==> gét thông tin về ==> thoát While
                                    employees.Clear();
                                    getquatrinh(CMND_);
                                    check = false;
                                }
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    getquatrinh(LoginPageUI.SOCMND);
                }
            }
            catch (Exception e) { }
        }

        public bool checknull()
        {
            if (this.AddressSiteLabelNavigatePage.Text == this.addressSiteLabelNavigatePage
                || this.JobSiteLabelNavigatePage.Text == this.jobSiteLabelNavigatePage
                || string.IsNullOrWhiteSpace(NGHENGHIEP_.Text))
            {
                _ = DisplayAlert("Thông báo", "Mời bạn nhập đầy đủ thông tin!", "OK");
                checkFocus();
                return false;
            }
            else return true;

        }

        public void checkFocus()
        {
            if (NGHENGHIEP_.Text == string.Empty || NGHENGHIEP_.Text == null)
            {
                NGHENGHIEP_.Focus();
            }
        }

        public async Task sendquatrinhAsync()
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            int dem = 0;
            int i = 1;
            bool co = true;
            while (co)
            {
                try
                {
                    FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND_ + "/KHACHTHUEQUATRINH/" + i.ToString() + "/");
                    if (tk.Body == "null")
                    {
                        co = false;
                        break;
                    }
                    i++;
                    dem++;

                }
                catch
                {
                    co = false;
                }
            }

            try
            {
                if (checknull())
                {
                    var data = new KHACH_THUE_QUA_TRINH
                    {
                        ID = (IDThongTinKhachHangGD == 0) ? (dem + 1) : IDThongTinKhachHangGD,
                        CHO_O = AddressSiteLabelNavigatePage.Text,
                        NOILAMVIEC = JobSiteLabelNavigatePage.Text,
                        TUTHANGNAM = TUNGAY_.Date,
                        DENTHANGNAM = DENNGAY_.Date,
                        NGHENGHIEP = NGHENGHIEP_.Text
                    };
                    SetResponse response = await client.SetAsync("khachthue/" + CMND_ + "/KHACHTHUEQUATRINH/" + ((IDThongTinKhachHangGD == 0) ? (dem + 1) : IDThongTinKhachHangGD).ToString(), data);
                    KHACH_THUE_QUA_TRINH data1 = response.ResultAs<KHACH_THUE_QUA_TRINH>();

                    getquatrinh(CMND_);
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
            catch
            {
                _ = DisplayAlert("Thông báo", "Thêm thất bại", "OK");
            }
        }


        private void btnlammoi_Clicked(object sender, EventArgs e)
        {
            setmoi();
        }

        public async void getquatrinh(string CMND)
        {
            try
            {
                var mang1 = new List<listquatrinh>();
                int i = 0;
                bool dem = true;
                while (dem)
                {
                    i++;
                    try
                    {
                        FirebaseResponse tk = await client.GetAsync("khachthue/" + CMND + "/KHACHTHUEQUATRINH/" + i + "/");
                        KHACH_THUE_QUA_TRINH QT = tk.ResultAs<KHACH_THUE_QUA_TRINH>();
                        mang1.Add(
                            new listquatrinh(
                                QT.ID,
                                QT.NGHENGHIEP,
                                QT.NOILAMVIEC,
                                QT.CHO_O,
                                QT.TUTHANGNAM,
                                QT.DENTHANGNAM
                                )
                            );
                    }
                    catch
                    {
                        break;
                    }

                }
                if (employees.Count > 0)
                {
                    employees.Clear();
                }
                for (int ii = 0; ii < mang1.Count; ii++)
                {
                    lst.ItemsSource = employees;
                    employees.Add(new listquatrinh(
                        mang1[ii].ID,
                        mang1[ii].nghenghiep,
                        mang1[ii].noilamviec,
                        mang1[ii].choo,
                        mang1[ii].tungay,
                        mang1[ii].denngay
                        ));
                }

                // get Date Last Index
                if (employees.Count > 0)
                {
                    this.dateLastIndex = (DateTime)employees[employees.Count - 1].denngay;
                    var getAddress = employees[employees.Count - 1].choo;
                    this.TUNGAY_.Date = this.dateLastIndex;
                    this.DENNGAY_.Date = dateLastIndex.Date.AddDays(6);
                    this.AddressSiteLabelNavigatePage.Text = getAddress;
                }
                else
                {
                    TUNGAY_.Date = DateTime.Now;
                    DENNGAY_.Date = DateTime.Now;
                }

                try
                {
                    if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessDBDateTuNgay))
                    {
                        TUNGAY_.Date = Convert.ToDateTime(Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessDBDateTuNgay]);
                    }
                }
                catch { }

                try
                {
                    if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerProcessDBDateDenNgay))
                    {
                        DENNGAY_.Date = Convert.ToDateTime(Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessDBDateDenNgay]);
                    }
                }
                catch { }
            }
            catch (Exception e)
            { }
        }

        public void setmoi()
        {
            AddressSiteLabelNavigatePage.Text = addressSiteLabelNavigatePage;
            JobSiteLabelNavigatePage.Text = jobSiteLabelNavigatePage;
            NGHENGHIEP_.Text = string.Empty;
            TUNGAY_.Date = DateTime.Now;
            DENNGAY_.Date = DateTime.Now;
            Application.Current.Properties.Clear();
        }

        private void Themmoi_Clicked_1(object sender, EventArgs e)
        {
            if (TUNGAY_.Date < DENNGAY_.Date)
            {
                _ = sendquatrinhAsync();
                //  lênh này dùng để clear hết toàn bộ key đã lưu -> giảm dung lượng app
                Application.Current.Properties.Clear();
            }
            else
            {
                DisplayAlert("Thông báo", "(Từ ngày) phải nhỏ hơn (Đến ngày)", "OK");
            }

        }

        void AddressSite_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SaveKey();
                Navigation.PushAsync(new AddAddressPageUI(
                    ETypePageAddAddress.EProcessUser,
                    (int)EProcessUser.Hometown
                    ));
            });
        }

        void JobSite_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SaveKey();
                Navigation.PushAsync(new AddAddressPageUI(
                    ETypePageAddAddress.EProcessUser,
                    (int)EProcessUser.JobSite
                    ));
            });
        }

        void HomeProcess_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        public void SaveKey()
        {
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessDBJob] = NGHENGHIEP_.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessDBDateTuNgay] = dateTuNgay;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerProcessDBDateDenNgay] = dateDenNgay;
            Application.Current.SavePropertiesAsync();
        }
    }
}

public class listquatrinh1
{

    private string nghenghiep;
    public string Nghenghiep
    {
        get { return nghenghiep; }
        set { nghenghiep = value; }
    }
    private string noilamviec;
    public string Noilamviec
    {
        get { return noilamviec; }
        set { noilamviec = value; }
    }
    private string choo;
    public string Choo
    {
        get { return choo; }
        set { choo = value; }
    }
}
