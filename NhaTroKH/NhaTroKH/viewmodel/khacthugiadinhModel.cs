using NhaTroKH.Model; 
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace NhaTroKH.ViewModel
{
    class khacthugiadinhModel
    {
        static IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "mJh3ttHegM5JEw4I8KKbreCWxcmVjIrcM5I9fhjx",
            BasePath = "https://nhatro-271ce.firebaseio.com/" 
        };
        public ObservableCollection<khacthuegiadinh> Khachthuegiadinh
        {
            get;
            set;
        }
        public Command<khacthuegiadinh> Xoadanhsach
        {
            get
            {
                return new Command<khacthuegiadinh>((khacthuegiadinh) =>
                {
                    Khachthuegiadinh.Remove(khacthuegiadinh);
                });
            }

        }
        public khacthugiadinhModel()
        {
            getgiadinh();
            //Khachthuegiadinh = new ObservableCollection<khacthuegiadinh>
            //{
            //    new khacthuegiadinh
            //    {
            //        HOTEN ="Trần Quốc Cường",
            //        NGHENGHIEP = "Bán hàng rông"
            //    }
            //};
        }
        //public async void gethuyenAsync()
        //{
        //    //activityIndicator1.IsRunning = true;
        //    //lblLoadingText.IsVisible = true;  

        //    IFirebaseClient clientKT = new FireSharp.FirebaseClient(config);
        //    FirebaseResponse tk1 = await clientKT.GetAsync("TinhThanh/" + 1);
        //    Tinh _tinh1 = tk1.ResultAs<Tinh>();
        //    Source = new List<Quan>()
        //        {
        //            new Quan(_tinh1.MaTinh, _tinh1.TenTinh)
        //        };
        //}
        public async void getgiadinh()
        {

            ObservableCollection<khacthuegiadinh> manggiadinh = new ObservableCollection<khacthuegiadinh>();
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            int i = 0;
            bool dem = true;
            FirebaseResponse tk = await client.GetAsync("khachthue/" + 33956565 + "/KHACHTHUEGIADINH/" + 1 + "/");
            //if (tk.Body == "null")
            //{
            //    dem = false;
            //}
            khacthuegiadinh GD = tk.ResultAs<khacthuegiadinh>();

            Khachthuegiadinh = new ObservableCollection<khacthuegiadinh>()
                    {
                        new khacthuegiadinh(
                            GD.HOTEN,
                            GD.NGAYSINH,
                            GD.GIOITINH,
                            GD.QUANHE,
                            GD.NGHENGHIEP,
                            GD.DIACHICHOOHIENNAY)
                };


        }
    }
}
