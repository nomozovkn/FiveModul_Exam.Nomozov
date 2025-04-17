///////////////////////////////////////////////////////////////////////////////////
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using ToDoList.Dal.Entities;

using ToDoList.Repository.Settings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.Repository.ToDoItemRepository;

public class AdoNetToDoItemRepository : IToDoItemRepository
{
    private readonly string _connectionString;

    public AdoNetToDoItemRepository(SqlDBConeectionString sqlDBConeectionString)
    {
        _connectionString = sqlDBConeectionString.ConnectionString;
    }

    public async Task DeleteToDoItemByIdAsync(long id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_DeleteToDoItem", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ToDoItemId", id);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }


    public async Task<long> AddToDoItemAsync(ToDoItem toDoItem)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_AddToDoItem", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Title", toDoItem.Title);
            command.Parameters.AddWithValue("@Description", toDoItem.Description);
            //command.Parameters.AddWithValue("@IsCompleted", toDoItem.IsCompleted);
            command.Parameters.AddWithValue("@DueDate", toDoItem.DueDate);

            await connection.OpenAsync();
            object result = await command.ExecuteScalarAsync();
            //return Convert.ToInt64(result);
            return toDoItem.ToDoItemId;
        }
    }


    public async Task<ICollection<ToDoItem>> GetAllToDoItemsAsync(int skip, int take)
    {
        var items = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_GetAllToDoItems", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Skip", skip);
            command.Parameters.AddWithValue("@Take", take);

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    items.Add(MapToDoItem(reader));
                }
            }
        }

        return items;
    }


    public async Task<ICollection<ToDoItem>> GetByDueDateAsync(DateTime dueDate)
    {
        var items = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("SELECT * FROM fn_GetToDoItemsByDueDate(@DueDate)", connection))
        {
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@DueDate", dueDate.Date);

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                    items.Add(MapToDoItem(reader));
            }
        }

        return items;
    }


    public async Task<ICollection<ToDoItem>> GetCompletedAsync(int skip, int take)
    {
        return await SelectWithPaging("sp_GetCompleted", skip, take);
    }

    public async Task<ICollection<ToDoItem>> GetIncompleteAsync(int skip, int take)
    {
        return await SelectWithPaging("sp_GetIncompleted", skip, take);
    }

    public async Task<ToDoItem> GetToDoItemByIdAsync(long id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_GetToDoItemById", connection))
        {
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ToDoItemId", id);


            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                    return MapToDoItem(reader);

                return null;
            }
        }
    }


    public async Task UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_UpdateToDoItem", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ToDoItemId", toDoItem.ToDoItemId);
            command.Parameters.AddWithValue("@Title", toDoItem.Title);
            command.Parameters.AddWithValue("@Description", toDoItem.Description);
            command.Parameters.AddWithValue("@IsCompleted", toDoItem.IsCompleted);
            command.Parameters.AddWithValue("@DueDate", toDoItem.DueDate);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }

    private async Task<ICollection<ToDoItem>> SelectWithPaging(string storedProcedure, int skip, int take)
    {
        var items = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(storedProcedure, connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Skip", skip);
            command.Parameters.AddWithValue("@Take", take);

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                    items.Add(MapToDoItem(reader));
            }
        }

        return items;
    }

    private ToDoItem MapToDoItem(SqlDataReader reader)
    {
        return new ToDoItem
        {
            ToDoItemId = reader.GetInt64(reader.GetOrdinal("ToDoItemId")),
            Title = reader.GetString(reader.GetOrdinal("Title")),
            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
            IsCompleted = reader.GetBoolean(reader.GetOrdinal("IsCompleted")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate"))
        };
    }

    public async Task<ICollection<ToDoItem>> SearchToDoItemsAsync(string keyword)
    {
        var items = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_SearchToDoItems", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Keyword", keyword);

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    items.Add(MapToDoItem(reader));
                }
            }
        }

        return items;
    }




    public async Task<ICollection<ToDoItem>> SelectOverdueItemsAsync()
    {
        var items = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_GetOverdueToDoItems", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    items.Add(MapToDoItem(reader));
                }
            }
        }

        return items;
    }




    public async Task<ICollection<ToDoItem>> GetUpcomingDeadlinesAsync()
    {
        var items = new List<ToDoItem>();


        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("sp_GetUpcomingDeadlines", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    items.Add(MapToDoItem(reader));
                }
            }
        }

        return items;
    }

}



