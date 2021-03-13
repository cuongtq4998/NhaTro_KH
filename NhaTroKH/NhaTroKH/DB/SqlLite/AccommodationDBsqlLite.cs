using System;
using SQLite;

namespace NhaTroKH.Database
{
    public class AccommodationDBsqlLite
    {
        /// <summary>
        ///  declare data async connection
        /// </summary>
        readonly SQLiteAsyncConnection database;

        public AccommodationDBsqlLite(string strPath)
        {
            /// innit
            database = new SQLiteAsyncConnection(strPath);
            /// create table type: datatinhmodel
            database.CreateTableAsync<DataTinhModel>().Wait();
        }
    }
}
