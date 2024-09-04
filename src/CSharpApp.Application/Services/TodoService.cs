namespace CSharpApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly ApiClientService<TodoRecord> _toDoRecordClient;

    public TodoService(ApiClientService<TodoRecord> toDoRecordClient)
    {
        _toDoRecordClient = toDoRecordClient;
    }

    public async Task<TodoRecord?> GetTodoById(int id)
    {
        return await _toDoRecordClient.GetByIdAsync($"todos", id);
    }

    public async Task<ReadOnlyCollection<TodoRecord>> GetAllTodos()
    {
        return await _toDoRecordClient.GetAllAsync($"todos");
    }
}