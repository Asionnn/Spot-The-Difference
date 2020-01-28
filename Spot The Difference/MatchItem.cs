using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spot_The_Difference
{
    class MatchItem
    {
        public int type { get; set; }
        public bool correct { get; set; }
        public double reactionTime { get; set; }

        public MatchItem(int type, bool correct, double reactionTime)
        {
            this.type = type;
            this.correct = correct;
            this.reactionTime = reactionTime;
        }

        override
        public string ToString()
        {
            string typeStr = "";
            switch (type)
            {
                case 0:
                    typeStr = "background-color";
                    break;
                case 1:
                    typeStr = "word";
                    break;
                case 2:
                    typeStr = "circle-color";
                        break;
                case 3:
                    typeStr = "circle-pos";
                    break;
            }

            return "( " + typeStr + ", " + correct + ", " + reactionTime + "s )"; 
        }
       
    }
}
