using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_acces.Models;
using PropertyChanged;


namespace Music_Player
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        private ObservableCollection<Playlist> playlists;
        private ObservableCollection<Track> tracks;
        public string Login { get; set; }
        public string txtTrackName { get; set; }
        public string Password { get; set; }
        public bool IsRandomTracks { get; set; }
        public IEnumerable<Playlist> Playlists => playlists;
        public IEnumerable<Track> Tracks => tracks;
        public void AddTrack(Track tr)
        {
            tracks.Add(tr);
        }
        public void AddPlaylist(Playlist pl)
        {
            playlists.Add(pl);
        }

    }
}
