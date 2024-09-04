using CSharpApp.Application.Services;
using CSharpApp.Core.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDefaultConfiguration();

builder.Services.AddHttpClient<ApiClientService<TodoRecord>>(client =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<ApiClientService<PostRecord>>(client =>
{
    var baseUrl = builder.Configuration["BaseUrl"];
    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddTransient(typeof(ApiClientService<>), typeof(ApiClientService<>));

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

//app.UseHttpsRedirection();

app.MapGet("/todos", async (ITodoService todoService) =>
    {
        var todos = await todoService.GetAllTodos();
        return todos;
    })
    .WithName("GetTodos")
    .WithOpenApi();

app.MapGet("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
    {
        var todos = await todoService.GetTodoById(id);
        return todos;
    })
    .WithName("GetTodosById")
    .WithOpenApi();

app.Run();