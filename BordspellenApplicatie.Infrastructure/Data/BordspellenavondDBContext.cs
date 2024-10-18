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

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.HasDefaultSchema(SqlSchema);

    }
}
