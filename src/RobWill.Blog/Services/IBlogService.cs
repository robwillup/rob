namespace RobWill.Blog.Services;

public interface IBlogService
{
    public Task<List<GitHubItem>> GetBlogPostsAsync();
    public Task<string> GetBlogPostAsync(string id);
}