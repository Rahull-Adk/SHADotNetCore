using Microsoft.EntityFrameworkCore;
using SHADotNetCore.ToDoList.Models;
using Task = SHADotNetCore.ToDoList.Models.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
AppDbContext db = new AppDbContext();
app.MapGet("/tasks", () =>
{
    AppDbContext db = new AppDbContext();
    var items = db.Tasks.AsNoTracking().ToList();
    return Results.Ok(items);

}).WithName("GetTasks").WithOpenApi();

app.MapGet("/tasks/{id}", (int id) =>
{
    var item = db.Tasks.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
    if (item is null)
    {
        return Results.BadRequest("No Data Found");
    }
    return Results.Ok(item);
}).WithName("GetTask").WithOpenApi();

app.MapPost("/tasks", (Task task) =>
{
    db.Tasks.Add(task);
    db.SaveChanges();
    return Results.Ok(task);
}).WithName("CreateTasks").WithOpenApi();

app.MapPut("/tasks/{id}", (int id, Task task) =>
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

app.MapDelete("/tasks/{id}", (int id) =>
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


app.Run();