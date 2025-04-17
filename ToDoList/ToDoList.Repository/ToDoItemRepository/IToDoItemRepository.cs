using ToDoList.Dal.Entities;

namespace ToDoList.Repository.ToDoItemRepository;

public interface IToDoItemRepository
{
    Task<long> AddToDoItemAsync(ToDoItem toDoItem);
    Task DeleteToDoItemByIdAsync(long id);
    Task UpdateToDoItemAsync(ToDoItem toDoItem);
    Task<ICollection<ToDoItem>> GetAllToDoItemsAsync(int skip, int take);
    Task<ToDoItem> GetToDoItemByIdAsync(long id);
    Task<ICollection<ToDoItem>> GetByDueDateAsync(DateTime dueDate);
    Task<ICollection<ToDoItem>> GetCompletedAsync(int skip, int take);
    Task<ICollection<ToDoItem>> GetIncompleteAsync(int skip, int take);
}