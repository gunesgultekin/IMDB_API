using IMDB_API.Context;

namespace IMDB_API.Interfaces
{
    public interface ISearchService
    {
        public Task<(List<IMDB_MOVIES>, List<IMDB_DIRECTORS>, List<IMDB_ACTORS>)> genericSearch(string keyword);
        public Task<List<IMDB_MOVIES>> titleSearch(string keyword);
        public Task<List<IMDB_ACTORS>> actorSearch (string keyword);
        public Task<List<IMDB_DIRECTORS>> directorSearch(string keyword);

    }
}
