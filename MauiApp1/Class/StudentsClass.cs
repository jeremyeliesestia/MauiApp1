using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Constants = MauiApp1.Class.Constants;

namespace MauiApp1
{


    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public bool Done { get; set; }
    }


    public class StudentDatabase
    {
        SQLiteAsyncConnection Database;

        public StudentDatabase()
        {
        }

        async Task Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Student>();
        }

        public async Task<List<Student>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Student>().ToListAsync();
        }

        public async Task<List<Student>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<Student>().Where(t => !t.Done).ToListAsync();
        }

        public async Task<Student> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Student>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Student item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Student item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }


        public async Task<int> DeleteAllItemsAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<Student>();
        }
    }


}
