using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Evaluation_Workshop_Final_CDA_C_.Data;
using System.Web.WebPages.Html;
using Evaluation_Workshop_Final_CDA_C_.Models;

namespace Evaluation_Workshop_Final_CDA_C_.Data
{
    public class Evaluation_Workshop_Final_CDA_C_Context : DbContext
    {
        public Evaluation_Workshop_Final_CDA_C_Context(DbContextOptions<Evaluation_Workshop_Final_CDA_C_Context> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animal { get; set; } = default!;
        public DbSet<Race> Race { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
