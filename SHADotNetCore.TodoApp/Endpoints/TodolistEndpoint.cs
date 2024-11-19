using Microsoft.EntityFrameworkCore;
using SHADotNetCore.TodoApp.Model;

namespace SHADotNetCore.TodoApp.Endpoints
{
    public static class TodolistEndpoint
    {
        public static IEndpointRouteBuilder UseTaskListEndpoint(this IEndpointRouteBuilder app)
        {
           
            app.MapGet("/tasks", async (DataContext db) =>
            {
                var items = await db.Tasks.AsNoTracking().ToListAsync();
                return Results.Ok(items);
            })
.WithName("GetTasks")
.WithOpenApi();
            app.MapGet("/tasks/{id}", async (int id, DataContext db) =>
            {
                var item = await db.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.TaskId == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found");
                }
                return Results.Ok(item);
            }).WithName("GetTask").WithOpenApi();

            app.MapPost("/tasks", async (TaskList task, DataContext db) =>
            {
                await db.Tasks.AddAsync(task);
                await db.SaveChangesAsync();
                return Results.Ok(task);
            }).WithName("CreateTasks").WithOpenApi();

            app.MapPut("/tasks/{id}", async (int id, TaskList task, DataContext db) =>
            {
                var item = await db.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);
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
                await db.SaveChangesAsync();
                return Results.Ok(item);
            }).WithName("UpdateTask").WithOpenApi();

            app.MapDelete("/tasks/{id}", async(int id, DataContext db) =>
            {
                var item = await db.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.TaskId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found");
                }
                db.Entry(item).State = EntityState.Deleted;
               await db.SaveChangesAsync();
                return Results.Ok("Task deleted successfully");
            }).WithName("DeleteTask").WithOpenApi();

            return app;

        }
    }
}
