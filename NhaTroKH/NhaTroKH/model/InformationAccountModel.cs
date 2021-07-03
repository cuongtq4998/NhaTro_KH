using System;
using SQLite;

namespace NhaTroKH.model
{
    public class InformationAccountModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string IdCard { get; set; }
        public string PassWord { get; set; }
        public bool IsSaveAccount { get; set; }
    }
}
