using Microsoft.EntityFrameworkCore;
using SHADotNetCore.ToDoList.Models;
using System.Runtime.CompilerServices;
using Task = SHADotNetCore.ToDoList.Models.Task;

namespace SHADotNetCore.ToDoList.Endpoints.TaskList
{
    public static class TaskListEndpoint
    {
      
        public static IEndpointRouteBuilder MapTaskEndpoint(this IEndpointRouteBuilder app)

        {
            app.MapControllers();

            app.MapGet("/tasks",async  (AppDbContext db) =>
            {
                var items = db.Tasks.AsNoTracking().ToList();
                return Results.Ok(items);
            }).WithName("GetTasks").WithOpenApi();

            app.MapGet("/tasks/{id}", (int id, AppDbContext db) =>
            {
                var item = db.Tasks.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found");
                }
                return Results.Ok(item);
            }).WithName("GetTask").WithOpenApi();

            app.MapPost("/tasks", (Task task, AppDbContext db) =>
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return Results.Ok(task);
            }).WithName("CreateTasks").WithOpenApi();

            app.MapPut("/tasks/{id}", (int id, Task task, AppDbContext db) =>
            {
                var item = db.Tasks.FirstOrDefault(x => x.TaskId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found");
                }
                item.TaskTitle = task.TaskTitle;
                item.TaskDescription = task.TaskDescription;
                item.TaskStatus = task.TaskStatus;
                item.DueDate = task.DueDate;
                item.CompletedDate = task.CompletedDate;
                item.CategoryId = task.CategoryId;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(item);
            }).WithName("UpdateTask").WithOpenApi();

            app.MapDelete("/tasks/{id}", (int id, AppDbContext db) =>
            {
                var item = db.Tasks.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found");
                }
                db.Entry(item).State = EntityState.Deleted;
                db.SaveChanges();
                return Results.Ok("Task deleted successfully");
            }).WithName("DeleteTask").WithOpenApi();

            return app;
        }
    }
}
