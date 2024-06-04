using IMDB_API.Context;
using IMDB_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Repositories
{
    public class MovieServiceRepository : IMovieServiceRepository
    {
        private DBContext _context;
        public MovieServiceRepository(DBContext context) 
        {
            _context = context;
        }
        public async Task<List<IMDB_MOVIES>> getTen()
        {
            List<IMDB_MOVIES> topTen = await _context.IMDB_MOVIES
                .Include(m => m.category)
                .Include(m => m.director)
                .Where(m => !m.isDeleted)
                .Take(10)           
                .ToListAsync();
            return topTen;
        }

        public async Task<IMDB_MOVIES> getMovie(string title)
        {
            return await _context.IMDB_MOVIES
                .Include(m => m.category)
                .Include(m => m.director)
                .Where(m => m.title == title)
                .Where(m => !m.isDeleted)
                .FirstAsync();
        }

        //[Authorize]
        public async Task<int> rateMovie(RatingModel model)
        {
            var user = await _context.IMDB_USERS.Where(u => u.username == model.username).FirstOrDefaultAsync();
            var movie = await _context.IMDB_MOVIES.Where(m => m.title == model.title).FirstOrDefaultAsync();

            IMDB_RATINGS userRating = new IMDB_RATINGS();
            
            userRating.user_id = user.id;
            userRating.movie_id = movie.id;
            userRating.rating = model.rating;
            userRating.isDeleted = false;

            _context.IMDB_RATINGS.Add(userRating);

            await _context.SaveChangesAsync();

            return userRating.rating;
        }

        public async Task handlepopularities()
        {
            
            await Task.Run(async () =>
            {
                var movies = await _context.IMDB_MOVIES.ToListAsync();
                foreach (var movie in movies )
                {
                    int? currentPopularity = movie.popularity;
                    int? updatedPopularity = int.Parse(movie.review_count.ToString()) / int.Parse(((int)movie.imdb_rating * 100).ToString());

                    movie.popularity = updatedPopularity;
                    await _context.SaveChangesAsync();

                    if (currentPopularity < updatedPopularity)
                    {
                        movie.popularity_status = "Increased";
                        await _context.SaveChangesAsync();
                    }

                    else
                    {
                        movie.popularity_status = "Decreased";
                        await _context.SaveChangesAsync();
                    }
                }
            });    
        }
    }
}
