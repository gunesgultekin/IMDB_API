namespace IMDB_API.Context
{
    public class IMDB_RATINGS
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int movie_id { get; set; }
        public int rating { get; set; } 
        public bool isDeleted { get; set; }
        public IMDB_MOVIES movie { get; set; }
        public IMDB_USERS user { get; set; }
    }
}
