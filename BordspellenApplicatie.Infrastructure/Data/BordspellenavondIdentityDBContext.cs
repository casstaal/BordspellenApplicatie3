using BordspellenApplicatie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Data
{
    public class BordspellenavondIdentityDBContext : IdentityDbContext
    {
        public const string SqlSchema = "Identity";

        public BordspellenavondIdentityDBContext(DbContextOptions<BordspellenavondIdentityDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.HasDefaultSchema(SqlSchema);

            base.OnModelCreating(modelBuilder);
        }
    }
}
