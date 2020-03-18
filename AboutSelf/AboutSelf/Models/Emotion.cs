using System;
using System.Collections.Generic;
using System.Text;

namespace AboutSelf.Models
{
    public class Emotion
    {
        public int Id { get; set; }
        public string Feeling { get; set; }
        public string Reason { get; set; }
        public DateTime Time { get; set; }
    }
}
