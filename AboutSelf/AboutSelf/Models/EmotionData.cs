using System;
using System.Collections.Generic;
using System.Text;

namespace AboutSelf.Models
{
    public class EmotionData
    {
        public int Joy { get; set; }
        public int Anger { get; set; }
        public int Disgust { get; set; }
        public int Contempt { get; set; }
        public int Sadness { get; set; }
        public int Fear { get; set; }
        public int Surprise { get; set; }

        public int Total()
        {
            int sum = Joy + Anger + Disgust + Contempt + Sadness + Fear + Surprise;

            return sum;
        }

        public double Percentage (int input)
        {
            double sum = (double) Total();
            double value = (double) input;
            double percentage = Math.Round(value/sum*100, 1);

            return percentage;
        }

    }



}
