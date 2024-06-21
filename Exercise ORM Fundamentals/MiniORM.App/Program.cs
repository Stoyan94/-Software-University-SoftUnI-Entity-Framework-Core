
using MiniORM;
using MiniORM.App.Entities;

var departments = new Department[] { 
    new Department { Id = 1, Name = "First"}, 
    new Department { Id = 2, Name = "Second" } };

var changeTracker = new ChangeTracker<Department>(departments);

Console.WriteLine();