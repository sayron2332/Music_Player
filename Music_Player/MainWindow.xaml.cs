using Data_acces;
using Data_acces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public User StaticUser;
        public MainWindow()
        {
            InitializeComponent();
            dbContext = new MusicPlayerDbContext();
          
            this.DataContext = viewModel;
        



        }

        private void click_btnRegister(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text != " " && !String.IsNullOrWhiteSpace(txtLogin.Text) && txtPassword.Text != " " && !String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                User user = new User();
                user.Login = txtLogin.Text;
                user.Password = txtPassword.Text;

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

         
            StaticUser = dbContext.Users.FirstOrDefault(u => u.Login == txtLogin.Text);
            if (StaticUser != null && StaticUser.Password == txtPassword.Text)
            {
                viewModel = ViewModel.Initialize(StaticUser);
                MusicPlayer music = new MusicPlayer(StaticUser);
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
