using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_acces.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public int? PlaylistsId { get; set; }
        public Playlist? Playlists { get; set; }  


    }
}
