using IMDB_API.Context;
using IMDB_API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private DBContext _context;
        public UserServiceRepository(DBContext context) {  _context = context; }
        public async Task<long> register(IMDB_USERS user)
        {
            string plainTextPassword = user.password;
            user.password = SecurityService.ComputeSha512Hash(plainTextPassword);
            
            await _context.IMDB_USERS.AddAsync(user);
            _context.SaveChanges();
            return user.id;
        }
        
        public async Task<string> login(LoginModel user)
        {
            string plainTextPassword = user.password;
            user.password = SecurityService.ComputeSha512Hash(plainTextPassword);

            var queryResult = await _context.IMDB_USERS.Where(
                u =>
                    (u.username == user.credential ||
                     u.email == user.credential) &&
                     u.password == user.password
            ).FirstOrDefaultAsync();

            if (queryResult != null)
            {
                return SecurityService.GenerateToken(user.credential);
            }

            else
            {
                return "";
            }
        }

        public async Task<int> addToWatchlist(string username, string title)
        {
            var watchlistItem = new IMDB_WATCHLISTS();
            var movie = await _context.IMDB_MOVIES.Where(m => m.title == title).FirstAsync();
            var user = await _context.IMDB_USERS.Where(u => u.username == username).FirstOrDefaultAsync();
            watchlistItem.user_id = user.id;
            watchlistItem.movie_id = movie.id;
            _context.IMDB_WATCHLISTS.Add(watchlistItem);
            _context.SaveChanges();
            return watchlistItem.id;
        }
        public async Task<int> removeFromWatchlist(string username, string title)
        {
            var user = await _context.IMDB_USERS.Where(u => u.username == username).FirstOrDefaultAsync();
            var wathListItem = await _context.IMDB_WATCHLISTS
                .Where(w => w.user_id == user.id)
                .FirstOrDefaultAsync();
            _context.IMDB_WATCHLISTS .Remove(wathListItem);
            _context.SaveChanges ();
            return wathListItem.id;
        }


        public async Task<List<IMDB_MOVIES>> myWatchlist(string username)
        {
            var watchlist =  await _context.IMDB_WATCHLISTS
                .Include(w => w.user)
                .Include(w => w.movie)
                  .ThenInclude(m => m.category)
                .Include(w => w.movie)
             .ThenInclude(m => m.director)
             .Include(w => w.movie)
            .ThenInclude(m => m.movieCasts)

                .Where(w => w.user.username == username)
                .ToListAsync();
                
            List<IMDB_MOVIES> watchListMovies = new List<IMDB_MOVIES>();
            foreach(var watchlistItem in watchlist)
            {
                watchListMovies.Add(watchlistItem.movie);
            }
            return watchListMovies;
        }
    }
}
