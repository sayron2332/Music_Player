using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Data_acces
{
    public class MusicPlayerDbContext : DbContext
    {
      

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
        DbSet<Track> Tracks { get; set; }
        DbSet<Playlist> Playlists { get; set; }
        DbSet<User> Users { get; set; }

    }
}
