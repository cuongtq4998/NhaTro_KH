using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NhaTroKH.DB.SqlLite;
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
            database.CreateTableAsync<DataHuyenModel>().Wait();
            database.CreateTableAsync<DataXaModel>().Wait();
        }

        /// <summary>
        /// get all
        /// </summary>
        /// <returns></returns>
        public Task<List<DataTinhModel>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<DataTinhModel>().ToListAsync();
        }
        public Task<List<DataHuyenModel>> GetHuyenAsync()
        {
            //Get all notes.
            return database.Table<DataHuyenModel>().ToListAsync();
        }
        public Task<List<DataXaModel>> GetXaAsync()
        {
            //Get all notes.
            return database.Table<DataXaModel>().ToListAsync();
        }


        /// <summary>
        ///  get theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<DataTinhModel> GetNoteAsyncID(int id)
        {
            // Get a specific note.
            return database.Table<DataTinhModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Task<DataHuyenModel> GetHuyenAsyncID(string index)
        {
            // Get a specific note.
            return database.Table<DataHuyenModel>()
                            .Where(i => i.indexHuyen == index)
                            .FirstOrDefaultAsync();
        }
        public Task<DataXaModel> GetXaAsyncID(int id)
        {
            // Get a specific note.
            return database.Table<DataXaModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// get danh sach huyen, xa
        /// </summary>
        /// <returns></returns>
        public Task<List<DataHuyenModel>> GetListHuyenAsync(string indexTinh)
        {
            return database.Table<DataHuyenModel>()
                .Where(i => i.indexTinh == indexTinh)
                .OrderBy(x => x.Text) 
                .ToListAsync();
        }

        public Task<List<DataXaModel>> GetListXaAcync(string indexHuyen)
        {
            return database.Table<DataXaModel>()
               .Where(i => i.indexHuyen == indexHuyen)
               .OrderBy(x => x.Text)
               .ToListAsync();
        }

        /// <summary>
        /// save data db
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public Task<int> SaveNoteAsync(DataTinhModel note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }
        public Task<int> SaveHuyenAsync(DataHuyenModel note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }
        public Task<int> SaveXaAsync(DataXaModel note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }
        /// <summary>
        ///  delete db
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public Task<int> DeleteNoteAsync(DataTinhModel note)
        { 
            return database.DeleteAsync(note);
        }

        public Task<int> DeleteAll()
        {
            return database.ExecuteAsync("DELETE FROM TableTinh");
        }

        public Task<int> DeleteDistrictAsync(DataHuyenModel huyenData)
        {
            return database.DeleteAsync(huyenData);
        }

        public Task<int> DeleteDistrictAsync(DataXaModel xaData)
        {
            return database.DeleteAsync(xaData);
        }
    }
}
