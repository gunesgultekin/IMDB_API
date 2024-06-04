namespace IMDB_API.Context
{
    public class MovieModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public int category_id { get; set; }
        public int director_id { get; set; }
        public string? cover {  get; set; }

        public float? imdb_rating { get; set; }
        public long? review_count { get; set; }
        public int? popularity { get; set; }
        public int length { get; set; }
        public int year { get; set; }
        public string? age_restriction { get; set; }
        public string clip {  get; set; }
        public string popularity_status { get; set; }
        public bool isDeleted { get; set; }


    }
}
