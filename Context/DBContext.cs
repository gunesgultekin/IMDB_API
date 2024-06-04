using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Context
{
    public class DBContext : DbContext
    {
        public DbSet<IMDB_USERS> IMDB_USERS { get; set; }
        public DbSet<IMDB_MOVIES> IMDB_MOVIES { get; set; }
        public DbSet<IMDB_CATEGORIES> IMDB_CATEGORIES { get; set; }
        public DbSet<IMDB_DIRECTORS> IMDB_DIRECTORS { get; set; }
        public DbSet<IMDB_RATINGS> IMDB_RATINGS { get; set; }
        public DbSet<IMDB_ACTORS> IMDB_ACTORS { get; set; }
        public DbSet<IMDB_WATCHLISTS> IMDB_WATCHLISTS { get; set; }
        public DbSet<IMDB_MOVIECASTS> IMDB_MOVIECASTS { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IMDB_USERS>()
                .HasKey(u => u.id);

            modelBuilder.Entity<IMDB_MOVIES>()
                .HasKey(m => m.id);

            modelBuilder.Entity<IMDB_MOVIES>()
                .HasOne(m => m.category)
                .WithMany(c => c.movies)
                .HasForeignKey(m => m.category_id);

            modelBuilder.Entity<IMDB_MOVIES>()
                .HasOne(m => m.director)
                .WithMany(d => d.movies)
                .HasForeignKey(m => m.director_id);

            modelBuilder.Entity<IMDB_CATEGORIES>()
                .HasKey(c => c.id);

            modelBuilder.Entity<IMDB_DIRECTORS>()
                .HasKey(d => d.id);


            modelBuilder.Entity<IMDB_RATINGS>()
                .HasKey(r => r.id);

            modelBuilder.Entity<IMDB_RATINGS>()
                .HasOne(r => r.user)
                .WithMany(u => u.ratings)
                .HasForeignKey(r => r.user_id);

            modelBuilder.Entity<IMDB_RATINGS>()
                .HasOne(r => r.movie)
                .WithMany(m => m.ratings)
                .HasForeignKey(r => r.movie_id);

            modelBuilder.Entity<IMDB_ACTORS>()
                .HasKey(a => a.id);

            modelBuilder.Entity<IMDB_MOVIECASTS>()
                .HasKey(m => m.id);

            modelBuilder.Entity<IMDB_MOVIECASTS>()
                .HasOne(mc => mc.movie)
                .WithMany(m => m.movieCasts)
                .HasForeignKey(mc => mc.movie_id);

            modelBuilder.Entity<IMDB_MOVIECASTS>()
                 .HasOne(mc => mc.actor)
                 .WithMany(a => a.movies)
                 .HasForeignKey(mc => mc.actor_id);

            modelBuilder.Entity<IMDB_WATCHLISTS>()
                .HasKey(w => w.id);

            modelBuilder.Entity<IMDB_WATCHLISTS>()
                .HasOne(w => w.movie)
                .WithMany(m => m.watchists)
                .HasForeignKey(w => w.movie_id);

            modelBuilder.Entity<IMDB_WATCHLISTS>()
                .HasOne(w => w.user)
                .WithMany(u => u.watchlist)
                .HasForeignKey(w => w.user_id);
        }
    }
}
