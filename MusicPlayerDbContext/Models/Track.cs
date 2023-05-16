using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_acces.Models
{
    public class Track
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
       


    }
}
