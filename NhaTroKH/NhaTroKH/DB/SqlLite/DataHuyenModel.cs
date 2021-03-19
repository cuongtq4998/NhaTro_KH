using System;
using SQLite;

namespace NhaTroKH.DB.SqlLite
{
    [Table("TableHuyen")]
    public class DataHuyenModel
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        [NotNull]
        public string Text { get; set; } = "Huyện thứ nhất"; // tên huyện

        [NotNull]
        public DateTime Date { get; set; } = DateTime.Now;

        public string indexHuyen { get; set; } // index huyện

        public string indexTinh { get; set; } // index tỉnh

        public DataHuyenModel() { }

        public DataHuyenModel(string text, string indexHuyen, string indexTinh)
        {
            this.Text = text;
            this.indexHuyen = indexHuyen;
            this.indexTinh = indexTinh;
        }
    }
}
