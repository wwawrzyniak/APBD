using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(entity =>
            {
                //Title - according to the stackoverflow setting minimum length with Fluent Api is not yet possible
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Title).HasMaxLength(60);

                //Release Date 
                entity.Property(e => e.ReleaseDate).HasColumnType("Date");
                entity.Property(e => e.ReleaseDate).HasColumnName("Release Date");


                //Price
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Price).HasColumnType("Money");

                //Genre
                entity.Property(e => e.Genre).HasMaxLength(30);
                entity.Property(e => e.Genre).IsRequired();

                //Rating
                entity.Property(e => e.Rating).IsRequired();

                entity.ToTable("Movie");
            });


        }
    }
}
