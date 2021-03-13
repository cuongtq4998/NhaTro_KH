using System;
namespace NhaTroKH.Model
{
    public class TinhModel
    {
        private int ID;
        private string TenTinh;

        public TinhModel(int id, string tentinh)
        {
            this.ID = id;
            this.TenTinh = tentinh;
        }

        public int getID
        {
            get { return ID; }
            set { ID = value; }
        }

        public string getTenTinh
        {
            get { return TenTinh; }
            set
            {
                TenTinh = value;
            }
        }
    }
}
