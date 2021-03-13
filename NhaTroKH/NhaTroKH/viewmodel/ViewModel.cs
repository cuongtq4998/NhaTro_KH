using NhaTroKH.Model;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhaTroKH.ViewModel
{
    public class BusinessObject
    {
        public BusinessObject(string name, string imageSource)
        {
            this.Name = name;
            this.ImageSource = imageSource;
        }

        public string Name { get; set; }

        public string ImageSource { get; set; }
    }
    public class Quan
    {
        public Quan(string matinh, string tentinh)
        {
            this.MaTinh = matinh;
            this.TenTinh = tentinh;
        }

        public string MaTinh { get; set; }

        public string TenTinh { get; set; }
    }
    public class ViewModel
    {
        static IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "mJh3ttHegM5JEw4I8KKbreCWxcmVjIrcM5I9fhjx",
            BasePath = "https://nhatro-271ce.firebaseio.com/"

        };
        public async void gethuyenAsync()
        {
            //activityIndicator1.IsRunning = true;
            //lblLoadingText.IsVisible = true;  

            IFirebaseClient clientKT = new FireSharp.FirebaseClient(config);
            FirebaseResponse tk1 = await clientKT.GetAsync("TinhThanh/" + 1);
            Tinh _tinh1 = tk1.ResultAs<Tinh>();
            Source = new List<Quan>()
                {
                    new Quan(_tinh1.MaTinh, _tinh1.TenTinh)
                };
        }
        public ViewModel()
        {

            gethuyenAsync();
            //chuoi_44.Items.Add(_tinh1.TenTinh);
            //chuoi_444.Items.Add(_tinh1.TenTinh);
            //chuoi_4_4.Items.Add(_tinh1.TenTinh); 

            //Source = new List<Quan>()
            //{ 
            //    new Quan("Freda Curtis", "available32.png"),
            //    new Quan("Jeffery Francis", "away32.png"),
            //    new Quan("Eva Lawson", "available32.png"),
            //    new Quan("Emmett Santos", "away32.png")
            //};

        }

        public List<Quan> Source { get; set; }
    }
}
