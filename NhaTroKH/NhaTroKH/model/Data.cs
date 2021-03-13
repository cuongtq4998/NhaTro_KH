using System;
using System.Collections.Generic;
using System.Text;

namespace NhaTroKH.Models
{
    public class Data
    {
        #region name
        private int _MA_KHACH;

        private string _TEN_KHACH = "";

        private string _CMND = "";

        private System.Nullable<System.DateTime> _NGAY_CAP = DateTime.Now;

        private System.Nullable<System.DateTime> _NGAY_SINH = DateTime.Now;

        private string _GIOI_TINH = "Nam";

        private string _QUE_QUAN = "";

        private string _SDT = "";


        private string _GHI_CHU = "";

        private string _NOITHUONGTHU = "";

        private string _DIACHIHIENNAY = "";

        private string _NGHENGHIEP = "";

        private string _NOILAMVIEC = "";

        private string _EMAIL = "";

        private string _BIENSOXE = "";

        private string _LOAIXE = "";

        private string _DANTOC = "";

        private string _TONGIAO = "";

        private string _TRINHDOHOCVAN = "";

        private string _TRINHDOCHUYENMON = "";

        private string _CongTy = "";

        #endregion

        #region properti
        public int MA_KHACH
        {
            get
            {
                return this._MA_KHACH;
            }
            set
            {
                if ((this._MA_KHACH != value))
                {

                    this._MA_KHACH = value;

                }
            }
        }


        public string TEN_KHACH
        {
            get
            {
                return this._TEN_KHACH;
            }
            set
            {
                if ((this._TEN_KHACH != value))
                {

                    this._TEN_KHACH = value;

                }
            }
        }


        public string CMND
        {
            get
            {
                return this._CMND;
            }
            set
            {
                if ((this._CMND != value))
                {

                    this._CMND = value;

                }
            }
        }


        public System.Nullable<System.DateTime> NGAY_CAP
        {
            get
            {
                return this._NGAY_CAP;
            }
            set
            {
                if ((this._NGAY_CAP != value))
                {

                    this._NGAY_CAP = value;

                }
            }
        }


        public System.Nullable<System.DateTime> NGAY_SINH
        {
            get
            {
                return this._NGAY_SINH;
            }
            set
            {
                if ((this._NGAY_SINH != value))
                {

                    this._NGAY_SINH = value;

                }
            }
        }


        public string GIOI_TINH
        {
            get
            {
                return this._GIOI_TINH;
            }
            set
            {
                if ((this._GIOI_TINH != value))
                {

                    this._GIOI_TINH = value;

                }
            }
        }


        public string QUE_QUAN
        {
            get
            {
                return this._QUE_QUAN;
            }
            set
            {
                if ((this._QUE_QUAN != value))
                {

                    this._QUE_QUAN = value;

                }
            }
        }


        public string SDT
        {
            get
            {
                return this._SDT;
            }
            set
            {
                if ((this._SDT != value))
                {

                    this._SDT = value;

                }
            }
        }

        public string GHI_CHU
        {
            get
            {
                return this._GHI_CHU;
            }
            set
            {
                if ((this._GHI_CHU != value))
                {

                    this._GHI_CHU = value;

                }
            }
        }


        public string NOITHUONGTHU
        {
            get
            {
                return this._NOITHUONGTHU;
            }
            set
            {
                if ((this._NOITHUONGTHU != value))
                {

                    this._NOITHUONGTHU = value;

                }
            }
        }

        public string DIACHIHIENNAY
        {
            get
            {
                return this._DIACHIHIENNAY;
            }
            set
            {
                if ((this._DIACHIHIENNAY != value))
                {

                    this._DIACHIHIENNAY = value;

                }
            }
        }


        public string NGHENGHIEP
        {
            get
            {
                return this._NGHENGHIEP;
            }
            set
            {
                if ((this._NGHENGHIEP != value))
                {

                    this._NGHENGHIEP = value;

                }
            }
        }


        public string NOILAMVIEC
        {
            get
            {
                return this._NOILAMVIEC;
            }
            set
            {
                if ((this._NOILAMVIEC != value))
                {

                    this._NOILAMVIEC = value;

                }
            }
        }


        public string EMAIL
        {
            get
            {
                return this._EMAIL;
            }
            set
            {
                if ((this._EMAIL != value))
                {

                    this._EMAIL = value;

                }
            }
        }



        public string BIENSOXE
        {
            get
            {
                return this._BIENSOXE;
            }
            set
            {
                if ((this._BIENSOXE != value))
                {

                    this._BIENSOXE = value;

                }
            }
        }



        public string LOAIXE
        {
            get
            {
                return this._LOAIXE;
            }
            set
            {
                if ((this._LOAIXE != value))
                {

                    this._LOAIXE = value;

                }
            }
        }



        public string DANTOC
        {
            get
            {
                return this._DANTOC;
            }
            set
            {
                if ((this._DANTOC != value))
                {

                    this._DANTOC = value;

                }
            }
        }



        public string TONGIAO
        {
            get
            {
                return this._TONGIAO;
            }
            set
            {
                if ((this._TONGIAO != value))
                {

                    this._TONGIAO = value;

                }
            }
        }



        public string TRINHDOHOCVAN
        {
            get
            {
                return this._TRINHDOHOCVAN;
            }
            set
            {
                if ((this._TRINHDOHOCVAN != value))
                {

                    this._TRINHDOHOCVAN = value;

                }
            }
        }



