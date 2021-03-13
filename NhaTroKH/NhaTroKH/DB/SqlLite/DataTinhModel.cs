using System;
using SQLite;

namespace NhaTroKH.Database
{
    public class DataTinhModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime  dateTime { get; set; }
    }
}
