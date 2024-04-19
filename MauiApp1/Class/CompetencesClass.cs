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
    public class Competences
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int SousMatiereID { get; set; }
        public bool Done { get; set; }

    }

    public class CompetencesDatabase
    {
        SQLiteAsyncConnection Database;

        public CompetencesDatabase()
        {
        }

        async Task Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Competences>();
        }

        public async Task<List<Competences>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Competences>().ToListAsync();
        }

        public async Task<List<Competences>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<Competences>().Where(t => !t.Done).ToListAsync();
        }

        public async Task<Competences> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Competences>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Competences item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Competences item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }


        public async Task<int> DeleteAllItemsAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<Competences>();
        }
    }

}
