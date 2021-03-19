using System;
using SQLite;

namespace NhaTroKH.Database
{
    [Table("TableTinh")]
    public class DataTinhModel
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        
        [NotNull]
        public string Text { get; set; } = "Tỉnh thứ nhất"; //  tên tỉnh

        [NotNull]
        public DateTime Date { get; set; } = DateTime.Now;
         
        public string indexTinh { get; set; } // index tỉnh

        public DataTinhModel() {}

        public DataTinhModel(string text, string indexTinh)
        {
            this.Text = text;
            this.indexTinh = indexTinh;
        }
    }
}
