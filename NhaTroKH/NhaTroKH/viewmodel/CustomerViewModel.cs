using NhaTroKH.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NhaTroKH.ViewModel
{
    public class CustomerViewModel
    {
        private ObservableCollection<Hocvan> hocvancolection;
        public ObservableCollection<Hocvan> Hocvancolection
        {
            get { return hocvancolection; }
            set { hocvancolection = value; }
        }
        public CustomerViewModel()
        {
            hocvancolection = new ObservableCollection<Hocvan>();
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Tiến sĩ" });
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Thạc sĩ" });
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Đại học" });
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Cao đăng" });
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Trung cấp" });
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Tốt nghiệp phổ thông trung học" });
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Tốt nghiệp phổ thông cơ sở" });
            hocvancolection.Add(new Hocvan() { Image = "muiten.png", Tenhocvan = "Không biết chữ" });
        }
    }
}
