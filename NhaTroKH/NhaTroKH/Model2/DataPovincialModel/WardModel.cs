using System;
namespace NhaTroKH.Model2.DataPovincialModel
{
    public class WardModel
    {
        // index xa trên firebase
        public string IndexWard;
        // index tỉnh firebase
        public string IndexProvincial;
        //index huyện trên firebase
        public string IndexDistrict;
        // mã huyện
        public string IDDistrict;
        // mã xã
        public string IdWard;
        // tên xã
        public string NameWard;

        public WardModel(
            string IndexWard,
            string IndexProvincial,
            string IndexDistrict,
            string IDDistrict,
            string IdWard,
            string NameWard)
        {
            this.IndexWard = IndexWard;
            this.IndexProvincial = IndexProvincial;
            this.IndexDistrict = IndexDistrict;
            this.IDDistrict = IDDistrict;
            this.IdWard = IdWard;
            this.NameWard = NameWard;
        }
    }
}
