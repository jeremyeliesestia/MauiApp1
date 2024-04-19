using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Constants = MauiApp1.Class.Constants;


namespace MauiApp1.Class
{
    public class SousMatieres
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int MatiereID { get; set; }
        public bool Done { get; set; }


    }

    public class SousMatieresDatabase
    {
        SQLiteAsyncConnection Database;

        public SousMatieresDatabase()
        {
        }

        async Task Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<SousMatieres>();
        }

        public async Task<List<SousMatieres>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<SousMatieres>().ToListAsync();
        }

        public async Task<List<SousMatieres>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<SousMatieres>().Where(t => !t.Done).ToListAsync();
        }

        public async Task<SousMatieres> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<SousMatieres>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(SousMatieres item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(SousMatieres item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }


        public async Task<int> DeleteAllItemsAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<SousMatieres>();
        }
    }


}
