using Microsoft.EntityFrameworkCore;
using ToDoList.Dal;
using ToDoList.Dal.Entities;


namespace ToDoList.Repository.ToDoItemRepository;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly MainContext MainContext;

    public ToDoItemRepository(MainContext mainDbContext)
    {
        MainContext = mainDbContext;
    }

    public async Task DeleteToDoItemByIdAsync(long id)
    {
        var toDoItem = await GetToDoItemByIdAsync(id);
        MainContext.ToDoItems.Remove(toDoItem);
        await MainContext.SaveChangesAsync();
    }

    public async Task<long> AddToDoItemAsync(ToDoItem toDoItem)
    {
        await MainContext.ToDoItems.AddAsync(toDoItem);
        await MainContext.SaveChangesAsync();
        return toDoItem.ToDoItemId;

    }

    public async Task<ICollection<ToDoItem>> GetAllToDoItemsAsync(int skip, int take)
    {
        if (skip < 0 || take <= 0)
        {
            throw new ArgumentOutOfRangeException("Skip and take must be non-negative and take must be greater than zero.");
        }
        return await MainContext.ToDoItems
              .Skip(skip)
              .Take(take)
              .ToListAsync();
    }

    public async Task<ICollection<ToDoItem>> GetByDueDateAsync(DateTime dueTime)
    {
        var query = MainContext.ToDoItems
            .Where(t => t.DueDate.Date == dueTime);
        return await query.ToListAsync();
    }

    public async Task<ICollection<ToDoItem>> GetCompletedAsync(int skip, int take)
    {
        if (skip < 0 || take <= 0)
        {
            throw new ArgumentOutOfRangeException("Skip and take must be non-negative and take must be greater than zero.");
        }

        var query = MainContext.ToDoItems
            .Where(t => t.IsCompleted)
            .Skip(skip)
            .Take(take);

        return await query.ToListAsync();
    }

    public async Task<ICollection<ToDoItem>> GetIncompleteAsync(int skip, int take)
    {
        if (skip < 0 || take <= 0)
        {
            throw new ArgumentOutOfRangeException("Skip and take must be non-negative and take must be greater than zero.");
        }
        var query = MainContext.ToDoItems
            .Where(t => !t.IsCompleted)
            .Skip(skip)
            .Take(take);

        return await query.ToListAsync();
    }

    public async Task<ToDoItem> GetToDoItemByIdAsync(long id)
    {
        var toDoItem = await MainContext.ToDoItems.FirstOrDefaultAsync(x => x.ToDoItemId == id);

        return toDoItem;
    }

    public async Task UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        MainContext.ToDoItems.Update(toDoItem);
        await MainContext.SaveChangesAsync();
    }

   
}

