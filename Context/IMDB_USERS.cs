namespace IMDB_API.Context
{
    public class IMDB_USERS
    {
        public int id {  get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string country { get; set; }
        public string? city { get; set; }
        public bool isDeleted { get; set; }
        public virtual List<IMDB_RATINGS> ratings { get; set; }
        public virtual List<IMDB_WATCHLISTS> watchlist { get; set; }
    }
}
