namespace IMDB_API.Context
{
    public class IMDB_CATEGORIES
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<IMDB_MOVIES> movies { get; set; }
    }
}
