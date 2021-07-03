using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FireSharp.Interfaces;
using FireSharp.Response;
using NhaTroKH.model;
using NhaTroKH.Models;
using Xamarin.Forms;

namespace NhaTroKH.viewUI
{
    public partial class RoomInformationChartPageUI : ContentPage
    {
        public RoomInformationChartPageUI()
        {
            InitializeComponent();
            NAM_.Text = Convert.ToString(now.Year);
            THANG_.Text = Convert.ToString(now.Month);
            activityIndicator.IsRunning = false;
            //---
            TIENPHONG_.Text = "0";
            TIENNUOC_.Text = "0";
            TIENDIEN_.Text = "0";
            TIENDICHVU_.Text = "0";
            TONGTIEN_.Text = "0";
            xeSo.Text = "0";
            xeTayGa.Text = "0";
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
        ObservableCollection<Chitietdichvu> dichvu = new ObservableCollection<Chitietdichvu>();
        public ObservableCollection<Chitietdichvu> DICHVU { get { return dichvu; } }

        public void setcolorandSize()
        {
            NAM_.TextColor = Color.Red;
            THANG_.TextColor = Color.Red;
            TIENPHONG_.TextColor = Color.Red;
            TIENNUOC_.TextColor = Color.Red;
            TIENDIEN_.TextColor = Color.Red;
            TIENDICHVU_.TextColor = Color.Red;
            TONGTIEN_.TextColor = Color.Red;
            xeSo.TextColor = Color.Red;
            xeTayGa.TextColor = Color.Red;

            //--
            NAM_.FontSize = 20;
            THANG_.FontSize = 20;
            TIENPHONG_.FontSize = 20;
            TIENNUOC_.FontSize = 20;
            TIENDIEN_.FontSize = 20;
            TIENDICHVU_.FontSize = 20;
            TONGTIEN_.FontSize = 20;
            xeTayGa.FontSize = 20;
            xeSo.FontSize = 20;

        }

        public string formatmoney(string value)
        {
            return string.Format("{0:0,0} ", int.Parse(value));
        }

        public async void Xemthongtin()
        {
            if (PHONG_.Text != string.Empty)
            {
                activityIndicator.IsRunning = true;
                try
                {
                    FirebaseResponse res;
                    if (now.Month < 10)
                    {
                        res = await client.GetAsync("ThongTinTienPhong/" + NAM_.Text + "/" + Convert.ToString(now.Month).PadLeft(2, '0') + "/" + UserData.shared.IDCard + "/" + PHONG_.Text + "/");
                    }
                    else
                    {
                        res = await client.GetAsync("ThongTinTienPhong/" + NAM_.Text + "/" + Convert.ToString(now.Month) + "/" + UserData.shared.IDCard + "/" + PHONG_.Text + "/");
                    }
                    THONGTINTIENPHONG tienphong = res.ResultAs<THONGTINTIENPHONG>();
                    if (res.Body == "null")
                    {
                        TIENPHONG_.Text = "-";
                        TIENNUOC_.Text = "-";
                        TIENDIEN_.Text = "-";
                        TIENDICHVU_.Text = "-";
                        TONGTIEN_.Text = "-";
                        xeSo.Text = "-";
                        xeTayGa.Text = "-";
                        activityIndicator.IsRunning = false;
                        dichvu.Clear();
                    }
                    TIENPHONG_.Text = tienphong.tienPhong + " đ";
                    TIENNUOC_.Text = tienphong.tienNuoc + " đ";
                    TIENDIEN_.Text = tienphong.tienDien + " đ";
                    TIENDICHVU_.Text = tienphong.tienDichVu + " đ";
                    TONGTIEN_.Text = tienphong.tongTien + " đ";
                    xeSo.Text = tienphong.tienXeSo + " đ";
                    xeTayGa.Text = tienphong.tienXeTayGa + " đ";

                    var chitietDV = new List<Chitietdichvu>();
                    string CTDV = tienphong.chitietdichvu;
                    string[] arrayDV = CTDV.Split(',');
                    for (int i = 0; i < arrayDV.Length; i++)
                    {
                        if (arrayDV[i] != string.Empty)
                        {
                            chitietDV.Add(new Chitietdichvu(arrayDV[i]));
                        }
                    }
                    for (int j = 0; j < chitietDV.Count; j++)
                    {
                        lstCTDV.ItemsSource = dichvu;
                        dichvu.Add(new Chitietdichvu(chitietDV[j].chitietdichvu));
                    }
                    activityIndicator.IsRunning = false;
                }
                catch
                {
                    activityIndicator.IsRunning = false;
                    return;
                }
            }
        }

        #endregion

        private void BACK__Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnXem_Clicked(object sender, EventArgs e)
        {
            if (PHONG_.Text != null)
            {
                Xemthongtin();
                setcolorandSize();
            }
            else
            {
                DisplayAlert("Thông báo", "Chưa nhập số phòng", "OK");
                activityIndicator.IsRunning = false;

            }
        }
    }
}

public class Chitietdichvu
{
    public Chitietdichvu(string CTDV)
    {
        chitietdichvu = CTDV;
    }
    public string chitietdichvu { get; set; }
}
