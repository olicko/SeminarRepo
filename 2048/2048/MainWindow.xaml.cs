﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateGameGrid();
        }

        public void CreateGameGrid()
        {
            for (int i = 0; i < 4; i++) 
            {
                for (int j = 0; j < 4; j++)
                { 
                    Label label = new Label();

                    Grid.SetRow(label, i);
                    Grid.SetColumn(label, j);

                    
                }
            }
        }
    }
}