using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NhaTroKH.model;
using SQLite;

namespace NhaTroKH.DB
{
    public class DatabaseSQLite
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseSQLite(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<InformationAccountModel>().Wait();
        }

        public Task<List<InformationAccountModel>> GetPeopleAsync()
        {
            return _database.Table<InformationAccountModel>().ToListAsync();
        }

        public Task<int> SavePersonAsync(InformationAccountModel person)
        {
            return _database.InsertAsync(person);
        }

        public async Task DeleteItems(string idCard)
        { 
            var collectionItem = await _database.Table<InformationAccountModel>(). Where(x => x.IdCard == idCard).ToListAsync();
            if(collectionItem.Count > 0)
            {
                foreach (InformationAccountModel item in collectionItem)
                {
                    await _database.DeleteAsync(item);
                } 
            } 
        }
    }
}
