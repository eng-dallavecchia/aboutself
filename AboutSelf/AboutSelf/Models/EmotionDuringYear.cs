using System;
using System.Collections.Generic;
using System.Text;

namespace AboutSelf.Models
{
    public class EmotionDuringYear
    {
        public string Emotion { get; set; }

        public List<int> Month { get; set; } = new List<int>();

        public List<double> Percentage { get; set; } = new List<double>();


        public int Total()
        {
            int sum = 0;

            foreach (int element in Month)
            {
                sum = sum + element;
            }

            return sum;
        }



    }

}

