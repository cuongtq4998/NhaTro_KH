using System;
using System.Collections.Generic;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.Common;
using NhaTroKH.Models;
using NhaTroKH.Service;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class LoginPageUI : ContentPage
    {
        public LoginPageUI()
        {
            InitializeComponent();
            activityIndicator.IsRunning = false;
            CMND_login.Focus();
            client = new FireSharp.FirebaseClient(iconfig.config);
        }

        private Iconfig iconfig = new Iconfig();
        private IFirebaseClient client;
        private FirebaseResponse response;

        public static string SOCMND;
         

        private void CreateAcount_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccountPageUI());
        }

        private async void dangnhap_Clicked(object sender, EventArgs e)
        {
            try
            {
                activityIndicator.IsRunning = true;
                response = await client.GetAsync("taikhoan/" + CMND_login.Text);
                LOGIN login = response.ResultAs<LOGIN>();

                if (CMND_login.Text != null && MATKHAU_login.Text != null)
                {
                    if (login != null)
                    {
                        if (MATKHAU_login.Text == login.MATKHAU_TK)
                        {
                            activityIndicator.IsRunning = false;
                            _ = DisplayAlert("Thông báo", "Đăng nhập thành công", "OK");
                            //CMND_login.Text = string.Empty;
                            MATKHAU_login.Text = string.Empty;
                            _ = Navigation.PushAsync(new HomePageUI());
                            SOCMND = CMND_login.Text;
                        }
                        else
                        {
                            _ = DisplayAlert("Thông báo", "Kiểm tra lại mật khẩu", "OK");
                            activityIndicator.IsRunning = false;
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Thông báo", "Tài khoản không tồn tại", "OK");
                        activityIndicator.IsRunning = false;
                    }
                }
                else
                {
                    _ = DisplayAlert("Thông báo", "Mời bạn nhập đầy đủ thông tin", "OK");
                    activityIndicator.IsRunning = false;
                }
            }
            catch (Exception ex) { }
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

        private void CMND_login_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate.validateCMND(sender);
        }
    }
}
