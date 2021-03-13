using System;
using System.Collections.Generic;
using System.Text;

namespace NhaTroKH.Model
{
    public class abc
    {
        public int MaTinh { get; set; }
        public string TenTinh { get; set; }
    }
    public class Tinh
    {
        public Tinh(string maTinh, string tenTinh)
        {
            matinh = maTinh;
            tentinh = tenTinh;
        }

        private string matinh;
        public string MaTinh
        {
            get { return matinh; }
            set { matinh = value; }
        }
        private string tentinh;



        public string TenTinh
        {
            get { return tentinh; }
            set { tentinh = value; }
        }
    }
    public class QuanHuyen
    {
        private int mahuyen;
        public int MaQuanHuyen
        {
            get { return mahuyen; }
            set { mahuyen = value; }
        }
        private string tenquan;
        public string TenQuanHuyen
        {
            get { return tenquan; }
            set { tenquan = value; }
        }
    }
    public class PhuongXa
    {
        private int maxa;
        public int maPhuongXa
        {
            get { return maxa; }
            set { maxa = value; }
        }
        private string tenxa;
        public string TenXaPhuong
        {
            get { return tenxa; }
            set { tenxa = value; }
        }
    }
    public class listquatrinh
    {

        public listquatrinh(int iD, string nGHENGHIEP, string nOILAMVIEC, string cHO_O, DateTime? tUTHANGNAM, DateTime? dENTHANGNAM)
        {
            ID = iD;
            nghenghiep = nGHENGHIEP;
            noilamviec = nOILAMVIEC;
            choo = cHO_O;
            tungay = tUTHANGNAM;
            denngay = dENTHANGNAM;
        }

        private System.Nullable<System.DateTime> _tungay;
        public DateTime? tungay { get => _tungay; set => _tungay = value; }

        private System.Nullable<System.DateTime> _denngay;

        public DateTime? denngay { get => _denngay; set => _denngay = value; }
        public int ID
        {
            get;
            set;
        }
        public string nghenghiep
        {
            get;
            set;
        }
        public string noilamviec
        {
            get;
            set;
        }
        public string choo
        {
            get;
            set;
        }
    }
    public class listgiadinh
    {

        public listgiadinh(int iD, string hOTEN, DateTime? nGAYSINH, string gIOITINH, string qUANHE, string nGHENGHIEP, string cHO_O)
        {
            id = iD;
            hoten = hOTEN;
            ngaysinh = nGAYSINH;
            gioitinh = gIOITINH;
            quanhe = qUANHE;
            nghenghiep = nGHENGHIEP;
            choo = cHO_O;
        }

        public int id
        {
            get;
            set;
        }

        private System.Nullable<System.DateTime> _ngaysinh;
        public DateTime? ngaysinh { get => _ngaysinh; set => _ngaysinh = value; }
        public string hoten
        {
            get;
            set;
        }
        public string gioitinh
        {
            get;
            set;
        }
        public string quanhe
        {
            get;
            set;
        }
        public string nghenghiep
        {
            get;
            set;
        }
        public string choo
        {
            get;
            set;
        }
    }
}
