﻿using Data_acces;
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
    public partial class MusicPlayer : Window
    {
        

        public MusicPlayer(ViewModel Model)
        {
            InitializeComponent();
            //Context = new MusicPlayerDbContext();
            //Model = new ViewModel(); 
            
            MessageBox.Show(Model.Password);
        }
    }
}