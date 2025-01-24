using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace RGB_michatko
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            valuePairs = new Dictionary<TextBox, Slider>()
            {
                { redBox, redSlider },
                { greenBox, greenSlider },
                { blueBox, blueSlider }
            };
        }
        Dictionary<TextBox, Slider> valuePairs;
        private void ChangeRectangleColor()
        {
            byte red = Convert.ToByte(redBox.Text);
            byte green = Convert.ToByte(greenBox.Text);
            byte blue = Convert.ToByte(blueBox.Text);
            rectangle.Fill = new SolidColorBrush(Color.FromRgb(red, green, blue));
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (KeyValuePair<TextBox,Slider> pair in valuePairs)
            {
                if (pair.Value == (Slider)sender)
                {
                    pair.Key.Text = ((Slider)sender).Value.ToString();
                }
            }
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (text != "" && Convert.ToInt32(text) > 255)
            {
                MessageBox.Show("Zadávejte pouze hodnoty nižší než 256");
                ((TextBox)sender).Text = text.Substring(0, text.Length - 1);
            }
            if (valuePairs != null)
            {
                valuePairs[((TextBox)sender)].Value = Convert.ToInt32(((TextBox)sender).Text);
                ChangeRectangleColor();
            }
        }
    }
}