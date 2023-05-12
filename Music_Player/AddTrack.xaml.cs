using Data_acces;
using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Path = System.IO.Path;
namespace Music_Player
{
    /// <summary>
    /// Interaction logic for AddTrack.xaml
    /// </summary>
    public partial class AddTrack : Window
    {
        ViewModel Model;
        User User;
        public AddTrack(User user)
        {
            InitializeComponent();
            User = user;
            Model = ViewModel.Initialize(user);
            this.DataContext = Model;
        }

        private void Click_btnEnterTrack(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
         
            if (dialog.ShowDialog() == true)
            {
                Model.Source = dialog.FileName;
             
            }
            else
            {
               
            }
        }

        private void Click_btnAddTrack(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Model.Source) && !string.IsNullOrEmpty(Model.TrackName) && !string.IsNullOrEmpty(Model.AvtorName))
            {
              
               
                string Destination = "Music\\";
                
                string destFilePath = Path.Combine(Destination, Path.GetFileName(Model.TrackName + ".mp3"));
                File.Copy(Model.Source, destFilePath, true);

                Track track = new Track();
                track.Source = destFilePath;
                track.Name = Model.TrackName;
                track.Author = Model.AvtorName;
                track.Date = DateTime.Now;


                using (MusicPlayerDbContext Context = new MusicPlayerDbContext())
                {
                    Track TrackForDB = Context.Tracks.FirstOrDefault(u => u.Name == track.Name);
                    if (TrackForDB != null && TrackForDB.Name == track.Name)
                    {
                        MessageBox.Show("such track already exists");

                    }
                    else
                    {
                        Model.AddTrack(track);
                        Context.Tracks.Add(track);
                        Context.SaveChanges();
                        MessageBox.Show("Track add");
                        this.Close();
                        Model.AvtorName = null;
                        Model.TrackName = null;
                        Model.Source = null;
                    }
                

                }
            }
            else
            {
                MessageBox.Show("Enter full property");
            }
           
        }
    }
}
