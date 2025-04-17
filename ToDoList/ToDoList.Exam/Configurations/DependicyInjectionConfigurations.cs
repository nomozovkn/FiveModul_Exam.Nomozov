using FluentValidation;
using ToDoList.Bll.DTOs;
using ToDoList.Bll.Service;
using ToDoList.Bll.Services;
using ToDoList.Bll.Validators;
using ToDoList.Repository.ToDoItemRepository;

namespace ToDoList.Server.Configurations;

public static class DependicyInjectionConfigurations
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IToDoItemRepository, AdoNetToDoItemRepository>();
        builder.Services.AddScoped<IToDoItemService, ToDoItemService>();

        builder.Services.AddScoped<ToDoItemUpdateDtoValidator, ToDoItemUpdateDtoValidator>();
        builder.Services.AddScoped<ToDoItemCreateDtoValidator, ToDoItemCreateDtoValidator>();

        builder.Services.AddScoped<IValidator<ToDoItemCreateDto>, ToDoItemCreateDtoValidator>();
    }
}
