using AboutSelf.Interfaces;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Plugin.CurrentActivity;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(AboutSelf.Droid.Share))]

namespace AboutSelf.Droid
{
    class Share : Activity, IShare
    {

        public async void ShareContent(string subject, string message, ImageSource image)
        {

            var intent = new Intent(Intent.ActionSend);
            intent.PutExtra(Intent.ExtraText, message);
            intent.SetType("image/png");

            var handler = new StreamImagesourceHandler();
            var bitmap = await handler.LoadImageAsync(image, this);

            var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads
                + Java.IO.File.Separator + "mydiagram.png");

            using (System.IO.FileStream os = new System.IO.FileStream(path.AbsolutePath, System.IO.FileMode.Create))
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, os);
            }

            intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(path));
            CrossCurrentActivity.Current.Activity.StartActivity(Intent.CreateChooser(intent, "Share Image"));
        }



    }
}