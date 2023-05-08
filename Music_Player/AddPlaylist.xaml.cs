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
        
        ViewModel model;
        MusicPlayerDbContext Context;
        User User;
    
        public AddPlaylist(User user)
        {
            InitializeComponent();
           
            Context = MusicPlayerDbContext.Initialize();
            model = ViewModel.Initialize();
            User = user;
            this.DataContext = model;

        }

        private void Click_btnAddPlaylist(object sender, RoutedEventArgs e)
        {

            if (model.PlaylistName != " " && !String.IsNullOrWhiteSpace(model.PlaylistName))
            {
                Playlist playlist = new Playlist();
                playlist.Name = model.PlaylistName;
                playlist.UserId = User.Id;
                Context.Playlists.Add(playlist);
                Context.SaveChanges();
                MessageBox.Show("Playlist add");
                MusicPlayer ms = new MusicPlayer(User);
                this.Close();
                ms.Show();
             
                


            }
            else
            {
                MessageBox.Show("Enter corect name");
               
            }
        }
       
    }
}
