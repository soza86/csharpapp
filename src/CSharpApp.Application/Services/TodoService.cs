namespace CSharpApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly ILogger<TodoService> _logger;
    private readonly HttpClient _client;
    private readonly string? _baseUrl;

    private readonly IHttpClientFactory _httpClientFactory;

    public TodoService(ILogger<TodoService> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _client = new HttpClient();
        _baseUrl = configuration["BaseUrl"];
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