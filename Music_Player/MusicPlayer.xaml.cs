using Data_acces;
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
using System.Threading;
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
using System.Windows.Threading;
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
        DispatcherTimer Timer;

        bool Play = false;
        public MusicPlayer(User User)
        {
            user = User;
            InitializeComponent();
            viewModel = ViewModel.Initialize(User);
            this.DataContext = viewModel;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            viewModel.sourceImg = "ui-img/play.png";
            viewModel.slVolume = 1;

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            viewModel.slLentghTrack = myMediaElement.Position.TotalSeconds;
           
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
        public void UpdateTracksList()
        {
            viewModel.TrackToFindClear();
            using (MusicPlayerDbContext Context = new MusicPlayerDbContext())
            {
                var query = viewModel.Tracks.AsQueryable();

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

          private void PlayNextTrack()
        {

            Play = true;
            Track TrackFromGrid = DG1.SelectedItem as Track;
            viewModel.TrackSourcePlayNow = TrackFromGrid.Source;
            viewModel.txtTrackName = TrackFromGrid.Name;
            viewModel.txtAvtorName = TrackFromGrid.Author;
            myMediaElement.Play();
            viewModel.sourceImg = "ui-img/pause.png";
        }
       
        private void PlayMuisc(object sender, RoutedEventArgs e)
        {

            if (DG1.SelectedItem == null)
            {
                DG1.SelectedIndex = 0; 
                PlayNextTrack();
            }



            else if (Play == false)
            {
                PlayNextTrack();
            }

            else if (Play == true)
            {
                viewModel.sourceImg = "ui-img/play.png";
                Play = false;
                myMediaElement.Pause();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTracksList();
        }
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            Timer.Start();
           
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            Play = true;
            DG1.SelectedIndex++;
            PlayNextTrack();


        }
      
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> e)
        {


            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
         
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)viewModel.slLentghTrack);
            myMediaElement.Position = TimeSpan.FromSeconds(viewModel.slLentghTrack);
         



        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMediaElement.Volume = viewModel.slVolume;
        }

        private void Click_btnSwitchRight(object sender, RoutedEventArgs e)
        {
            if (DG1.Items.Count - 1 == DG1.SelectedIndex)
            {
                DG1.SelectedIndex = 0;
                PlayNextTrack();
            }

            else
            {
                DG1.SelectedIndex++;
                PlayNextTrack();
            }

        }

        private void Click_btnSwitchLeft(object sender, RoutedEventArgs e)
        {
            if (DG1.SelectedIndex == 0)
            {

                DG1.SelectedIndex = DG1.Items.Count;
                PlayNextTrack();
            }

            else
            {
                DG1.SelectedIndex--;
                PlayNextTrack();
            }
        }

        private void Click_btnDeleteTrack(object sender, RoutedEventArgs e)
        {
            if (DG1.SelectedItem != null)
            {
                Track tr = DG1.SelectedItem as Track;
                viewModel.RemoveTrack(tr);
                using (MusicPlayerDbContext Context = new MusicPlayerDbContext())
                {
                    Context.Tracks.Remove(tr);
                    Context.SaveChanges();
                }
                File.Delete(tr.Source);
                UpdateTracksList();
              

            }
            else
            {
                MessageBox.Show("enter track please");

            }
        }
    }
}
