using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spot_The_Difference
{

    class Stats
    {
        public int bgWrong { get; set; }
        public int bgRight { get; set; }
        public int wordRight { get; set; }
        public int wordWrong { get; set; }
        public int cColorRight { get; set; }
        public int cColorWrong { get; set; }
        public int cColorPosRight { get; set; }
        public int cColorPosWrong { get; set; }
        public double bgTime { get; set; }
        public double wordTime { get; set; }
        public double cColorTime { get; set; }
        public double cColorPosTime { get; set; }

        public double getbgAccuracy()
        {
            return Math.Round((bgRight / 1.0 * (bgRight + bgWrong)), 2);
        }
        public double getwordAccuracy()
        {
            return Math.Round((wordRight / 1.0 * (wordRight + wordWrong)), 2);
        }
        public double getccolorAccuracy()
        {
            return Math.Round((cColorRight / 1.0 * (cColorRight + cColorWrong)), 2);
        }
        public double getccolorposAccuracy()
        {
            return Math.Round((cColorPosRight / 1.0 * (cColorPosRight + cColorPosWrong)), 2);
        }
    }
}
