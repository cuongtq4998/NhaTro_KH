using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaTroKH.Model2.DataPovincialModel
{
    public class DistrictModel
    {
        //index district firebase
        public string IndexDistrict;
        //index provincial firebase
        public string IndexProvincial;
        // Mã Tỉnh
        public string IDProvincial;
        // Mã Huyện
        public string IDDistrict;
        // Tên Huyện
        public string NameDistrict;

        public DistrictModel(
            string IndexDistrict,
            string IndexProvincial,
            string IDProvincial,
            string IDDistrict,
            string NameDistrict
            )
        {
            this.IndexDistrict = IndexDistrict;
            this.IndexProvincial = IndexProvincial;
            this.IDProvincial = IDProvincial;
            this.IDDistrict = IDDistrict;
            this.NameDistrict = NameDistrict;
        }
    }
}
