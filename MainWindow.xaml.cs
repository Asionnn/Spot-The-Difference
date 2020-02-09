using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Stats stats;

        private bool correct;

        private Stopwatch sw;
   
        private Random rand;

        private ArrayList times;
        private ArrayList matches;

        private double totalTime;
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
            correct = false;

            stats = new Stats();

            totalTime = 0;

            sw = new Stopwatch();
            rand = new Random();

            currentMatchType = rand.Next(4);

            populateWords();
            randomizeWords();
            randomizeBoxes();
            randomizeCircleColors();
            randomizeCirclePos();
            updateGameTypeLbl();
            updateDebug();

  
            matches = new ArrayList();

            sw.Start();
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
                    gameIndicator.Content = "BackGround-Color";
                    break;
                case (int)(matchType.word):
                    gameIndicator.Content = "Word";
                    break;
                case (int)(matchType.circle):
                    gameIndicator.Content = "Circle-Color";
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
            while ((correctColorBox = rand.Next(4)) == correctWordBox) ;
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
            while (correctCircleColorBox == correctColorBox || correctCircleColorBox == correctWordBox)
            {
                correctCircleColorBox = rand.Next(4);
            }
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
            while (correctCirclePosBox == correctColorBox || correctCirclePosBox == correctWordBox || correctCirclePosBox == correctCircleColorBox)
            {
                correctCirclePosBox = rand.Next(4);
            }

            var correctCirclePos_x = rand.Next(89, 171);
            var correctCirclePos_y = rand.Next(97, 149);
            int wrongCirclePos_x;
            int wrongCirclePos_y;

            while ((wrongCirclePos_x = rand.Next(89, 164)) == correctCirclePos_x) ;
            while ((wrongCirclePos_y = rand.Next(97, 152)) == correctCirclePos_y) ;

            if (correctCirclePosBox == 1)
            {
                correctCirclePos_x += 175;
            }
            if(correctCirclePosBox == 2)
            {
                correctCirclePos_y += 128;
            }
            if(correctCirclePosBox == 3)
            {
                correctCirclePos_x += 175;
                correctCirclePos_y += 128;
            }

            setCirclePos(circles[correctCirclePosBox], correctCirclePos_x, correctCirclePos_y);

            if(correctCirclePosBox != 0)
            {
                setCirclePos(box1_circle, wrongCirclePos_x, wrongCirclePos_y);
            }
            if(correctCirclePosBox != 1)
            {
                setCirclePos(box2_circle, wrongCirclePos_x + 175, wrongCirclePos_y);
            }
            if(correctCirclePosBox != 2)
            {
                setCirclePos(box3_circle, wrongCirclePos_x, wrongCirclePos_y + 128);
            }
            if(correctCirclePosBox != 3)
            {
                setCirclePos(box4_circle, wrongCirclePos_x + 175, wrongCirclePos_y + 128);
            }
        }

        private void updateDebug()
        {
            debug.Text = "Right: " + numRight + "\n" + "Wrong: " + numWrong;
        }

        private void checkAnswer(int box)
        {
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            double ms = ts.TotalMilliseconds;
            if (ms < 6000)
            {
                totalTime += ms;
                switch (currentMatchType)
                {
                    case (int)(matchType.color):
                        if (correctColorBox == box)
                        {
                            numRight++;
                            stats.bgRight++;
                            correct = true;
                        }
                        else
                        {
                            numWrong++;
                            stats.bgWrong++;
                            correct = false;
                        }
                        stats.bgTime += ms;
                        break;
                    case (int)(matchType.word):
                        if (correctWordBox == box)
                        {
                            numRight++;
                            stats.wordRight++;
                            correct = true;
                        }
                        else
                        {
                            numWrong++;
                            stats.wordWrong++;
                            correct = false;
                        }
                        stats.wordTime += ms;
                        break;
                    case (int)(matchType.circle):
                        if (correctCircleColorBox == box)
                        {
                            numRight++;
                            stats.cColorRight++;
                            correct = true;
                        }
                        else
                        {
                            numWrong++;
                            stats.cColorWrong++;
                            correct = false;
                        }
                        stats.cColorTime += ms;
                        break;
                    case (int)(matchType.circlePos):
                        if (correctCirclePosBox == box)
                        {
                            numRight++;
                            stats.cColorPosRight++;
                            correct = true;
                        }
                        else
                        {
                            numWrong++;
                            stats.cColorPosWrong++;
                            correct = false;
                        }
                        stats.cColorPosTime += ms;
                        break;
                }

                matches.Add(new MatchItem(currentMatchType, correct, Math.Round(ts.TotalMilliseconds / 1000.0, 2)));
            }
            currentMatchType = rand.Next(4);
            updateGameTypeLbl();

            sw.Reset();
            sw.Start();
        }

        private void Box1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkAnswer(0);
            randomizeWords();
            randomizeBoxes();
            randomizeCircleColors();
            randomizeCirclePos();
            updateDebug();
        }

        private void Box2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkAnswer(1);
            randomizeWords();
            randomizeBoxes();
            randomizeCircleColors();
            randomizeCirclePos();
            updateDebug();
        }

        private void Box3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkAnswer(2);
            randomizeWords();
            randomizeBoxes();
            randomizeCircleColors();
            randomizeCirclePos();
            updateDebug();
        }

        private void Box4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkAnswer(3);
            randomizeWords();
            randomizeBoxes();
            randomizeCircleColors();
            randomizeCirclePos();
            updateDebug();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string time = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss");
            var fileName = @"C:\Users\minisim\source\repos\Spot-The-Difference\data\DifferenceData.txt";
            //var fileName = "data2.txt";
            double percentCorrect = Math.Round(((numRight*1.0) / (numRight + numWrong)) * 100, 2);
            double avgTime = Math.Round((totalTime / (numRight + numWrong))/1000, 2);

            //[#Trials, %Correct, Total Time, Avg reaction time]

            string StringLiteral =
