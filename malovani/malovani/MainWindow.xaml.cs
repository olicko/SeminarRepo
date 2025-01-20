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

namespace malovani
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Image"; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "Image files (.png)|*.png"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
            }
        }

        private void SAve(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Image"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Image files (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result.Value)
            {
                // Save document
                string filename = dlg.FileName;
            }
        }
        public Color penColor;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 picker = new Window1();
            picker.ShowDialog();

            if (picker.Success)
            {
                penColor = picker.Color;
            }
            clrButton.Background = new SolidColorBrush(penColor);
        }
        private bool isDrawing;
        private double circleRadius = 1;
        private void DrawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                isDrawing = true;
                circleRadius = ThicknessSlider.Value;
            }
        }

        private void DrawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(isDrawing)
            {
                isDrawing = false;
            }
        }

        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                System.Windows.Point currentPoint = e.GetPosition(DrawCanvas);

                Ellipse circle = new Ellipse
                {
                    Width = circleRadius,
                    Height = circleRadius,
                    Fill = new SolidColorBrush(penColor)
                };

                Canvas.SetLeft(circle, currentPoint.X - circleRadius);
                Canvas.SetTop(circle, currentPoint.Y - circleRadius);

                DrawCanvas.Children.Add(circle);
            }

        }
    }
}
