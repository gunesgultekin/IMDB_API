namespace IMDB_API.Context
{
    public class IMDB_WATCHLISTS
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int movie_id {  get; set; }
        public bool isDeleted { get; set; }
        public virtual IMDB_MOVIES movie { get; set; }
        public virtual IMDB_USERS user { get; set; }
    }
}
