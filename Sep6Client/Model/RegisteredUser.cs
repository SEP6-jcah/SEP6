using System.Collections.Generic;

namespace Sep6Client.Model{
    public class RegisteredUser
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public List<string> FavouriteMovies { get; set; } = new List<string>();
        public List<string> Followers { get; set; } = new List<string>();
        public List<string> Following { get; set; } = new List<string>();
    }
}