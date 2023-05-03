using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_acces;
using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;


namespace Music_Player
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        
        public ViewModel(MusicPlayerDbContext context)
        {
            context = new MusicPlayerDbContext();
            playlists = new ObservableCollection<Playlist>(context.Playlists.ToArray());
            tracks = new ObservableCollection<Track>(context.Tracks.ToArray());
            user = new ObservableCollection<User>(context.Users.ToArray());
        }
        private ObservableCollection<Playlist> playlists;
        private ObservableCollection<Track> tracks;
        private ObservableCollection<User> user;
        public string Login { get; set; }
        public double slVolume { get; set; }
        public double slLentghTrack { get; set; }
        public string txtTrackName { get; set; }
        public string Password { get; set; }
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
        public void AddUser(User us)
        {
            user.Add(us);
        }

    }
}
