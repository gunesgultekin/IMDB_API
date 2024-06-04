namespace IMDB_API.Context
{
    public class IMDB_MOVIECASTS
    {
        public int id {  get; set; }
        public int movie_id { get; set; }
        public int actor_id { get; set; }
        public virtual IMDB_MOVIES? movie { get; set; }
        public virtual IMDB_ACTORS? actor { get; set; }
    }
}
