﻿using Data_acces;
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
            dbContext = new MusicPlayerDbContext();
            viewModel = new ViewModel(dbContext);
            this.DataContext = viewModel;
         



        }

        private void click_btnRegister(object sender, RoutedEventArgs e)
        {
            MusicPlayer music = new MusicPlayer(viewModel);
            music.Show();
        }

        private void click_btnLogin(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
