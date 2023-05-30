using Sep6Client.Model;

namespace Sep6Client.Data.Social{
    public interface ICommentsService
    {
        Task<List<Comment>> GetCommentsByMovieIdAsync(int movieId);
        Task PostCommentAsync(Comment comment);
    }
}