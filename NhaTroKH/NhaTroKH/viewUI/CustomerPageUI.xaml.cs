using System;
using System.Collections.Generic;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.DB;
using NhaTroKH.model;
using NhaTroKH.Models;
using NhaTroKH.Service;
using NhaTroKH.viewmodel;
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewUI
{
    public partial class CustomerPageUI : ContentPage
    {
        AddressSettingModel addressSetting;

        Iconfig config = new Iconfig();
        IFirebaseClient client;

        static public string CMND_ = "";
        string gioitinh = "";
        string trinhdochuyenmon = "";
        string biensoxe = "";
        private bool isResident, isAccomodation, isCurrentSite, isJobSite = false;

        // default dân tộc "Kinh" và tôn giáo "Không"
        private string _dantoc = "Kinh";
        private string _tongiao = "Không";


        private string _customerInforPlaceholder_JobSite = KeyCustomerViewEnumeration.CustomerInforPlaceholder_JobSite;
        private string _customerInforPlaceholder_CurentSite = KeyCustomerViewEnumeration.CustomerInforPlaceholder_CurentSite;
        private string _customerInforPlaceholder_Resident = KeyCustomerViewEnumeration.CustomerInforPlaceholder_Resident;
        private string _customerInforPlaceholder_Hometown = KeyCustomerViewEnumeration.CustomerInforPlaceholder_Hometown;


        public CustomerPageUI()
        {
            InitializeComponent();
            BindingContext = new CustomerPageVM(Navigation);
             
            client = new FireSharp.FirebaseClient(config.config);

            CMND.Text = UserData.shared.IDCard;

            CMND_ = CMND.Text;
            dantoc.Text = _dantoc;
            tongiao.Text = _tongiao;
            addpickerhocvan();
            addgioitinh();
            addloaixe();
            getkhachthue();

            this.handleEvent(); 
        }
        
        private void handleEvent()
        {
            chooseResidentButton.Clicked += (sender, e) =>
            { 
                this.showList(ValueIsAddress.isResident);
            };

            HometownButton.Clicked += (sender, e) =>
            { 
                this.showList(ValueIsAddress.isAccomodation);
            };

            SiteCurentButton.Clicked += (sender, e) =>
            { 
                this.showList(ValueIsAddress.isCurrentSite);
            };

            ChooseJobsiteButton.Clicked += (sender, e) =>
            {
                this.showList(ValueIsAddress.isJobSite);
            };

            cancelPopupButton.Clicked += (sender, e) =>
            {
                var viewModel = BindingContext as CustomerPageVM;
                for (int i = 0; i < viewModel.Items.Count; i++)
                {
                    if (viewModel.Items[i].IsSelected)
                    {
                        viewModel.Items[i].IsSelected = false;
                    }
                }

                viewListChoose.IsVisible = false;
                if (addressSetting != null)
                {
                    addressSetting.IsSelected = false;
                }
            };

            confirmPopupButton.Clicked += (sender, e) => {
                var viewModel = BindingContext as CustomerPageVM;
                if(viewModel.Items.Count > 0)
                { 
                    for (int i = 0; i < viewModel.Items.Count; i++)
                    {
                        AddressSettingModel vm = viewModel.Items[i];
                        if (vm.IsSelected)
                        {  
                            if (isAccomodation)
                            {
                                HometownLabelNavigatePage.Text = vm.TitleAddress;
                            }
                            if (isCurrentSite)
                            {
                                SiteCurentLabelNavigatePage.Text = vm.TitleAddress;
                            }
                            if (isResident)
                            {
                                AddressResidentLabelNavigatePage.Text = vm.TitleAddress;
                            }
                            if (isJobSite)
                            {
                                PlaceJobLabelNavigatePage.Text = vm.TitleAddress;
                            }
                        }
                    }

                    for (int i = 0; i < viewModel.Items.Count; i++)
                    {
                        if (viewModel.Items[i].IsSelected)
                        {
                            viewModel.Items[i].IsSelected = false;
                        }
                    }

                    viewListChoose.IsVisible = false;
                    if (addressSetting != null)
                    {
                        addressSetting.IsSelected = false;
                    }
                }else{
                    _ = Navigation.PushAsync(new SettingPageUI());
                    viewListChoose.IsVisible = false;
                }
            };
        }

        private void arrange(ValueIsAddress addressSetting)
        {
            switch (addressSetting)
            {
                case ValueIsAddress.isAccomodation:
                    this.isAccomodation = true;
                    this.isCurrentSite = false;
                    this.isResident = false;
                    this.isJobSite = false;
                    break;
                case ValueIsAddress.isCurrentSite: 
                    this.isCurrentSite = true;
                    this.isAccomodation = false;
                    this.isResident = false;
                    this.isJobSite = false;
                    break;
                case ValueIsAddress.isResident:
                    this.isResident = true;
                    this.isCurrentSite = false;
                    this.isAccomodation = false;
                    this.isJobSite = false;
                    break;
                case ValueIsAddress.isJobSite:
                    this.isJobSite = true;
                    this.isResident = false;
                    this.isCurrentSite = false;
                    this.isAccomodation = false; 
                    break;
            }
        }

        private void showList(ValueIsAddress valueIsAddress)
        {
            this.arrange(valueIsAddress);
            viewListChoose.IsVisible = true;

            var viewModel = BindingContext as CustomerPageVM;
            if(viewModel.Items.Count <= 0)
            {
                listAddressCus.IsVisible = false;
                titleNoData.IsVisible = true;
            }
            else
            {
                listAddressCus.IsVisible = true;
                titleNoData.IsVisible = false;
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as CustomerPageVM;
            if(vm.Items.Count <= 0)
            {
                BindingContext = new CustomerPageVM(Navigation);
            }
            SiteCurentLabelNavigatePage.Text = this._customerInforPlaceholder_CurentSite;
            HometownLabelNavigatePage.Text = this._customerInforPlaceholder_Hometown;
            PlaceJobLabelNavigatePage.Text = this._customerInforPlaceholder_JobSite;
            AddressResidentLabelNavigatePage.Text = this._customerInforPlaceholder_Resident;


            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforCurrentSite))
            {
                SiteCurentLabelNavigatePage.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCurrentSite].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforHometown))
            {
                HometownLabelNavigatePage.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforHometown].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforJobSite))
            {
                PlaceJobLabelNavigatePage.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforJobSite].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforResidentSite))
            {
                AddressResidentLabelNavigatePage.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforResidentSite].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforName))
            {
                TENKH.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforName] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforName].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforCMNDCreateDate))
            {
                ngaycap.Date = Convert.ToDateTime(Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCMNDCreateDate]);
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforBirthday))
            {
                ngaysinh.Date = Convert.ToDateTime(Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforBirthday]);
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforSex))
            {
                //gioitinh_.SelectedItem = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforSex];
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforPhone))
            {
                dienthoai.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforPhone] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforPhone].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforDanToc))
            {
                dantoc.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforDanToc] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforDanToc].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforTonGiao))
            {
                tongiao.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforTonGiao] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforTonGiao].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforJob))
            {
                nghenghiep.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforJob] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforJob].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforCompany))
            {
                Congty.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCompany] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCompany].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforEmail))
            {
                mail.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforEmail] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforEmail].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforCartMoto))
            {
                bienso.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCartMoto] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCartMoto].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforTypeMoto))
            {
                //bienso.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCartMoto].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforEdu))
            {
                //bienso.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforEdu].ToString();
            }
            if (Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.CustomerInforProfection))
            {
                Chuyenmon.Text = Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforProfection] == null ? "" : Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforProfection].ToString();
            } 
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RemoveKey(
                new List<string>
                {
                    KeyCustomerViewEnumeration.CustomerInforCurrentSite,
                    KeyCustomerViewEnumeration.CustomerInforHometown,
                    KeyCustomerViewEnumeration.CustomerInforJobSite,
                    KeyCustomerViewEnumeration.CustomerInforResidentSite,
                    KeyCustomerViewEnumeration.CustomerInforName,
                    KeyCustomerViewEnumeration.CustomerInforCMNDCreateDate,
                    KeyCustomerViewEnumeration.CustomerInforBirthday,
                    KeyCustomerViewEnumeration.CustomerInforSex,
                    KeyCustomerViewEnumeration.CustomerInforPhone,
                    KeyCustomerViewEnumeration.CustomerInforDanToc,
                    KeyCustomerViewEnumeration.CustomerInforTonGiao,
                    KeyCustomerViewEnumeration.CustomerInforJob,
                    KeyCustomerViewEnumeration.CustomerInforCompany,
                    KeyCustomerViewEnumeration.CustomerInforEmail,
                    KeyCustomerViewEnumeration.CustomerInforCartMoto,
                    KeyCustomerViewEnumeration.CustomerInforTypeMoto,
                    KeyCustomerViewEnumeration.CustomerInforEdu,
                    KeyCustomerViewEnumeration.CustomerInforProfection,
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

        #region Function
        public async void getkhachthue()
        {

            try
            {
                FirebaseResponse res = await client.GetAsync("khachthue/" + CMND_ + "/KHACHTHUE/");
                Data thongtinkhachthue = res.ResultAs<Data>();
                if(thongtinkhachthue != null)
                {
                    TENKH.Text = thongtinkhachthue.TEN_KHACH;
                    dienthoai.Text = thongtinkhachthue.SDT;
                    dantoc.Text = thongtinkhachthue.DANTOC;
                    tongiao.Text = thongtinkhachthue.TONGIAO;
                    nghenghiep.Text = thongtinkhachthue.NGHENGHIEP;
                    mail.Text = thongtinkhachthue.EMAIL;
                    bienso.Text = thongtinkhachthue.BIENSOXE;
                    Chuyenmon.Text = thongtinkhachthue.TRINHDOCHUYENMON;
                    ngaysinh.Date = Convert.ToDateTime(thongtinkhachthue.NGAY_SINH);
                    ngaycap.Date = Convert.ToDateTime(thongtinkhachthue.NGAY_CAP);
                    Congty.Text = thongtinkhachthue.CongTy;

                    PlaceJobLabelNavigatePage.Text = (thongtinkhachthue.NOILAMVIEC == "") ? _customerInforPlaceholder_JobSite : thongtinkhachthue.NOILAMVIEC;
                    SiteCurentLabelNavigatePage.Text = (thongtinkhachthue.DIACHIHIENNAY == "") ? _customerInforPlaceholder_CurentSite : thongtinkhachthue.DIACHIHIENNAY;
                    AddressResidentLabelNavigatePage.Text = (thongtinkhachthue.NOITHUONGTHU == "") ? _customerInforPlaceholder_Resident : thongtinkhachthue.NOITHUONGTHU;
                    HometownLabelNavigatePage.Text = (thongtinkhachthue.QUE_QUAN == "") ? _customerInforPlaceholder_Hometown : thongtinkhachthue.QUE_QUAN;

                    //check hiện thị
                    for (int i = 0; i < gioitinh_.Items.Count; i++)
                    {
                        try
                        {
                            if (thongtinkhachthue.GIOI_TINH == gioitinh_.Items[i].ToString())
                            {
                                gioitinh_.SelectedIndex = i;
                            }
                        }
                        catch { gioitinh_.SelectedIndex = 0; }
                    }

                    for (int i = 0; i < hocvan_.Items.Count; i++)
                    {
                        try
                        {
                            if (thongtinkhachthue.TRINHDOHOCVAN == hocvan_.Items[i].ToString())
                            {
                                hocvan_.SelectedIndex = i;
                            }
                        }
                        catch { hocvan_.SelectedIndex = 0; }

                    }
                    for (int i = 0; i < loaixe_.Items.Count; i++)
                    {
                        try
                        {
                            if (thongtinkhachthue.LOAIXE == loaixe_.Items[i].ToString())
                            {
                                loaixe_.SelectedIndex = i;
                            }
                        }
                        catch { loaixe_.SelectedIndex = 0; }
                    }
                } 
            }
            catch
            {
                //if (!Application.Current.Properties.ContainsKey(KeyCustomerViewEnumeration.DefaultAddress))
                //{
                //    _ = Navigation.PushAsync(new SettingPageUI());
                //}
                this.gioitinh_.SelectedIndex = 0;
                this.loaixe_.SelectedIndex = 0;
                this.hocvan_.SelectedIndex = 0;
                return;
            }
        }

        private bool checkSelectPicker()
        {
            List<int> selectIndexList = new List<int> {
                gioitinh_.SelectedIndex,
                loaixe_.SelectedIndex,
                hocvan_.SelectedIndex

            };

            foreach (var i in selectIndexList)
            {
                if (i == -1)
                {
                    _ = DisplayAlert("Thông báo", "Vui lòng chọn đầy đủ thông tin!", "OK");
                    return false;
                }
            }
            return true;
        }

        private bool checkChangeAddress()
        {
            List<Label> addressList = new List<Label> {
                PlaceJobLabelNavigatePage,
                AddressResidentLabelNavigatePage,
                HometownLabelNavigatePage
            };
            foreach (var i in addressList)
            {
                if (i.Text == KeyCustomerViewEnumeration.CustomerInforPlaceholder_Hometown ||
                    i.Text == KeyCustomerViewEnumeration.CustomerInforPlaceholder_JobSite ||
                    i.Text == KeyCustomerViewEnumeration.CustomerInforPlaceholder_Resident)
                {
                    _ = DisplayAlert("Thông báo", "Vui lòng chọn địa chỉ các trường còn thiếu!", "OK");
                    return false;
                }
            }
            return true;
        }

        public bool checkNull()
        {
            List<Entry> entryList = new List<Entry>{
                CMND,
                TENKH,
                dienthoai,
                dantoc,
                tongiao,
                Congty,
                nghenghiep,
                mail,
                bienso,
                Chuyenmon
            };
            for (int i = 0; i < entryList.Count; i++)
            {
                if (entryList[i].Text == string.Empty ||
                    entryList[i].Text == null)
                {
                    _ = DisplayAlert("Thông báo", "Mời bạn nhập đầy đủ thông tin!", "OK");
                    focusTextBox(entryList[i]);
                    return true;
                }
            }
            return false;
        }

        private void focusTextBox(Entry entry)
        {
            if (entry.Text == string.Empty ||
                entry.Text == null)
            {
                entry.Focus();
            }
        }

        private bool emailValidation(Entry emailAddress)
        {
            if (!emailAddress.Text.Contains("@gmail.com"))
            {
                _ = DisplayAlert("Thông báo", "Kiểm tra lại Email: " + mail.Text, "OK");
                focusTextBox(emailAddress);
                return false;
            }
            return true;
        }

        private void addpickerhocvan()
        {
            hocvan_.Items.Add("Tiến sĩ");
            hocvan_.Items.Add("Thạc sĩ");
            hocvan_.Items.Add("Đại học");
            hocvan_.Items.Add("Cao đẳng");
            hocvan_.Items.Add("Trung cấp");
            hocvan_.Items.Add("Tốt nghiệp trung học phổ thông");
            hocvan_.Items.Add("Tốt nghiệp trung học cơ sở");
            hocvan_.Items.Add("Không biết chữ");
        }

        private void addgioitinh()
        {
            gioitinh_.Items.Add("Nam");
            gioitinh_.Items.Add("Nữ");
        }

        private void addloaixe()
        {
            loaixe_.Items.Add("Xe số");
            loaixe_.Items.Add("Xe tay ga");
        }

        private void refreshLayout()
        {
            btnsave.IsEnabled = false;
            CMND.Text = string.Empty;
            TENKH.Text = string.Empty;
            dienthoai.Text = string.Empty;
            dantoc.Text = _dantoc;
            tongiao.Text = tongiao.Text;
            Congty.Text = string.Empty;
            nghenghiep.Text = string.Empty;
            mail.Text = string.Empty;
            Chuyenmon.Text = string.Empty;

            PlaceJobLabelNavigatePage.Text = KeyCustomerViewEnumeration.CustomerInforPlaceholder_JobSite;
            SiteCurentLabelNavigatePage.Text = KeyCustomerViewEnumeration.CustomerInforPlaceholder_CurentSite;
            AddressResidentLabelNavigatePage.Text = KeyCustomerViewEnumeration.CustomerInforPlaceholder_Resident;
            HometownLabelNavigatePage.Text = KeyCustomerViewEnumeration.CustomerInforPlaceholder_Hometown;

            RemoveKey(
                new List<string>
                {
                    KeyCustomerViewEnumeration.CustomerInforCurrentSite,
                    KeyCustomerViewEnumeration.CustomerInforHometown,
                    KeyCustomerViewEnumeration.CustomerInforJobSite,
                    KeyCustomerViewEnumeration.CustomerInforResidentSite,
                    KeyCustomerViewEnumeration.CustomerInforName,
                    KeyCustomerViewEnumeration.CustomerInforCMNDCreateDate,
                    KeyCustomerViewEnumeration.CustomerInforBirthday,
                    KeyCustomerViewEnumeration.CustomerInforSex,
                    KeyCustomerViewEnumeration.CustomerInforPhone,
                    KeyCustomerViewEnumeration.CustomerInforDanToc,
                    KeyCustomerViewEnumeration.CustomerInforTonGiao,
                    KeyCustomerViewEnumeration.CustomerInforJob,
                    KeyCustomerViewEnumeration.CustomerInforCompany,
                    KeyCustomerViewEnumeration.CustomerInforEmail,
                    KeyCustomerViewEnumeration.CustomerInforCartMoto,
                    KeyCustomerViewEnumeration.CustomerInforTypeMoto,
                    KeyCustomerViewEnumeration.CustomerInforEdu,
                    KeyCustomerViewEnumeration.CustomerInforProfection,
                });
        }
        #endregion

        private async void CancelInputUser_Clicked(object sender, EventArgs e)
        {
            //Application.Current.Properties.Clear();
            await Navigation.PopAsync();
        }

        private async void btnsave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!checkNull())
                {
                    if (checkSelectPicker())
                    {
                        if (emailValidation(mail))
                        {
                            if (dienthoai.Text.Length == 9)
                            {
                                _ = DisplayAlert("Thông báo", "Kiểm tra lại số điện thoại: " + dienthoai.Text, "OK");
                            }
                            else
                            {
                                if (checkChangeAddress())
                                {
                                    var data = new Data
                                    {
                                        CMND = CMND.Text,
                                        TEN_KHACH = TENKH.Text,
                                        GIOI_TINH = gioitinh,
                                        SDT = dienthoai.Text,
                                        DANTOC = dantoc.Text,
                                        TONGIAO = tongiao.Text,
                                        NGHENGHIEP = nghenghiep.Text,
                                        NOILAMVIEC = PlaceJobLabelNavigatePage.Text,
                                        DIACHIHIENNAY = SiteCurentLabelNavigatePage.Text,
                                        QUE_QUAN = HometownLabelNavigatePage.Text,
                                        EMAIL = mail.Text,
                                        BIENSOXE = bienso.Text,
                                        LOAIXE = biensoxe,
                                        TRINHDOCHUYENMON = Chuyenmon.Text,
                                        NOITHUONGTHU = AddressResidentLabelNavigatePage.Text,
                                        TRINHDOHOCVAN = trinhdochuyenmon,
                                        NGAY_CAP = ngaycap.Date,
                                        NGAY_SINH = ngaysinh.Date,
                                        CongTy = Congty.Text

                                    };
                                    SetResponse response = await client.SetAsync("khachthue/" + CMND_ + "/KHACHTHUE/", data);
                                    Data data1 = response.ResultAs<Data>();
                                    if (data1.CMND == Convert.ToString(CMND.Text))
                                    {
                                        _ = DisplayAlert("Thông báo", "Thêm thành công", "OK");
                                        getkhachthue();
                                        CMND.Text = UserData.shared.IDCard;
                                        CMND_ = CMND.Text;
                                        if (!checkNull())
                                        {
                                            btnsave.IsEnabled = true;
                                        }
                                        else
                                        {
                                            btnsave.IsEnabled = true;
                                        } 
                                    }
                                    else
                                    {
                                        _ = DisplayAlert("Thông báo", "Thêm thất bại", "OK");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { _ = DisplayAlert("Thông báo", "Thêm thất bại", "OK"); }
        }

        private void btnGD_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerFamilyPageUI());
        }

        private void btnQT_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerProcessPageUI());
        }

        private void btnrefresh_Clicked(object sender, EventArgs e)
        {
            refreshLayout();
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker trinhdo = sender as Picker;
            var selectedItemtrinhdo = trinhdo.SelectedItem;
            trinhdochuyenmon = Convert.ToString(selectedItemtrinhdo);
        }

        private void UtilityPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker sex = sender as Picker;
            var selectedItem = sex.SelectedItem;
            gioitinh = Convert.ToString(selectedItem);
        }

        private void loaixe__SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker loaixe = sender as Picker;
            var selectedItemloaixe = loaixe.SelectedItem;
            biensoxe = Convert.ToString(selectedItemloaixe);
        }

        void Hometown_Tapped(System.Object sender, System.EventArgs e)
        {
            NavigatePageWithKey(KeyCustomerViewEnumeration.CustomerInforHometown);
        }

        void PlaceJob_Tapped(System.Object sender, System.EventArgs e)
        {
            NavigatePageWithKey(KeyCustomerViewEnumeration.CustomerInforJobSite);
        }

        void SiteCurent_Tapped(System.Object sender, System.EventArgs e)
        {
            NavigatePageWithKey(KeyCustomerViewEnumeration.CustomerInforCurrentSite);
        }

        void AddressResident_Tapped(System.Object sender, System.EventArgs e)
        {
            NavigatePageWithKey(KeyCustomerViewEnumeration.CustomerInforResidentSite);
        }

        public void NavigatePageWithKey(string key)
        {
            switch (key)
            {
                case KeyCustomerViewEnumeration.CustomerInforCurrentSite:
                    Navigation.PushAsync(new AddAddressPageUI(
                       ETypePageAddAddress.EInforCustomer,
                       (int)EInforCustomer.CurrentSite
                       ));
                    break;
                case KeyCustomerViewEnumeration.CustomerInforHometown:
                    Navigation.PushAsync(new AddAddressPageUI(
                        ETypePageAddAddress.EInforCustomer,
                        (int)EInforCustomer.Hometown
                        ));
                    break;
                case KeyCustomerViewEnumeration.CustomerInforJobSite:
                    Navigation.PushAsync(new AddAddressPageUI(
                        ETypePageAddAddress.EInforCustomer,
                        (int)EInforCustomer.JobSite
                        ));
                    break;
                case KeyCustomerViewEnumeration.CustomerInforResidentSite:
                    Navigation.PushAsync(new AddAddressPageUI(
                        ETypePageAddAddress.EInforCustomer,
                        (int)EInforCustomer.ResidentSite
                        ));
                    break;
            }
            SaveDB();
        }

        public void SaveDB()
        {
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforName] = TENKH.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCMNDCreateDate] = ngaycap.Date;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforBirthday] = ngaysinh.Date;
            //Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforSex] = gioitinh_.sel;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforPhone] = dienthoai.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforDanToc] = dantoc.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforTonGiao] = tongiao.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforJob] = nghenghiep.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCompany] = Congty.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforEmail] = mail.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforCartMoto] = bienso.Text;
            //Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforTypeMoto] = loaixe_.Text;
            //Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforEdu] = hocvan_.Text;
            Application.Current.Properties[KeyCustomerViewEnumeration.CustomerInforProfection] = Chuyenmon.Text;
            Application.Current.SavePropertiesAsync();
        }
         
        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (addressSetting != null)
            {
                addressSetting.IsSelected = false;
            }
            AddressSettingModel currentModel = ((CheckBox)sender).BindingContext as AddressSettingModel;
            addressSetting = currentModel;

            if (currentModel.IsSelected)
            {
                var viewModel = BindingContext as CustomerPageVM;
                int index = viewModel.Items.IndexOf(currentModel);
            }

        }
         
    }
} 