using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {

        #region " Variables "

        private SQLiteAsyncConnection _connection;

        #endregion

        #region " Events "

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;

        #endregion

        #region " Public Methods "

        public async Task<List<TodoItem>> GetItems()
        {
            await CreateConnection();
            return await _connection.Table<TodoItem>().ToListAsync();
        }

        public async Task AddItem(TodoItem item)
        {
            await CreateConnection();
            await _connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task UpdateItem(TodoItem item)
        {
            await CreateConnection();
            await _connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }

        public async Task AddOrUpdate(TodoItem item)
        {
            if(item.Id == 0)
            {
                await AddItem(item: item);
            }
            else
            {
                await UpdateItem(item: item);
            }
            
        }

        #endregion

        #region " Private Methods "

        private async Task CreateConnection()
        {
            if(_connection != null)
            {
                return;
            }
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "TodoItems.db");
            _connection = new SQLiteAsyncConnection(databasePath);
            await _connection.CreateTableAsync<TodoItem>();

            if(await _connection.Table<TodoItem>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new TodoItem() { Title = "Welcome to ToDo!" });
            }
        }

        #endregion

    }
}
