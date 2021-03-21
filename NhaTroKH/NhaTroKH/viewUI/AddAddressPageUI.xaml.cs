using System;
using System.Collections.Generic;
using System.ComponentModel; 
using NhaTroKH.DB; 
using Xamarin.Forms;
using static NhaTroKH.@interface.Enum;

namespace NhaTroKH.viewUI
{
    public partial class AddAddressPageUI : ContentPage, INotifyPropertyChanged
    {
        List<Distrist> quanHuyen = new List<Distrist>();
        List<Ward> phuongXa = new List<Ward>();
        //property  
        private string provincialSelect = "";
        private string districtValue = "";
        private string wardValue = "";
        private string streesSelect = "";
        private bool flagCheckHidden = true;
        private ETypePageAddAddress eTypePage;
        private int inforPositon;
        private string[] listInforAddress = { "", "", "", "" };


        public AddAddressPageUI(ETypePageAddAddress eTypePage, int inforPositon)
        {
            InitializeComponent();
            AddAddressButton.IsEnabled = false;
            DisttrictPicker.IsEnabled = false;
            WardPicker.IsEnabled = false;
            StreesEntry.IsEnabled = false;
            ViewInputFromKeyboard.IsVisible = false;

            // recive type page and thông tin page
            this.eTypePage = eTypePage;
            this.inforPositon = inforPositon;

            ProvincialPicker.ItemsSource = LoadingPageUI.provinces1;
            ProvincialPicker.SelectedIndexChanged += (sender, e) =>
            {
                if (ProvincialPicker.SelectedIndex != -1)
                {
                    Picker pickerValue = sender as Picker;
                    var selectedItem = pickerValue.SelectedItem as Province;

                    provincialSelect = selectedItem.name_with_type;
                    valueUser.Text = " , " + provincialSelect;
                    DisttrictPicker.IsEnabled = true;
                    quanHuyen.Clear();
                    foreach (var provinces2 in LoadingPageUI.provinces2)
                    {
                        if (provinces2.parent_code == selectedItem.code)
                        {
                            quanHuyen.Add(provinces2);
                        }
                    }

                }
                DisttrictPicker.ItemsSource = null;
                WardPicker.ItemsSource = null;
                DisttrictPicker.ItemsSource = quanHuyen;
            };

            DisttrictPicker.SelectedIndexChanged += (sender, e) =>
            {
                if (DisttrictPicker.SelectedIndex != -1)
                {
                    Picker pickerValue = sender as Picker;
                    var selectedItem = pickerValue.SelectedItem as Distrist;

                    districtValue = selectedItem.name_with_type;
                    valueUser.Text = " , " + districtValue + "," + provincialSelect;
                    WardPicker.IsEnabled = true;
                    phuongXa.Clear();
                    foreach (var provinces3 in LoadingPageUI.provinces3)
                    {
                        if (provinces3.parent_code == selectedItem.code)
                        {
                            phuongXa.Add(provinces3);
                        }
                    }
                    WardPicker.ItemsSource = null;
                    WardPicker.ItemsSource = phuongXa;
                }
            };

            WardPicker.SelectedIndexChanged += (sender, e) =>
            {
                if (WardPicker.SelectedIndex != -1)
                {
                    Picker pickerValue = sender as Picker;
                    var selectedItem = pickerValue.SelectedItem as Ward;

                    wardValue = selectedItem.name_with_type;
                    valueUser.Text = " , " + wardValue + " , " + districtValue + " , " + provincialSelect;
                    StreesEntry.IsEnabled = true;
                }
            };

            StreesEntry.TextChanged += (sender, e) =>
            {
                Entry entry = sender as Entry;
                streesSelect = entry.Text;
                valueUser.Text = streesSelect + " , " + wardValue + " , " + districtValue + " , " + provincialSelect;
                AddAddressButton.IsEnabled = true;
            };

            ProvincialEntry.TextChanged += (sender, e) =>
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
            };

            DistrictEntry.TextChanged += (sender, e) => {
                var districtEntry = (Entry)sender;
                string value = districtEntry.Text;
                listInforAddress[2] = value;
                valueUser.Text = String.Join(", ", listInforAddress);

                // check hiden button
                AddAddressButton.IsEnabled = (listInforAddress[0] != string.Empty ? true : false)
                    && (listInforAddress[2] != string.Empty ? true : false)
                    && (listInforAddress[3] != string.Empty ? true : false);
            };

            WardEntry.TextChanged += (sender, e) => {
                var wardEntry = (Entry)sender;
                string value = wardEntry.Text;
                listInforAddress[1] = value;
                valueUser.Text = String.Join(", ", listInforAddress);

                // check hiden button
                AddAddressButton.IsEnabled = (listInforAddress[0] != string.Empty ? true : false)
                    && (listInforAddress[2] != string.Empty ? true : false)
                    && (listInforAddress[3] != string.Empty ? true : false);
            };

            StreesEntryUser.TextChanged += (sender, e) =>
            {
                var streesEntry = (Entry)sender;
                string value = streesEntry.Text;
                listInforAddress[0] = value;
                valueUser.Text = String.Join(", ", listInforAddress);

                // check hiden button
                AddAddressButton.IsEnabled = (listInforAddress[0] != string.Empty ? true : false)
                    && (listInforAddress[2] != string.Empty ? true : false)
                    && (listInforAddress[3] != string.Empty ? true : false);
            };
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

            // check nếu là page setting
            if (this.eTypePage == ETypePageAddAddress.EDefaultAddress)
            { 
                switch (this.inforPositon)
                {
                    case (int)ESetting.DefaultAddress:
                        Application.Current.Properties[KeyCustomerViewEnumeration.DefaultAddress] = getTextAddressFromUI;
                        break;
                    case (int)ESetting.HometownSetting:
                        Application.Current.Properties[KeyCustomerViewEnumeration.HometownSiteSetting] = getTextAddressFromUI;
                        break;
                    case (int)ESetting.ResidentSetting:
                        Application.Current.Properties[KeyCustomerViewEnumeration.ResidentSiteSetting] = getTextAddressFromUI;
                        break; 
                }
                Application.Current.SavePropertiesAsync();
                Navigation.PopAsync();
            }
        }  

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
    }
} 