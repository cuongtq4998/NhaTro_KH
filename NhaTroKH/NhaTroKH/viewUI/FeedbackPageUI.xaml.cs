using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.Models;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class FeedbackPageUI : ContentPage
    {
        public FeedbackPageUI()
        {
            InitializeComponent();
        }

        #region Connect
        DateTime now = DateTime.Now;
        static IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "mJh3ttHegM5JEw4I8KKbreCWxcmVjIrcM5I9fhjx",
            BasePath = "https://nhatro-271ce.firebaseio.com/"

        };

        public static IFirebaseClient client = new FireSharp.FirebaseClient(config);
        #endregion

        #region Function

        public bool checknull()
        {
            if (!string.IsNullOrWhiteSpace(phongfeedback.Text)
                && !string.IsNullOrWhiteSpace(thongtinphanhoi.Text))
            {
                return true;
            }
            else return false;
        }

        public async Task Addfeedback()
        {
            activityIndicator.IsRunning = true;
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            int dem = 0;
            int i = 1;
            bool co = true;
            while (co)
            {
                try
                {
                    FirebaseResponse thongtinphanhoi = await client.GetAsync("ThongTinPhanHoi/" + phongfeedback.Text + "/" + i.ToString() + "/");
                    if (thongtinphanhoi.Body == "null")
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
                    var data = new FEEDBACK
                    {
                        CMND = LoginPageUI.SOCMND,
                        Phong = phongfeedback.Text,
                        STT = (dem + 1).ToString(),
                        TrangThai = "0",
                        PhanHoi = thongtinphanhoi.Text
                    };
                    SetResponse response = await client.SetAsync("ThongTinPhanHoi/" + phongfeedback.Text + "/" + (dem + 1).ToString(), data);
                    //SetResponse response = await client.SetAsync("ThongTinPhanHoi/" + phongfeedback.Text + "/1", data);
                    _ = DisplayAlert("Thông báo", "Ghi nhận thành công", "OK");
                    activityIndicator.IsRunning = false;

                }
                else
                {
                    _ = DisplayAlert("Thông báo", "Mời bạn nhập đầy đủ thông tin!", "OK");
                    activityIndicator.IsRunning = false;
                }


            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Thông báo", "Thất bại", "OK");
            }
        }

        #endregion

        private void btnfeedback_Clicked(object sender, EventArgs e)
        {
            _ = Addfeedback();
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
