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

namespace Spot_The_Difference
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SolidColorBrush[] brushes =
        {
            Brushes.Red,
            Brushes.Blue,
            Brushes.Beige,
            Brushes.AntiqueWhite,
            Brushes.Azure,
            Brushes.Coral,
            Brushes.DarkGray,
            Brushes.Green,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.Pink,
            Brushes.Purple,
            Brushes.Gold,
            Brushes.LightGray,
            Brushes.GreenYellow,
            Brushes.Brown,
            Brushes.RosyBrown,
            Brushes.RoyalBlue,
            Brushes.Navy,
            Brushes.Tan
        };
        private int brushCount = brushes.Length;
        private Random rand;
        public MainWindow()
        {
            InitializeComponent();
            rand = new Random();
        }

        private void Box1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int num = rand.Next(brushCount);
            box1.Background = brushes[num];
            box2.Background = brushes[num];
            box3.Background = brushes[num];
            num = rand.Next(brushCount);
            box4.Background = brushes[num];
        }

        private void Box2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            box2.Background = brushes[rand.Next(brushCount)];
        }

        private void Box3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            box3.Background = brushes[rand.Next(brushCount)];
        }

        private void Box4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            box4.Background = brushes[rand.Next(brushCount)];
        }
    }
}
