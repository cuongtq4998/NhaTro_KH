using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.Models;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class WaveInformationChargePageUI : ContentPage
    {
        public WaveInformationChargePageUI()
        {
            InitializeComponent();
            NAM.Text = Convert.ToString(now.Year);
            THANG.Text = Convert.ToString(now.Month);
            activityIndicator.IsRunning = false;
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
            if (!string.IsNullOrWhiteSpace(SODIEN.Text)
                && !string.IsNullOrWhiteSpace(SONUOC.Text)
                && !string.IsNullOrWhiteSpace(NAM.Text)
                && !string.IsNullOrWhiteSpace(THANG.Text)
                && !string.IsNullOrWhiteSpace(PHONG.Text)
                && !string.IsNullOrWhiteSpace(XESO.Text)
                && !string.IsNullOrWhiteSpace(XETAYGA.Text))
            {
                return true;
            }
            else return false;
        }

        public void ResetText()
        {
            PHONG.Text = string.Empty;
            SODIEN.Text = string.Empty;
            SONUOC.Text = string.Empty;
            XESO.Text = string.Empty;
            XETAYGA.Text = string.Empty;
        }

        public async Task DIENNUOC()
        {
            activityIndicator.IsRunning = true;
            try
            {
                if (checknull())
                {
                    var data = new DIENNUOC
                    {
                        SO_DIEN = SODIEN.Text,
                        SO_NUOC = SONUOC.Text,
                        SO_XE_SO_KHACH = XESO.Text,
                        SO_XE_TAY_GA_KHACH = XETAYGA.Text
                    };
                    SetResponse response = await client.SetAsync("diennuoc/" + NAM.Text + "/" + THANG.Text + "/" + PHONG.Text + "/", data);
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

        public void EnventTextChange()
        {

            try
            {
                activityIndicator.IsRunning = true;
                if (PHONG.Text == null || NAM.Text == null || PHONG.Text == null)
                {
                    return;
                    activityIndicator.IsRunning = false;
                }
                else
                {
                    settext();
                }
            }
            catch { activityIndicator.IsRunning = false; }
        }

        public async void settext()
        {
            if (NAM.Text != null && THANG.Text != null)
            {
                activityIndicator.IsRunning = true;
                try
                {

                    FirebaseResponse res = await client.GetAsync("diennuoc/" + NAM.Text + "/" + THANG.Text + "/" + PHONG.Text);
                    DIENNUOC dienuoc = res.ResultAs<DIENNUOC>();
                    if (dienuoc != null)
                    {
                        SODIEN.Text = dienuoc.SO_DIEN;
                        SONUOC.Text = dienuoc.SO_NUOC;
                        XESO.Text = dienuoc.SO_XE_SO_KHACH;
                        XETAYGA.Text = dienuoc.SO_XE_TAY_GA_KHACH;
                        if (SODIEN.Text != null && SONUOC.Text != null) {
                            _ = DisplayAlert("Thông báo", "Thông tin này đã nhập trước đó!", "Xem lại");
                            activityIndicator.IsRunning = false;
                        }
                    }
                    else
                    {
                        SODIEN.Text = string.Empty;
                        SONUOC.Text = string.Empty;
                        XESO.Text = string.Empty;
                        XETAYGA.Text = string.Empty;
                        activityIndicator.IsRunning = false;
                    }
                }
                catch (Exception ex) { return; }

            }
        }
        #endregion

        private void btnThem_Clicked(object sender, EventArgs e)
        {
            DIENNUOC();
            ResetText();
        }

        private void btnback_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void PHONG_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnventTextChange();
        }

        private void NAM_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnventTextChange();
        }

        private void THANG_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnventTextChange();
        }
    }
}
