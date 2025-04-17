using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Bll.DTOs;
using ToDoList.Bll.Service;
using ToDoList.Bll.Services;

namespace ToDoList.Server.Controller
{
    [Route("api/toDoList")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoListController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        [HttpPost("add")]
        public async Task<long> AddToDoItem(ToDoItemCreateDto toDoItemCreateDto)
        {
            var id = await _toDoItemService.AddToDoItemAsync(toDoItemCreateDto);
            return id;
        }

        [HttpDelete("delete")]
        public async Task DeleteToDoItemByIdAsync(long id)
        {
            await _toDoItemService.DeleteToDoItemByIdAsync(id);
        }

        [HttpGet("getCompleted")]
        public Task<List<ToDoItemGetDto>> GetCompletedAsync(int skip, int take)
        {
            return _toDoItemService.GetCompletedAsync(skip, take);
        }

        [HttpGet("getAll")]
        public async Task<List<ToDoItemGetDto>> GetAllToDoItemsAsync(int skip, int take)
        {
            return await _toDoItemService.GetAllToDoItemsAsync(skip, take);
        }

        [HttpGet("getById")]
        public async Task<ToDoItemGetDto> GetToDoItemByIdAsync(long id)
        {
            return await _toDoItemService.GetToDoItemByIdAsync(id);
        }

        [HttpGet("getByDueDate")]
        public Task<List<ToDoItemGetDto>> GetByDueDateAsync(DateTime dueDate)
        {
            return _toDoItemService.GetByDueDateAsync(dueDate);
        }

        [HttpGet("getIncompleted")]
        public Task<List<ToDoItemGetDto>> GetIncompleteAsync(int skip, int take)
        {
            return _toDoItemService.GetIncompleteAsync(skip, take);
        }

        [HttpGet("update")]
        public async Task UpdateToDoListAsync(ToDoItemUpdateDto updatingToDoList)
        {
            // Implementation for updating the ToDoList
            await _toDoItemService.UpdateToDoItemAsync(updatingToDoList);

        }
    }
}
