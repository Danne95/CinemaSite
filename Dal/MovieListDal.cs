using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Project.Models;

namespace Project.Dal
{
    public class MovieListDal : DbContext
    {
        public DbSet<MovieList> movies { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieList>().ToTable("movieList");
        }

    }
}