@"{0,-35}{1,-41}{2,-35}{3,-35}{4,-35}{5,-35}
{6,-35}{7,-41}{8,-35}{9,-35}{10,-35}{11,-35}
============================================================================================================================================================================================================================
";
            var data = string.Format(StringLiteral,
                "Date",
                "Overall",
                "bg-color",
                "word",
                "circle-color",
                "circle-pos",
                time,
                "[ " + (numWrong + numRight) + ", " + percentCorrect + "%, " + Math.Round(totalTime / 1000, 2) + "s, " + avgTime + "s" + " ]",
                "[ " + (stats.bgRight + stats.bgWrong) + ", " + stats.getbgAccuracy() + "%, " + Math.Round(stats.bgTime / 1000, 2) + "s, " + Math.Round((stats.bgTime / (stats.bgRight + stats.bgWrong)) / 1000, 2) + "s" + " ]",
                "[ " + (stats.wordRight + stats.wordWrong) + ", " + stats.getwordAccuracy() + "%, " + Math.Round(stats.wordTime / 1000, 2) + "s, " + Math.Round((stats.wordTime / (stats.wordRight + stats.wordWrong)) / 1000, 2) + "s" + " ]",
                "[ " + (stats.cColorRight + stats.cColorWrong) + ", " + stats.getccolorAccuracy() + "%, " + Math.Round(stats.cColorTime / 1000, 2) + "s, " + Math.Round((stats.cColorTime / (stats.cColorRight + stats.cColorWrong)) / 1000, 2) + "s" + " ]",
                "[ " + (stats.cColorPosRight + stats.cColorPosWrong) + ", " + stats.getccolorposAccuracy() + "%, " + Math.Round(stats.cColorPosTime / 1000, 2) + "s, " + Math.Round((stats.cColorPosTime / (stats.cColorPosRight + stats.cColorPosWrong)) / 1000, 2) + "s" + " ]"
                );
            
            System.IO.File.AppendAllText(fileName, data);
        }
    }
}
