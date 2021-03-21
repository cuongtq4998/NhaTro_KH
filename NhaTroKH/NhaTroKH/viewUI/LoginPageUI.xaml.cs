using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NhaTroKH.Common;
using NhaTroKH.Database;
using NhaTroKH.DB.SqlLite;
using NhaTroKH.Model;
using NhaTroKH.Models;
using NhaTroKH.Service;
using NhaTroKH.viewmodel;
using NhaTroKH.viewUI;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    [DesignTimeVisible(false)]
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

        private void CMND_login_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate.validateCMND(sender);
        }

        async Task LoginPage_Tapped()
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
                            _ = Navigation.PushAsync(new HomePageUI());
                            MATKHAU_login.Text = string.Empty;
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
        void CreateAccount_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CreateAccountPageUI());
        }

        void ExitApp_Tapped(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

        void LoginPage_Tapped(System.Object sender, System.EventArgs e)
        {
            _ = LoginPage_Tapped();
        }
    } 
} 