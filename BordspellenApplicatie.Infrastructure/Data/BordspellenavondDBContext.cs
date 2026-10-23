using BordspellenApplicatie.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Data
{
    public class BordspellenavondDBContext : DbContext
    {
        public const string SqlSchema = "Bordspellenavond";

        public BordspellenavondDBContext(DbContextOptions<BordspellenavondDBContext> options) : base(options)
        {
        }

        public DbSet<BoardGameNight> BoardGameNights => Set<BoardGameNight>();
        public DbSet<Boardgame> Games => Set<Boardgame>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserBoardGameNight> UserBoardGameNights => Set<UserBoardGameNight>();
        public DbSet<Review> Reviews => Set<Review>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SqlSchema);

            modelBuilder.Entity<UserBoardGameNight>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserBoardGameNights)
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserBoardGameNight>()
                .HasOne(ug => ug.GameNight)
                .WithMany(g => g.UserBoardGameNights)
                .HasForeignKey(ug => ug.BoardGameNightId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Review>()
                .HasOne(r => r.BoardGameNight)
                .WithMany(b => b.Reviews) 
                .HasForeignKey(r => r.BoardGameNightId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Boardgame>()
                .HasOne(g => g.BoardGameNight)
                .WithMany(b => b.Games)
                .HasForeignKey(g => g.BoardGameNightId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
