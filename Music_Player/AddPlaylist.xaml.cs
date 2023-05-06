using Data_acces;
using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Music_Player
{
    /// <summary>
    /// Interaction logic for AddPlaylist.xaml
    /// </summary>
    public partial class AddPlaylist : Window
    {
        User User;
        ViewModel model;
        MusicPlayerDbContext Context;
        bool IsPlaylistToBeDatabase = false;
        public AddPlaylist(User user)
        {
            InitializeComponent();
            User = user;
            Context = MusicPlayerDbContext.Initialize();
            model = ViewModel.Initialize();
            this.DataContext = model;

        }

        private void Click_btnAddPlaylist(object sender, RoutedEventArgs e)
        {

            if (model.PlaylistName != " " && !String.IsNullOrWhiteSpace(model.PlaylistName))
            {
                Playlist playlist = new Playlist();
                playlist.Name = model.PlaylistName;
                playlist.UserId = User.Id;
                Playlist playlist2 = new Playlist();
                playlist2 = model.Playlists.FirstOrDefault(u => u.UserId == User.Id);
                foreach (var item in model.Playlists)
                {
                  
                    if (playlist.Name == item.Name && playlist2.UserId == playlist.UserId)
                    {
                        IsPlaylistToBeDatabase = true;
                    }

                }
                if (IsPlaylistToBeDatabase)
                {
                    MessageBox.Show("this playlist to be in database");
                }
                else
                {
                    Context.Playlists.Add(playlist);
                    Context.SaveChanges();
                    MessageBox.Show("Playlist add");
                }
              
            }
            else
            {
                MessageBox.Show("Enter corect login or password");
               
            }
        }
       
    }
}
