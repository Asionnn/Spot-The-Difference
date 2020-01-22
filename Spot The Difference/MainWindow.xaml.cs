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
            circle = 2,
            circlePos = 3
        };
        private TextBlock[] boxes;
        private Label[] lbls;
        private Ellipse[] circles;
        
        private int brushCount;
        private int currentMatchType;
        private int correctColor;
        private int correctColorBox;
        private int wrongColor;
        private int correctWordBox;
        private int correctWord;
        private int correctCircleColorBox;
        private int correctCircleColor;
        private int correctCirclePosBox;
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
            circles = new Ellipse[]
            {
                box1_circle,
                box2_circle,
                box3_circle,
                box4_circle
            };

            words = new List<KeyValuePair<string, string>>();
    

            
            brushCount = brushes.Length;
            numRight = 0;
            numWrong = 0;

            rand = new Random();

            currentMatchType = rand.Next(4);

            populateWords();
            randomizeWords();
            randomizeBoxes();
            randomizeWords();
            randomizeCircleColors();
            randomizeCirclePos();
            updateGameTypeLbl();
            updateDebug();
        }

        private void populateWords()
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
 
        private void updateGameTypeLbl()
        {
            switch (currentMatchType)
            {
                case (int)(matchType.color):
                    gameIndicator.Content = "Color";
                    break;
                case (int)(matchType.word):
                    gameIndicator.Content = "Word";
                    break;
                case (int)(matchType.circle):
                    gameIndicator.Content = "Circle Color";
                    break;
                case (int)(matchType.circlePos):
                    gameIndicator.Content = "Circle Position";
                    break;
            }
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
            correctColorBox = rand.Next(4);
            correctColor= rand.Next(brushCount); 

            boxes[correctColorBox].Background = brushes[correctColor];

            while ((wrongColor = rand.Next(brushCount)) == correctColor) ;

            for (int x = 0; x < 4; x++)
            {
                if (correctColorBox != x)
                {
                    boxes[x].Background = brushes[wrongColor];
                }
            }
        }

        private void randomizeCircleColors()
        {
            correctCircleColorBox = rand.Next(4);
            var wrongCircleColor = rand.Next(brushCount);

            while ((correctCircleColor = rand.Next(brushCount)) == correctColor) ;
            while(wrongCircleColor == correctColor || wrongCircleColor == correctCircleColor || wrongCircleColor == wrongColor)
            {
                wrongCircleColor = rand.Next(brushCount);
            }

            circles[correctCircleColorBox].Fill = brushes[correctCircleColor];

            for(int x = 0; x < 4; x++)
            {
                if(correctCircleColorBox != x)
                {
                    circles[x].Fill = brushes[wrongCircleColor];
                }
            }

        }


        private void setCirclePos(Ellipse c, int x, int y)
        {
            Canvas.SetLeft(c, x);
            Canvas.SetTop(c, y);
        }
        private void randomizeCirclePos()
        {
            correctCirclePosBox = rand.Next(4);

            var correctCirclePos_x = rand.Next(189, 439);
            var correctCirclePos_y = rand.Next(156, 334);
            int wrongCirclePos_x;
            int wrongCirclePos_y;

            while ((wrongCirclePos_x = rand.Next(189, 439)) == correctCirclePos_x) ;
            while ((wrongCirclePos_y = rand.Next(156, 334)) == correctCirclePos_y) ;

            if (correctCirclePosBox == 1)
            {
                correctCirclePos_x += 597;
            }
            if(correctCirclePosBox == 2)
            {
                correctCirclePos_y += 396;
            }
            if(correctCirclePosBox == 3)
            {
                correctCirclePos_x += 597;
                correctCirclePos_y += 396;
            }

            setCirclePos(circles[correctCirclePosBox], correctCirclePos_x, correctCirclePos_y);

            if(correctCirclePosBox != 0)
            {
                setCirclePos(box1_circle, wrongCirclePos_x, wrongCirclePos_y);
            }
            if(correctCirclePosBox != 1)
            {
                setCirclePos(box2_circle, wrongCirclePos_x + 597, wrongCirclePos_y);
            }
            if(correctCirclePosBox != 2)
            {
                setCirclePos(box3_circle, wrongCirclePos_x, wrongCirclePos_y + 396);
            }
            if(correctCirclePosBox != 3)
            {
                setCirclePos(box4_circle, wrongCirclePos_x + 597, wrongCirclePos_y + 396);
            }
        }

        private void updateDebug()
        {
            debug.Text = "Right: " + numRight + "\n" + "Wrong: " + numWrong;
        }

        private void checkAnswer(int box)
        {
            switch (currentMatchType)
            {
                case (int)(matchType.color):
                    if (correctColorBox == box)
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
                    if(correctCircleColorBox == box)
                    {
                        numRight++;
                    }
                    else
                    {
                        numWrong++;
                    }
                    break;
                case (int)(matchType.circlePos):
                    if(correctCirclePosBox == box)
                    {
                        numRight++;
                    }
                    else
                    {
                        numWrong++;
                    }
                    break;
            }

            currentMatchType = rand.Next(4);
            updateGameTypeLbl();
        }

        private void Box1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkAnswer(0);
            randomizeBoxes();
            randomizeWords();
            randomizeCircleColors();
            randomizeCirclePos();

            updateDebug();
       
        }

        private void Box2_MouseDown(object sender, MouseButtonEventArgs e)
        {


            checkAnswer(1);
            randomizeBoxes();
            randomizeWords();
            randomizeCircleColors();
            randomizeCirclePos();

            updateDebug();

        }

        private void Box3_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            checkAnswer(2);
            randomizeBoxes();
            randomizeWords();
            randomizeCircleColors();
            randomizeCirclePos();

            updateDebug();

        }

        private void Box4_MouseDown(object sender, MouseButtonEventArgs e)
        {

            checkAnswer(3);
            randomizeBoxes();
            randomizeWords();
            randomizeCircleColors();
            randomizeCirclePos();

            updateDebug();

        }
    }
}
