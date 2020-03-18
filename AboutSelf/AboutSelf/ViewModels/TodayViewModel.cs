using AboutSelf.Interfaces;
using AboutSelf.Models;
using Microcharts;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.IO;
using System.Diagnostics;
using LiteDB;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;

namespace AboutSelf.ViewModels
{
    public class TodayViewModel : ReactiveObject
    {
        public INavigation Navigation { get; set; }
        public Command ShareButtonCommand { get; set; }

        public Chart DayChart { get; set; }
        [Reactive] byte[] bytes { get; set; }


        public EmotionData DailyEmotionData { get; set; }

        public TodayViewModel()
        {
            DailyEmotionData = DailyEmotions();
            DayChart = GetDayChart(DailyEmotionData);
            ShareButtonCommand = new Command(async () => await ShareEmotion(DailyEmotionData));


        }

        private EmotionData DailyEmotions()
        {

            string connectionString = DependencyService.Get<IDataBaseAccess>().DataBasePath();

            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<Emotion>("emotions");

                EmotionData emotionData = new EmotionData
                {
                    Joy = collection.Count(x => x.Feeling == "joy" && x.Time.Date == DateTime.Now.Date),
                    Anger = collection.Count(x => x.Feeling == "anger" && x.Time.Date == DateTime.Now.Date),
                    Disgust = collection.Count(x => x.Feeling == "disgust" && x.Time.Date == DateTime.Now.Date),
                    Contempt = collection.Count(x => x.Feeling == "contempt" && x.Time.Date == DateTime.Now.Date),
                    Sadness = collection.Count(x => x.Feeling == "sadness" && x.Time.Date == DateTime.Now.Date),
                    Fear = collection.Count(x => x.Feeling == "fear" && x.Time.Date == DateTime.Now.Date),
                    Surprise = collection.Count(x => x.Feeling == "surprise" && x.Time.Date == DateTime.Now.Date)
                };

                return emotionData;
            }
        }

        private Chart GetDayChart(EmotionData Data)
        {

            Chart chart = new BarChart();

            var entries = new[]
            {
                new Microcharts.Entry(Data.Joy)
                {
                    Label = "Joy",
                    TextColor = SKColor.Parse("#FF9EE4"),
                    ValueLabel = Data.Joy.ToString(),
                    Color = SKColor.Parse("#FF9EE4")

                },

            new Microcharts.Entry(Data.Anger)
            {
                Label = "Anger",
                TextColor = SKColor.Parse("#FF4874"),
                ValueLabel = Data.Anger.ToString(),
                Color = SKColor.Parse("#FF4874")
            },

            new Microcharts.Entry(Data.Disgust)
            {
                Label = "Disgust",
                //Label = ".",
                ValueLabel = Data.Disgust.ToString(),
                Color = SKColor.Parse("#C1FF84"),
            },

                new Microcharts.Entry(Data.Contempt)
            {
                Label = "Contempt",
                //Label = ".",
                ValueLabel = Data.Contempt.ToString(),
                Color = SKColor.Parse("#FFE381")
            },
                  new Microcharts.Entry(Data.Sadness)
            {
                Label = "Sadness",
                //Label = ".",
                ValueLabel = Data.Sadness.ToString(),
                Color = SKColor.Parse("#92B7FE")
            },

                new Microcharts.Entry(Data.Fear)
            {
                Label = "Fear",
                //Label = ".",
                ValueLabel = Data.Fear.ToString(),
                Color = SKColor.Parse("#FFFFFF")
            },
                new Microcharts.Entry(Data.Surprise)
            {
                Label = "Surprise",
                //Label = ".",
                ValueLabel = Data.Surprise.ToString(),
                Color = SKColor.Parse("#FF9D9D")
            }
        };

            chart.Entries = entries;
            chart.Margin = 20;
            chart.BackgroundColor = SKColors.Transparent;
            chart.LabelColor = SKColors.Transparent;


            return chart;
        }

        private async Task ShareEmotion(EmotionData Data)
        {

            Chart chart = new BarChart();
            chart = DayChart as BarChart;
            chart.IsAnimated = false;
            chart.Margin = 20;
            chart.LabelColor = SKColors.Purple;


            var bmp = new SKBitmap(500, 500);
            var canvas = new SKCanvas(bmp);
            var image = SKImage.FromPixels(bmp.PeekPixels());
            chart.DrawContent(canvas, 500, 500);
            canvas.Save();


            using (MemoryStream ms = new MemoryStream())
            {
                image.Encode(SKEncodedImageFormat.Png, 4).AsStream().CopyTo(ms);

                var str = Convert.ToBase64String(ms.ToArray());
                bytes = Convert.FromBase64String(str);

            }
            try
            {
                PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    status = results[Permission.Storage];
                }

                if (status != PermissionStatus.Granted)
                {
                    return;
                }
                else
                {

                    DependencyService.Get<IShare>()
                        .ShareContent("", "AboutSelf", ImageSource.FromStream(() => new MemoryStream(bytes)));

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }



    }
}