﻿using Data_acces;
using Data_acces.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;



namespace Music_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MusicPlayer : Window
    {
        ViewModel viewModel;
        User user;
        SoundPlayer soundPlayer;
        
        bool Play = false;
        public MusicPlayer(User User)
        {
            user = User;
            InitializeComponent();
            viewModel = ViewModel.Initialize(User);
            this.DataContext = viewModel;
            soundPlayer = new SoundPlayer();
            viewModel.sourceImg = "ui-img/play.png";

        }

        private void Click_btnAddPlaylist(object sender, RoutedEventArgs e)
        {
            
            AddPlaylist playlist = new AddPlaylist(user);
            playlist.ShowDialog();
           
         
           
        }

        private void Click_btnAddTrack(object sender, RoutedEventArgs e)
        {

            AddTrack Track = new AddTrack(user);
            Track.ShowDialog();




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateTracksList();
        }
        private void UpdateTracksList()
        {
            viewModel.TrackToFindClear();
            using (MusicPlayerDbContext Context = new MusicPlayerDbContext())
            {
                var query = Context.Tracks.AsQueryable();

                if (!string.IsNullOrEmpty(viewModel.FindNameTrack))
                    query = query.Where(x => x.Name.Contains(viewModel.FindNameTrack));



                query = query.OrderBy(x => x.Id);
                   

                var tracks = query.ToArray();
                foreach (var track in tracks)
                {
                   viewModel.TrackToFindAdd((Track)track);
                }
            }
         
        }

        private void Click_btnAddTrackToPlaylist(object sender, RoutedEventArgs e)
        {
            if (DG1.SelectedItem != null && lsPlaylist.SelectedItem != null)
            {
                Track trackWithGrid = DG1.SelectedItem as Track;
                Playlist playlist = lsPlaylist.SelectedItem as Playlist;
                if (trackWithGrid.PlaylistsId == playlist.Id)
                {
                    MessageBox.Show("this track such alredy this playlist");
                }
                else
                {
                    Track NewTrack = new Track()
                    {
                        Name = trackWithGrid.Name,
                        Author = trackWithGrid.Author,
                        Source = trackWithGrid.Source,
                        PlaylistsId = playlist.Id,

                    };



                    using (MusicPlayerDbContext Context = new MusicPlayerDbContext())
                    {

                        Context.Tracks.Remove(trackWithGrid);
                        Context.Tracks.Add(NewTrack);
                        Context.SaveChanges();

                    }
                }
               
                    

            }
            else
            {
                MessageBox.Show("Please Enter Track");
            }
          
        }

        private void PlayMuisc(object sender, RoutedEventArgs e)
        {

            if (DG1.SelectedItem == null)
            {
                MessageBox.Show("CHOOSE TRACK!!");
            }



            else if(Play == false)
            {
                Play = true;
                Track TrackFromGrid = DG1.SelectedItem as Track;
                soundPlayer = new System.Media.SoundPlayer(TrackFromGrid.Source);
                viewModel.txtTrackName = TrackFromGrid.Name;
                viewModel.txtAvtorName = TrackFromGrid.Author;
                
                viewModel.sourceImg = "ui-img/pause.png";
                soundPlayer.Play();
                
            }

            else if (Play == true)
            {
                viewModel.sourceImg = "ui-img/play.png";
                Play = false;
                soundPlayer.Stop();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTracksList();
        }

        
    }
}
