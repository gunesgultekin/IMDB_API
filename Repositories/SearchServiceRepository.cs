using IMDB_API.Context;
using IMDB_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace IMDB_API.Repositories
{
    public class SearchServiceRepository : ISearchService
    {
        private DBContext _context;

        public SearchServiceRepository(DBContext context) {  _context = context; }
        public async Task<(List<IMDB_MOVIES>, List<IMDB_DIRECTORS>, List<IMDB_ACTORS>)> genericSearch(string keyword)
        { 
            List<IMDB_MOVIES> movieSearch = new List<IMDB_MOVIES>();
            List<IMDB_DIRECTORS> directorSearch = new List<IMDB_DIRECTORS>();
            List<IMDB_ACTORS> actorSearch = new List<IMDB_ACTORS>();

            foreach (var movie in _context.IMDB_MOVIES)
            {
                if ((movie.title).ToLower().Contains(keyword.ToLower()) || movie.description.Contains(keyword))
                {
                    movieSearch.Add(movie);
                }
            }

            foreach(var actor in _context.IMDB_ACTORS)
            {
                if (actor.name.ToLower().Contains(keyword))
                {
                    actorSearch.Add(actor);
                }

                if (actor.surname.ToLower().Contains(keyword))
                {
                    actorSearch.Add(actor);
                }
            }

            foreach (var cast in _context.IMDB_MOVIECASTS)
            {
                if (cast.movie.title.ToLower().Contains(keyword))
                {
                    actorSearch.Add(cast.actor);
                }
            }

            foreach (var director in _context.IMDB_DIRECTORS)
            {
                if (director.name.ToLower().Contains(keyword))
                {
                    directorSearch.Add(director);
                }

                if (director.surname.ToLower().Contains(keyword))
                {
                    directorSearch.Add(director);
                }

                foreach (var movie in director.movies)
                {
                    if (movie.title.ToLower().Contains(keyword.ToLower()))
                    {
                        directorSearch.Add(director);

                    }
                }
            }
            return (movieSearch,directorSearch,actorSearch); 
        }
        public async Task<List<IMDB_MOVIES>> titleSearch(string keyword)
        {
            List<IMDB_MOVIES> moviesFound = new List<IMDB_MOVIES> ();

            moviesFound = await _context.IMDB_MOVIES
                .Include(m => m.director)
                .Include(m => m.category)
               .Where(a => a.title.ToLower().Contains(keyword.ToLower()) 
               
               
               ).ToListAsync();

            return moviesFound;
        }

        public async Task<List<IMDB_ACTORS>> actorSearch(string keyword)
        {
            List<IMDB_ACTORS> actorsFound = new List<IMDB_ACTORS>();

            actorsFound = await _context.IMDB_ACTORS.
                Where(a => a.name.ToLower().Contains(keyword.ToLower()) ||
                a.surname.ToLower().Contains(keyword.ToLower())  
                ).ToListAsync();

            return actorsFound;
        }

        public async Task<List<IMDB_DIRECTORS>> directorSearch(string keyword)
        {
            List<IMDB_DIRECTORS> directorsFound = new List<IMDB_DIRECTORS>();

            directorsFound = await _context.IMDB_DIRECTORS.
                Where(a => a.name.ToLower().Contains(keyword.ToLower()) ||
                a.surname.ToLower().Contains(keyword.ToLower())
                ).ToListAsync();

            return directorsFound;
        }
    }
}
