using Data_acces;
using Data_acces.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Music_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MusicPlayer : Window
    {
        ViewModel viewModel;
        User user;
        public MusicPlayer(User User)
        {
            user = User;
            InitializeComponent();
            viewModel = ViewModel.Initialize(User);
            this.DataContext = viewModel;
        


           
           
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

    }
}
