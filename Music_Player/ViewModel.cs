using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Data_acces;

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

        

            users = new ObservableCollection<User>(context.Users.ToArray());
            tracks = new ObservableCollection<Track>(context.Tracks
                .Where(p => p.UserId == user.Id)
                .ToArray());
            tracksToFind = new ObservableCollection<Track>(context.Tracks
                .Where(p => p.UserId == user.Id)
                .ToArray());
        }


       
      
        private ObservableCollection<Track> tracks;
        private ObservableCollection<User> users;
        private ObservableCollection<Track> tracksToFind;

        public string FindNameTrack { get; set; }
        public double slVolume { get; set; }
        public string PlaylistName { get; set; }
        public string TrackName { get; set; }
        public string txtTrackName { get; set; }
        public string sourceImg { get; set; }
        public string TrackSourcePlayNow { get; set; }
        public string txtAvtorName { get; set; }
        public string AvtorName { get; set; }
        public string Source { get; set; }
        public double slLentghTrack {get; set; }
        public IEnumerable<Track> Tracks => tracks;
        public IEnumerable<Track> TracksToFind => tracksToFind;
        public void AddTrack(Track tr)
        {
            tracks.Add(tr);
        }
        public void RemoveTrack(Track tr)
        {
            tracks.Remove(tr);
        }
        public void TrackToFindAdd(Track tr)
        {
            tracksToFind.Add(tr);
        }

        public void AddUser(User us)
        {
            users.Add(us);
        }
        public void TrackToFindClear()
        {
            tracksToFind.Clear();
        }

    }
}
