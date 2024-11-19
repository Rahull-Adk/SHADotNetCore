using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/birds", () =>
{
    string filePath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    return Results.Ok(result.Tbl_Bird);
}).WithName("GetBirds")
.WithOpenApi();


app.MapGet("/birds/{id}", (int id) =>
{
    string filePath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if(item is null)
    {
        return Results.NotFound("NO Data found");
    }
    return Results.Ok(item);

}).WithName("GetBird").WithOpenApi();

app.MapPost("/birds", (BirdModel requestModel) =>
{
    string filePath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    requestModel.Id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;
    result.Tbl_Bird.Add(requestModel);

    var jsonToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(filePath, jsonToWrite);

    return Results.Ok(requestModel);
}).WithName("CreateBird").WithOpenApi();

app.MapPut("/birds/{id}", (int id, BirdModel requestModel) =>
{
    string filePath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(filePath);

    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if(item is null)
    {
        return Results.NotFound("No Data found");
    }
    item.Description = requestModel.Description;
    item.BirdEnglishName = requestModel.BirdEnglishName;
    item.BirdMyanmarName = requestModel.BirdMyanmarName;
    item.ImagePath = requestModel.ImagePath;

    var jsonToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(filePath, jsonToWrite);
    return Results.Ok(item);

}).WithName("UpdateBird").WithOpenApi();

app.MapDelete("/birds/{id}", (int id) =>
{
    var filePath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(filePath)!;
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (item is null)
    {
        return Results.NotFound("No Data Found");
    }

    result.Tbl_Bird.Remove(item);

    var strToJson = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(filePath, strToJson);
    return Results.Ok("Item deleted successfully");

}).WithName("DeleteBird").WithOpenApi();

app.Run();



public class BirdResponseModel
{
    public List<BirdModel> Tbl_Bird { get; set; }
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}

