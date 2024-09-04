namespace CSharpApp.Application.Services
{
    public class PostService : IPostService
    {
        private readonly ApiClientService<PostRecord> _postRecordClient;

        public PostService(ApiClientService<PostRecord> postRecordClient)
        {
            _postRecordClient = postRecordClient;
        }

        public async Task<ReadOnlyCollection<PostRecord>> GetAllPosts()
        {
            return await _postRecordClient.GetAllAsync($"posts");
        }

        public async Task<PostRecord?> GetPostById(int id)
        {
            return await _postRecordClient.GetByIdAsync($"posts", id);
        }

        public async Task<PostRecord?> AddPost(PostRecord post)
        {
            return await _postRecordClient.PostAsync($"posts", post);
        }

        public async Task<PostRecord?> DeletePost(int id)
        {
            return await _postRecordClient.DeleteAsync($"posts", id);
        }
    }
}
