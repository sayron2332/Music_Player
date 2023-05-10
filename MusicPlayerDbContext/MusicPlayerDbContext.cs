using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Data_acces
{
    public class MusicPlayerDbContext : DbContext
    {
     

       
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=MusicPlayerDB.mssql.somee.com;
                                 Initial Catalog=MusicPlayerDB;
                                 Integrated Security=False;
                                 User ID=sayron_SQLLogin_1;
                                 Password=xhbdsdmj4b;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Track>()
          .HasMany(u => u.Playlists)
          .WithMany(a => a.Tracks);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Playlists)
                .WithOne(a => a.User);
          
        }
     

    }
}
