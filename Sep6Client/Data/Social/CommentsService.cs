using Microsoft.Extensions.Options;
using Sep6Client.Data.DataHelper;
using System.Text.Json;
using Sep6Client.Model;
using Firebase.Database;
using Firebase.Database.Query;

namespace Sep6Client.Data.Social{
    public class CommentsService : ICommentsService
    {
        private readonly FirebaseClient firebaseClient;
        private string baseUri;
        private string dbResource = "Comments";
        private readonly JsonSerializerOptions options;

        public CommentsService(IOptions<RtdbSettings> rtdbSettings)
        {
            baseUri = rtdbSettings.Value.BaseUri;
            firebaseClient = new FirebaseClient(baseUri);
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<Comment>> GetCommentsByMovieIdAsync(int movieId)
        {
            var comments = await firebaseClient
                .Child(dbResource)
                .OrderByKey()
                .StartAt($"{movieId}:")
                .EndAt($"{movieId}:\uf8ff")
                .OnceAsync<Comment>();

            return comments
                .Select(x => new Comment
                {
                    Username = x.Object.Username,
                    Timestamp = x.Object.Timestamp,
                    Body = x.Object.Body,
                    MovieId = x.Object.MovieId
                })
                .ToList();
        }


        public async Task PostCommentAsync(Comment comment)
        {
            var commentKey = $"{comment.MovieId}:{comment.Timestamp.Ticks}";
            await firebaseClient
                .Child(dbResource)
                .Child(commentKey)
                .PutAsync(new Comment
                {
                    MovieId = comment.MovieId,
                    Body = comment.Body,
                    Username = comment.Username,
                    Timestamp = comment.Timestamp
                });
        }
    }
}