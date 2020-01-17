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
        //TODO: implement different matching
        private enum matchType
        {
            color = 0,
            word = 1,
            circle = 2
        };
        private TextBlock[] boxes;
        private int brushCount;
        private int correctBox;
        private int correctBoxColor;
        private int wrongBoxesColor;
        private int numRight;
        private int numWrong;
   
        private Random rand;



        public MainWindow()
        {
            InitializeComponent();
            boxes = new TextBlock[]
            {
                box1,
                box2,
                box3,
                box4
            };
            brushCount = brushes.Length;
            numRight = 0;
            numWrong = 0;
            rand = new Random();

            randomizeBoxes();
        }

        private void randomizeBoxes()
        {
            correctBox = rand.Next(4);
            correctBoxColor = rand.Next(brushCount); 

            boxes[correctBox].Background = brushes[correctBoxColor];

            while ((wrongBoxesColor = rand.Next(brushCount)) == correctBoxColor) ;

            for (int x = 0; x < 4; x++)
            {
                if (correctBox != x)
                {
                    boxes[x].Background = brushes[wrongBoxesColor];
                }
            }
        }

        public void updateDebug()
        {
            debug.Text = "Right: " + numRight + "\n" + "Wrong: " + numWrong;
        }

        private void Box1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(correctBox == 0)
            {
                numRight++;
            }
            else
            {
                numWrong++;
            }

            randomizeBoxes();
            updateDebug();
            
            
        }

        private void Box2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correctBox == 1)
            {
                numRight++;
            }
            else
            {
                numWrong++;
            }

            randomizeBoxes();
            updateDebug();

        }

        private void Box3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correctBox == 2)
            {
                numRight++;
            }
            else
            {
                numWrong++;
            }

            randomizeBoxes();
            updateDebug();

        }

        private void Box4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correctBox == 3)
            {
                numRight++;
            }
            else
            {
                numWrong++;
            }

            randomizeBoxes();
            updateDebug();

        }
    }
}
