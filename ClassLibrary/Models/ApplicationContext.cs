using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<News> Feed { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public bool IsExists(News news)
        {
            return Feed.Where(x =>
            x.Link == news.Link)
                .FirstOrDefault() != null;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RssFeedDataBase1;Trusted_Connection=True;");
        }
    }
}
