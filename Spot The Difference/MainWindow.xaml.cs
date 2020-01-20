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

        private List<KeyValuePair<string, string>> words;
        private enum matchType
        {
            color = 0,
            word = 1,
            circle = 2
        };
        private TextBlock[] boxes;
        private Label[] lbls;

        private int brushCount;
        private int correctBox;
        private int correctBoxColor;
        private int wrongBoxesColor;
        private int currentMatchType;
        private int correctWordBox;
        private int correctWord;
        private int wrongWord;
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
            lbls = new Label[]
            {
                lbl1,
                lbl2,
                lbl3,
                lbl4
            };

            words = new List<KeyValuePair<string, string>>();
            
            brushCount = brushes.Length;
            numRight = 0;
            numWrong = 0;

            rand = new Random();

            currentMatchType = rand.Next(3);
            populateWords();
            randomizeWords();
            randomizeBoxes();
            randomizeWords();
            updateDebug();
        }

        public void populateWords()
        {
            words.Add(new KeyValuePair<string, string>("Bring", "Brink"));
            words.Add(new KeyValuePair<string, string>("Late", "Mate"));
            words.Add(new KeyValuePair<string, string>("Zoom", "Room"));
            words.Add(new KeyValuePair<string, string>("Ward", "Word"));
            words.Add(new KeyValuePair<string, string>("Clamber", "Climber"));
            words.Add(new KeyValuePair<string, string>("More", "Mourn"));
            words.Add(new KeyValuePair<string, string>("Poll", "Pole"));
            words.Add(new KeyValuePair<string, string>("Time", "Tine"));
            words.Add(new KeyValuePair<string, string>("Needle", "Noodle"));
            words.Add(new KeyValuePair<string, string>("Rot", "Pot"));
            words.Add(new KeyValuePair<string, string>("Hear", "Here"));
            words.Add(new KeyValuePair<string, string>("Sever", "Sewer"));
            words.Add(new KeyValuePair<string, string>("Wane", "Ware"));
            words.Add(new KeyValuePair<string, string>("Spill", "Skill"));
            words.Add(new KeyValuePair<string, string>("Mind", "Mine"));
            words.Add(new KeyValuePair<string, string>("Knot", "Know"));
            words.Add(new KeyValuePair<string, string>("Quiet", "Quite"));
            words.Add(new KeyValuePair<string, string>("That", "This"));
            words.Add(new KeyValuePair<string, string>("From", "Form"));
            words.Add(new KeyValuePair<string, string>("Wish", "Fish"));
        }

        private void randomizeWords()
        {
            int wordCount = words.Count();
            var wordArr = words.ToArray();
            correctWordBox = rand.Next(4);
            correctWord = rand.Next(wordCount);

            // Set the words;
            lbls[correctWordBox].Content = wordArr[correctWord].Key;

            for(int x = 0; x < 4; x++)
            {
                if(correctWordBox != x)
                {
                    lbls[x].Content = wordArr[correctWord].Value;
                }
            }

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
            var current = "";
            switch (currentMatchType)
            {
                case 0:
                    current = "color";
                        break;
                case 1:
                    current = "word";
                    break;
                case 2:
                    current = "circle";
                    break;
            }
            debug.Text = "Right: " + numRight + "\n" + "Wrong: " + numWrong + "\n" + "Type: " + current;
        }

        public void checkAnswer(int box)
        {
            switch (currentMatchType)
            {
                case (int)(matchType.color):
                    if (correctBox == box)
                    {
                        numRight++;
                    }
                    else
                    {
                        numWrong++;
                    }
                    break;
                case (int)(matchType.word):
                    if (correctWordBox == box)
                    {
                        numRight++;
                    }
                    else
                    {
                        numWrong++;
                    }
                    break;
                case (int)(matchType.circle):
                    break;
            }

            currentMatchType = rand.Next(3);
        }

        private void Box1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkAnswer(0);
            randomizeBoxes();
            randomizeWords();
            updateDebug();
       
        }

        private void Box2_MouseDown(object sender, MouseButtonEventArgs e)
        {


            checkAnswer(1);
            randomizeBoxes();
            randomizeWords();
            updateDebug();

        }

        private void Box3_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            checkAnswer(2);
            randomizeBoxes();
            randomizeWords();
            updateDebug();

        }

        private void Box4_MouseDown(object sender, MouseButtonEventArgs e)
        {

            checkAnswer(3);
            randomizeBoxes();
            randomizeWords();
            updateDebug();

        }
    }
}
