namespace CSharpApp.Core.Interfaces
{
    public interface IPostService
    {
        Task<ReadOnlyCollection<PostRecord>> GetAllPosts();
        Task<PostRecord?> GetPostById(int id);
        Task<PostRecord?> AddPost(PostRecord post);
        Task<PostRecord?> DeletePost(int id);
    }
}
