using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UTS_72210454.Data
{
    public class DatabaseHelper
    {
        string _dbPath;

        private SQLiteAsyncConnection _database;
        private async Task Init()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_dbPath);
            await _database.CreateTableAsync<Categories>();
        }
        public DatabaseHelper(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<Categories>> GetCategoriesAsync()
        {
            await Init();
            return await _database.Table<Categories>().ToListAsync();
        }

        public async Task addCategoryAsync(string name, string description)
        {
            int result = 0;
            await Init();
            result = await _database.InsertAsync(new Categories { name=name, description=description});
        }

        public async Task updateCategoryAsync(Categories category)
        {
            await Init();
            await _database.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await Init();
            var category = await _database.Table<Categories>().FirstOrDefaultAsync(c => c.categoryId == categoryId);
            if (category != null)
            {
                await _database.DeleteAsync(category);
            }
        }
    }
}
