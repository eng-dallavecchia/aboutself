using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xamarin.Forms;

namespace AboutSelf.Interfaces
{
    public interface IShare
    {
        void ShareContent(string subject, string message, ImageSource image);
    }
}

