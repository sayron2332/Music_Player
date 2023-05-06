using Data_acces;
using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Music_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel;
        MusicPlayerDbContext dbContext;
        public MainWindow()
        {
            InitializeComponent();
            dbContext = MusicPlayerDbContext.Initialize();
            viewModel = ViewModel.Initialize();
            this.DataContext = viewModel;
            MessageBox.Show(viewModel.GetHashCode().ToString());



        }

        private void click_btnRegister(object sender, RoutedEventArgs e)
        {
            if (viewModel.Login != " " && !String.IsNullOrWhiteSpace(viewModel.Login) && viewModel.Password != " " && !String.IsNullOrWhiteSpace(viewModel.Password))
            {
                User user = new User();
                user.Login = viewModel.Login;
                user.Password = viewModel.Password;

                User userForDB = dbContext.Users.FirstOrDefault(u  => u.Login == user.Login);
                if (userForDB != null && userForDB.Password == user.Password)
                {
                    MessageBox.Show("such user already exists");
                }
                else
                {
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    MessageBox.Show("You register!!!");
                }

              
            }
            else
            {
                MessageBox.Show("Enter corect login or password");
            }
           
        }

        private void click_btnLogin(object sender, RoutedEventArgs e)
        {
            User User = dbContext.Users.FirstOrDefault(u => u.Login == viewModel.Login);
            if (User != null && User.Password == viewModel.Password)
            {
               
                MusicPlayer music = new MusicPlayer(User);
                music.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("We dont have youre account");
            }
          

          
            
        }
    }
}
