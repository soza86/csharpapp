namespace CSharpApp.Application.Services
{
    public class ApiClientService<T>(HttpClient httpClient) where T : class
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ReadOnlyCollection<T>> GetAllAsync(string url)
        {
            var response = await _httpClient.GetFromJsonAsync<List<T>>(url);
            return response!.AsReadOnly();
        }

        public async Task<T?> GetByIdAsync(string url, int id)
        {
            var response = await _httpClient.GetFromJsonAsync<T>($"{url}/{id}");
            return response;
        }

        public async Task<T?> PostAsync<TRequest>(string url, TRequest payload) where TRequest : class
        {
            var response = await _httpClient.PostAsJsonAsync(url, payload);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();
            return result;
        }

        public async Task<T?> PutAsync<TRequest>(string url, TRequest payload) where TRequest : class
        {
            var response = await _httpClient.PutAsJsonAsync(url, payload);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();
            return result;
        }

        public async Task<T?> DeleteAsync(string url, int id)
        {
            return await _httpClient.DeleteFromJsonAsync<T>($"{url}/{id}");
        }
    }
}
