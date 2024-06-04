﻿namespace IMDB_API.Context
{
    public class IMDB_MOVIES
    {
        public int id {  get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public int category_id { get; set; }
        public int director_id { get; set; }
        public string? cover {  get; set; }
        public double? imdb_rating { get; set; }
        public long? review_count { get; set; }
        public int? popularity { get; set; }
        public int length { get; set; }
        public int year { get; set; }
        public string? age_restriction { get; set; }
        public string? clip {  get; set; }
        public string? popularity_status { get; set; }
        public bool isDeleted { get; set; }

        public virtual IMDB_CATEGORIES? category { get; set; }
        public virtual IMDB_DIRECTORS? director { get; set; }
        public virtual List<IMDB_MOVIECASTS>? movieCasts { get; set; }

        public virtual List<IMDB_RATINGS?> ratings { get; set; }
        public virtual List<IMDB_WATCHLISTS> watchists { get; set; }

    }
}
