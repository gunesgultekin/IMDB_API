namespace IMDB_API.Context
{
    public class IMDB_DIRECTORS
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string? photo {  get; set; }
        public string? knownAs { get; set; }
        public List<IMDB_MOVIES>? movies { get; set; }
    }
}
