using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Data.Repositories
{
    public class ToDosRepository : IToDosRepository
    {
        private AppDbContext _appDbContext;

        public ToDosRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<ToDoItem>> GetAll()
        {
            return await _appDbContext.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem?> GetById(int id)
        {
            return await _appDbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(ToDoItem toDoItem)
        {           
            await _appDbContext.ToDoItems.AddAsync(toDoItem);
            await SaveChanges();
        }

        public async Task Update(int id, ToDoItem toDoItem)
        {
            var toDoFromDB = await _appDbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (toDoFromDB == null)
            {
                throw new ArgumentException($"No ToDoItem with such ID {id}");
            }
            toDoFromDB.Name = toDoItem.Name;
            await SaveChanges();
        }

        public async Task Delete(int id)
        {
            var toDoFromDB = await _appDbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (toDoFromDB == null)
            {
                throw new ArgumentException($"No ToDoItem with such ID {id}");
            }
            _appDbContext.ToDoItems.Remove(toDoFromDB);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _appDbContext.SaveChangesAsync ();
        }
    }
}
