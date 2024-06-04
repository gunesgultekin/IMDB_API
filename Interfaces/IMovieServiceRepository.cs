using IMDB_API.Context;

namespace IMDB_API.Interfaces
{
    public interface IMovieServiceRepository
    {
        public Task<List<IMDB_MOVIES>> getTen();
        public Task<IMDB_MOVIES> getMovie(string title);
        public Task handlepopularities();
        public Task<int> rateMovie(RatingModel model);

    }
}
