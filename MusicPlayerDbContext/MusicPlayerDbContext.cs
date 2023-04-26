using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_acces
{
    public class MusicPlayerDbContext : DbContext
    {
        public MusicPlayerDbContext()
        {
            this.Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NMUKAA8;
                                        Initial Catalog = MusicPlayerDb;
                                        Integrated Security=True;
                                        Connect Timeout=2;Encrypt=False;
                                        TrustServerCertificate=False;
                                        ApplicationIntent=ReadWrite;
                                        MultiSubnetFailover=False");
        }
    }
}
