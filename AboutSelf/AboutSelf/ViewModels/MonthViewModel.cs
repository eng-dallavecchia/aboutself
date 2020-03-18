using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;
using Microcharts;
using AboutSelf.Models;
using SkiaSharp;
using AboutSelf.Interfaces;
using System.Linq;
using System.Diagnostics;
using LiteDB;
using System.IO;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace AboutSelf.ViewModels
{
    public class MonthViewModel : ReactiveObject
    {
        public INavigation Navigation { get; set; }
        public Chart MonthChart { get; set; }
        public Command ShareButtonCommand { get; set; }

        [Reactive] byte[] bytes { get; set; }
        public EmotionData MonthlyEmotionData { get; set; }
        public string Analysis { get; set; }

        public MonthViewModel()
        {
            MonthlyEmotionData = MonthlyEmotions();
            MonthChart = GetMonthChart(MonthlyEmotionData);

            Analysis = AnalyseEmotions(MonthlyEmotionData);
            ShareButtonCommand = new Command(async () => await ShareEmotion(MonthlyEmotionData));

        }

        private EmotionData MonthlyEmotions()
        {

            string connectionString = DependencyService.Get<IDataBaseAccess>().DataBasePath();

            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<Emotion>("emotions");

                EmotionData emotionData = new EmotionData
                {
                    Joy = collection.Count(x => x.Feeling == "joy" && x.Time.Month == DateTime.Now.Month && x.Time.Year == DateTime.Now.Year),
                    Anger = collection.Count(x => x.Feeling == "anger" && x.Time.Month == DateTime.Now.Month && x.Time.Year == DateTime.Now.Year),
                    Disgust = collection.Count(x => x.Feeling == "disgust" && x.Time.Month == DateTime.Now.Month && x.Time.Year == DateTime.Now.Year),
                    Contempt = collection.Count(x => x.Feeling == "contempt" && x.Time.Month == DateTime.Now.Month && x.Time.Year == DateTime.Now.Year),
                    Sadness = collection.Count(x => x.Feeling == "sadness" && x.Time.Month == DateTime.Now.Month && x.Time.Year == DateTime.Now.Year),
                    Fear = collection.Count(x => x.Feeling == "fear" && x.Time.Month == DateTime.Now.Month && x.Time.Year == DateTime.Now.Year),
                    Surprise = collection.Count(x => x.Feeling == "surprise" && x.Time.Month == DateTime.Now.Month && x.Time.Year == DateTime.Now.Year)
                };

                return emotionData;
            }
        }

        private string AnalyseEmotions(EmotionData emotionData)
        {
            Dictionary<string, int> emotionDictionary = new Dictionary<string, int>();
            Dictionary<string, string> emotionReason = new Dictionary<string, string>();

            emotionDictionary.Add("joy", emotionData.Joy);
            emotionDictionary.Add("anger", emotionData.Anger);
            emotionDictionary.Add("disgust", emotionData.Disgust);
            emotionDictionary.Add("contempt", emotionData.Contempt);
            emotionDictionary.Add("sadness", emotionData.Sadness);
            emotionDictionary.Add("fear", emotionData.Fear);
            emotionDictionary.Add("surprise", emotionData.Surprise);


            var maxValue = emotionDictionary.Values.Max();
            var maxEmotions = emotionDictionary.Where(x => x.Value == maxValue);

            Dictionary<string, List<string>> FeelingAndReasons = new Dictionary<string, List<string>>();

            string connectionString = DependencyService.Get<IDataBaseAccess>().DataBasePath();
            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<Emotion>("emotions");


                foreach (var feeling in maxEmotions)
                {
                    var reasonsByFeeling = collection.Find(x => x.Feeling == feeling.Key)
                        .GroupBy(x => x.Reason);

                    var reasonsByFeelingMaxValue = reasonsByFeeling.Max(x => x.Count());
                    var topReasons = reasonsByFeeling.Where(x => x.Count() == reasonsByFeelingMaxValue);


                    List<string> reasonList = new List<string>();
                    foreach (var reason in topReasons)
                    {
                        reasonList.Add(reason.Key);
                    }

                    FeelingAndReasons.Add(feeling.Key, reasonList);

                }
            }

            string finalAnalysis = "Based on your emotional data, we can tell you that: ";

            foreach (var dic in FeelingAndReasons)
            {
                string reasons = "";

                if (dic.Value.Count > 1)
                {
                    reasons = string.Join(", ", dic.Value.Take(dic.Value.Count() - 1));
                    reasons = reasons + " and " + dic.Value.Last();
                }
                else
                {
                    reasons = dic.Value.Last();
                }

                string statement = $"You feel a lot of {dic.Key} because of {reasons}. ";
                finalAnalysis += statement;
            }

            return finalAnalysis;
        }



        private Chart GetMonthChart(EmotionData Data)
        {
            Chart chart = new PieChart();

            var entries = new[]
            {
                new Microcharts.Entry(Data.Joy)
                {
                    Label = "Joy",
                    ValueLabel = Data.Joy.ToString(),
                    Color = SKColor.Parse("#FF9EE4"),
                    TextColor = SKColors.White
                },

            new Microcharts.Entry(Data.Anger)
            {
                Label = "Anger",
                ValueLabel = Data.Anger.ToString(),
                Color = SKColor.Parse("#FF4874"),
                TextColor = SKColors.White

            },

            new Microcharts.Entry(Data.Disgust)
            {
                Label = "Disgust",
                ValueLabel = Data.Disgust.ToString(),
                Color = SKColor.Parse("#C1FF84"),
                TextColor = SKColors.White
            },

                new Microcharts.Entry(Data.Contempt)
            {
                Label = "Contempt",
                ValueLabel = Data.Contempt.ToString(),
                Color = SKColor.Parse("#FFE381"),
                TextColor = SKColors.White
            },
                  new Microcharts.Entry(Data.Sadness)
            {
                Label = "Sadness",
                ValueLabel = Data.Sadness.ToString(),
                Color = SKColor.Parse("#92B7FE"),
                TextColor = SKColors.White
            },

                new Microcharts.Entry(Data.Fear)
            {
                Label = "Fear",
                ValueLabel = Data.Fear.ToString(),
                Color = SKColors.LightGray,
                TextColor = SKColors.White
            },
                new Microcharts.Entry(Data.Surprise)
            {
                Label = "Surprise",
                ValueLabel = Data.Surprise.ToString(),
                Color = SKColor.Parse("#FF9D9D"),
                TextColor = SKColors.White
            }
        };

            chart.Entries = entries;
            chart.BackgroundColor = SKColors.Transparent;


            return chart;
        }



        private async Task ShareEmotion(EmotionData Data)
        {

            Chart chart = new PieChart();
            chart = MonthChart as PieChart;
            chart.IsAnimated = false;
            chart.Entries.ToList().ForEach(x => x.TextColor = SKColors.Purple);
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
