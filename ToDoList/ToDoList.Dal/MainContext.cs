using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Dal.Configuraton;
using ToDoList.Dal.Entities;

namespace ToDoList.Dal;

public class MainContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ToDoItemConfigurations());
    }
}
