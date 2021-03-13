using System;
using System.Collections.Generic;
using System.Text;

namespace NhaTroKH.Model
{
    public class khacthuegiadinh
    {
        public khacthuegiadinh(string hOTEN, string nGAYSINH, string gIOITINH, string qUANHE, string nGHENGHIEP, string dIACHICHOOHIENNAY)
        {
            HOTEN = hOTEN;
            NGAYSINH = nGAYSINH;
            GIOITINH = gIOITINH;
            QUANHE = qUANHE;
            NGHENGHIEP = nGHENGHIEP;
            DIACHICHOOHIENNAY = dIACHICHOOHIENNAY;
        }

        public string HOTEN
        {
            get;
            set;
        }
        public string NGHENGHIEP
        {
            get;
            set;
        }
        public string GIOITINH
        {
            get;
            set;
        }
        public string NGAYSINH
        {
            get;
            set;
        }
        public string QUANHE
        {
            get;
            set;
        }
        public string DIACHICHOOHIENNAY
        {
            get;
            set;
        }
    }
}
