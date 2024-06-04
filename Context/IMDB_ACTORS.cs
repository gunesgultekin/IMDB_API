namespace IMDB_API.Context
{
    public class IMDB_ACTORS
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string? photo { get; set; }
        public string? knownAs { get; set; }
        public virtual List<IMDB_MOVIECASTS>? movies{ get; set; }
    }
}
