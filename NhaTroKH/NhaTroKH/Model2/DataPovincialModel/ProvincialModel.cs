using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaTroKH.Model2.DataPovincialModel
{
    public class ProvincialModel
    {
        //index Tỉnh
        public string IndexProvincial;
        // Mã tỉnh
        public string IDProvincial;
        // Tên tỉnh
        public string NameProvincial;

        // Kho

        public ProvincialModel(
            string IndexProvincial,
            string IDProvincial,
            string NameProvincial)
        {
            this.IndexProvincial = IndexProvincial;
            this.IDProvincial = IDProvincial;
            this.NameProvincial = NameProvincial;
        }
    }
}
