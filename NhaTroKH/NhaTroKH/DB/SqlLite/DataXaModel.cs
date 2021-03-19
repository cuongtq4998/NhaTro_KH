using System;
using SQLite;

namespace NhaTroKH.DB.SqlLite
{
    [Table("TableXa")]
    public class DataXaModel
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        [NotNull]
        public string Text { get; set; } = "Xã thứ nhất"; // tên xã

        [NotNull]
        public DateTime Date { get; set; } = DateTime.Now;

        public string indexHuyen { get; set; } // index huyện

        public DataXaModel() { }

        public DataXaModel(string text, string indexHuyen)
        {
            this.Text = text;
            this.indexHuyen = indexHuyen;
        }
    }
}
