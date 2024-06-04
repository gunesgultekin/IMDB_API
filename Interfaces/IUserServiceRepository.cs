using IMDB_API.Context;

namespace IMDB_API.Interfaces
{
    public interface IUserServiceRepository
    {
        public Task<long> register(IMDB_USERS user);
        public Task<string> login(LoginModel user);
        public Task<int> addToWatchlist(string username,string title);
        public Task<int> removeFromWatchlist(string username,string title);
        public Task<List<IMDB_MOVIES>> myWatchlist(string username);
    }
}
