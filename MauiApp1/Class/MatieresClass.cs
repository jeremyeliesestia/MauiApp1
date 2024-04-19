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
    public class Matieres
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }

    }

    public class MatieresDatabase
    {
        SQLiteAsyncConnection Database;

        public MatieresDatabase()
        {
        }

        async Task Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Matieres>();
        }

        public async Task<List<Matieres>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Matieres>().ToListAsync();
        }

        public async Task<List<Matieres>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<Matieres>().Where(t => !t.Done).ToListAsync();
        }

        public async Task<Matieres> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Matieres>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Matieres item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Matieres item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }


        public async Task<int> DeleteAllItemsAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<Matieres>();
        }
    }

}
