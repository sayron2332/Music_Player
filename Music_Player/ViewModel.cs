using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Data_acces;
using Data_acces.Migrations;
using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;


namespace Music_Player
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {

        private static ViewModel Model = null;
        MusicPlayerDbContext context;

        User user;
        public void setUser(User User)
        {
            user = User;
        }

        public static ViewModel Initialize(User user)
        {
            if (Model == null)
            {
                Model = new ViewModel(user);
                return Model;
            }
            else
            {
                return Model;
            }


        }



        protected ViewModel(User User)
        {
            user = User;
            context = new MusicPlayerDbContext();

            playlists = new ObservableCollection<Playlist>(context.Playlists
                .Where(p => p.UserId == user.Id)
                .ToArray());

            users = new ObservableCollection<User>(context.Users.ToArray());
            tracks = new ObservableCollection<Track>(context.Tracks.ToArray());
        }


        private ObservableCollection<Playlist> playlists;
      
        private ObservableCollection<Track> tracks;
        private ObservableCollection<User> users;
   
        public double slVolume { get; set; }
        public string PlaylistName { get; set; }
        public string TrackName { get; set; }
        public string AvtorName { get; set; }
        public string Source { get; set; }
        public double slLentghTrack { get; set; }
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
            users.Add(us);
        }

    }
}