        public string TRINHDOCHUYENMON
        {
            get
            {
                return this._TRINHDOCHUYENMON;
            }
            set
            {
                if ((this._TRINHDOCHUYENMON != value))
                {

                    this._TRINHDOCHUYENMON = value;

                }
            }
        }

        public string CongTy
        {
            get
            {
                return this._CongTy;
            }
            set
            {
                if(this._CongTy != value)
                {
                    this._CongTy = value;
                }
            }
        }
        #endregion
    }

    public class KHACH_THUE_GIA_DINH
    {
        #region name
        private int _ID;
        private string _HOTEN;

        private System.Nullable<System.DateTime> _NGAYSINH;

        private string _GIOITINH;

        private string _QUANHE;

        private string _NGHENGHIEP;

        private string _DIACHICHOOHIENNAY;
        #endregion
        #region properti
        public int ID { get => _ID; set => _ID = value; }
        public string HOTEN { get => _HOTEN; set => _HOTEN = value; }
        public DateTime? NGAYSINH { get => _NGAYSINH; set => _NGAYSINH = value; }
        public string GIOITINH { get => _GIOITINH; set => _GIOITINH = value; }
        public string QUANHE { get => _QUANHE; set => _QUANHE = value; }
        public string NGHENGHIEP { get => _NGHENGHIEP; set => _NGHENGHIEP = value; }
        public string DIACHICHOOHIENNAY { get => _DIACHICHOOHIENNAY; set => _DIACHICHOOHIENNAY = value; }
        #endregion
    }

    public class KHACH_THUE_QUA_TRINH
    {
        #region name
        private int _ID;
        private System.Nullable<System.DateTime> _TUTHANGNAM;

        private System.Nullable<System.DateTime> _DENTHANGNAM;

        private string _CHO_O;

        private string _NGHENGHIEP;

        private string _NOILAMVIEC;
        #endregion

        #region properti
        public DateTime? TUTHANGNAM { get => _TUTHANGNAM; set => _TUTHANGNAM = value; }
        public DateTime? DENTHANGNAM { get => _DENTHANGNAM; set => _DENTHANGNAM = value; }
        public int ID { get => _ID; set => _ID = value; }
        public string CHO_O { get => _CHO_O; set => _CHO_O = value; }
        public string NGHENGHIEP { get => _NGHENGHIEP; set => _NGHENGHIEP = value; }
        public string NOILAMVIEC { get => _NOILAMVIEC; set => _NOILAMVIEC = value; }
        #endregion
    }

    public class LOGIN
    {
        private string _CMND;
        private string _MATKHAU;

        public string CMND_TK { get => _CMND; set => _CMND = value; }
        public string MATKHAU_TK { get => _MATKHAU; set => _MATKHAU = value; }

    }

    public class DIENNUOC
    {
        private string _SO_DIEN;
        private string _SO_NUOC;
        private string _SO_XE_SO_KHACH;
        private string _SO_XE_TAY_GA_KHACH;

        public string SO_DIEN { get => _SO_DIEN; set => _SO_DIEN = value; }
        public string SO_NUOC { get => _SO_NUOC; set => _SO_NUOC = value; }
        public string SO_XE_SO_KHACH { get => _SO_XE_SO_KHACH; set => _SO_XE_SO_KHACH = value; }
        public string SO_XE_TAY_GA_KHACH { get => _SO_XE_TAY_GA_KHACH; set => _SO_XE_TAY_GA_KHACH = value; }
    }

    public class FEEDBACK
    {
        private string _CMND;
        private System.Nullable<System.DateTime> _Ngay = DateTime.Now;
        private string _Phong;
        private string _STT;
        private string _TrangThai;
        private string _PhanHoi;

        public string CMND { get => _CMND; set => _CMND = value; }
        public System.Nullable<System.DateTime> Ngay
        {
            get
            {
                return this._Ngay;
            }
            set
            {
                if ((this._Ngay != value))
                {

                    this._Ngay = value;

                }
            }
        }
        public string Phong { get => _Phong; set => _Phong = value; }
        public string STT { get => _STT; set => _STT = value; }
        public string TrangThai { get => _TrangThai; set => _TrangThai = value; }
        public string PhanHoi { get => _PhanHoi; set => _PhanHoi = value; }
    }

    public class THONGTINTIENPHONG
    {
        private string _chitietdichvu;
        private string _TIENPHONG;
        private string _TIENNUOC;
        private string _TIENDIEN;
        private string _TIENDICHVU;
        private string _TONGTIEN;
        private string _tienXeTayGa;
        private string _tienXeSo;

        public string tienPhong { get => _TIENPHONG; set => _TIENPHONG = value; }
        public string tienNuoc { get => _TIENNUOC; set => _TIENNUOC = value; }
        public string tienDien { get => _TIENDIEN; set => _TIENDIEN = value; }
        public string tienDichVu { get => _TIENDICHVU; set => _TIENDICHVU = value; }
        public string tongTien { get => _TONGTIEN; set => _TONGTIEN = value; }
        public string tienXeTayGa { get => _tienXeTayGa; set => _tienXeTayGa = value; }
        public string tienXeSo { get => _tienXeSo; set => _tienXeSo = value; }
        public string chitietdichvu { get => _chitietdichvu; set => _chitietdichvu = value; }
    }


}
