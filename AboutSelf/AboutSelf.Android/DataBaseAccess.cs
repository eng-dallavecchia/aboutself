using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AboutSelf.Interfaces;
using AboutSelf.Droid;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(AboutSelf.Droid.DataBaseAccess))]

namespace AboutSelf.Droid
{
    public class DataBaseAccess : IDataBaseAccess
    {
        public string DataBasePath()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string file = "Base.db";
            string directory = Path.Combine(path, "..", "Library", "Databases");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }



            return Path.Combine(directory, file);
        }
    }
}