using ToDoAPI.Models;

namespace ToDoAPI.Data.Repositories
{
    public interface IToDosRepository
    {
        Task<IEnumerable<ToDoItem>> GetAll();
        Task<ToDoItem?> GetById(int id);

        Task Add(ToDoItem toDoItem);

        Task Update(int id, ToDoItem toDoItem);

        Task Delete(int id);

        Task SaveChanges();
    }
}
