namespace CSharpApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TodoService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TodoRecord?> GetTodoById(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("client");
        var response = await httpClient.GetFromJsonAsync<TodoRecord>($"todos/{id}");

        return response;
    }

    public async Task<ReadOnlyCollection<TodoRecord>> GetAllTodos()
    {
        var httpClient = _httpClientFactory.CreateClient("client");
        var response = await httpClient.GetFromJsonAsync<List<TodoRecord>>($"todos");

        return response!.AsReadOnly();
    }
}