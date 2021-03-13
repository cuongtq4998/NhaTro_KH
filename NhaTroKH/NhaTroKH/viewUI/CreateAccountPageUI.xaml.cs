using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.Common;
using NhaTroKH.Models;
using NhaTroKH.Service;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class CreateAccountPageUI : ContentPage
    {
        public CreateAccountPageUI()
        {
            InitializeComponent();
            activityIndicator.IsRunning = false;
            client = new FireSharp.FirebaseClient(iconfig.config);
        }

        private Iconfig iconfig = new Iconfig();
        private IFirebaseClient client;
        private SetResponse response; 

        public bool checknull()
        {
            if (!string.IsNullOrWhiteSpace(CMND.Text)
                && !string.IsNullOrWhiteSpace(MATKHAU.Text))
            {
                return true;
            }
            else return false;
        }

        public async Task CreateAccount()
        {
            activityIndicator.IsRunning = true;
            try
            {
                if (checknull())
                {
                    if (CMND.Text.Length == 9 || CMND.Text.Length == 12)
                    {
                        var data = new LOGIN
                        {
                            MATKHAU_TK = MATKHAU.Text
                        };
                        response = await client.SetAsync("taikhoan/" + CMND.Text, data);
                        LOGIN data1 = response.ResultAs<LOGIN>();
                        if (data1 != null)
                        {
                            _ = DisplayAlert("Thông báo", "Tạo tài khoản thành công", "OK");
                            activityIndicator.IsRunning = false;
                            _ = Navigation.PushAsync(new LoginPageUI());
                            CMND.Text = string.Empty;
                            MATKHAU.Text = string.Empty;
                        }
                        else
                        {
                            _ = DisplayAlert("Thông báo", "Tạo tài khoản thất bại", "OK");
                            activityIndicator.IsRunning = false;
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Thông báo", "Kiểm tra lại CMND 9 số / Căn cước 12 số", "OK");
                        activityIndicator.IsRunning = false;
                    }
                }
                else
                {
                    _ = DisplayAlert("Thông báo", "Mời bạn nhập đầy đủ thông tin!", "OK");
                    activityIndicator.IsRunning = false;
                }
            }
            catch
            {
                _ = DisplayAlert("Thông báo", "Thất bại", "OK");
                activityIndicator.IsRunning = false;
            }
        }

        private void btnCreateTK_Clicked(object sender, EventArgs e)
        {
            _ = CreateAccount();

        }

        private void btnback_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void CMND_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate.validateCMND(sender);

        }
    }
}
