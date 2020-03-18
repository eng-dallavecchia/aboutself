using System;
using System.Collections.Generic;
using System.Text;
using AboutSelf.Interfaces;
using AboutSelf.Models;
using LiteDB;
using Microcharts;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;
using Xamarin.Forms;

namespace AboutSelf.ViewModels
{
    class YearViewModel : ReactiveObject
    {
        public INavigation Navigation { get; set; }

        public Chart JoyChart { get; set; }
        public Chart AngerChart { get; set; }
        public Chart DisgustChart { get; set; }
        public Chart ContemptChart { get; set; }
        public Chart SadnessChart { get; set; }
        public Chart FearChart { get; set; }
        public Chart SurpriseChart { get; set; }

        public List<EmotionDuringYear> YearlyEmotionsData { get; set; }


        public YearViewModel()
        {
            YearlyEmotionsData = YearlyEmotions();
            JoyChart = GetYearChart(YearlyEmotionsData.Find(x => x.Emotion == "joy"), SKColor.Parse("#FF9EE4"));
            AngerChart = GetYearChart(YearlyEmotionsData.Find(x => x.Emotion == "anger"), SKColor.Parse("#FF4874"));
            DisgustChart = GetYearChart(YearlyEmotionsData.Find(x => x.Emotion == "disgust"), SKColor.Parse("#C1FF84"));
            ContemptChart = GetYearChart(YearlyEmotionsData.Find(x => x.Emotion == "contempt"), SKColor.Parse("#FFE381"));
            SadnessChart = GetYearChart(YearlyEmotionsData.Find(x => x.Emotion == "sadness"), SKColor.Parse("#92B7FE"));
            FearChart = GetYearChart(YearlyEmotionsData.Find(x => x.Emotion == "fear"), SKColor.Parse("#FFFFFF"));
            SurpriseChart = GetYearChart(YearlyEmotionsData.Find(x => x.Emotion == "surprise"), SKColor.Parse("#FF9D9D"));
        }


        private List<EmotionDuringYear> YearlyEmotions()
        {

            string connectionString = DependencyService.Get<IDataBaseAccess>().DataBasePath();

            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<Emotion>("emotions");

                string[] emotions = { "joy", "anger", "disgust", "contempt", "sadness", "fear", "surprise" };


                List<EmotionDuringYear> EmotionsDuringYear = new List<EmotionDuringYear>();

                List<int> MonthTotal = new List<int>();

                MonthTotal.Add(collection.Count(x => x.Time.Month == 0 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 1 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 2 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 3 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 4 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 5 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 6 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 7 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 8 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 9 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 10 && x.Time.Year == DateTime.Now.Year));
                MonthTotal.Add(collection.Count(x => x.Time.Month == 11 && x.Time.Year == DateTime.Now.Year));

                foreach (string element in emotions)
                {

                    List<int> MonthCount = new List<int>();

                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 0 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 1 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 2 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 3 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 4 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 5 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 6 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 7 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 8 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 9 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 10 && x.Time.Year == DateTime.Now.Year));
                    MonthCount.Add(collection.Count(x => x.Feeling == element && x.Time.Month == 11 && x.Time.Year == DateTime.Now.Year));


                    List<double> MonthPercentage = new List<double>();

                    for (int i = 0; i < 12; i++)
                    {
                        double value = (double)MonthCount[i];
                        double total = (double)MonthTotal[i];
                        double monthPercent = 0;

                        if (total != 0)
                        {
                            monthPercent = value / total * 100;
                        }

                        MonthPercentage.Add(Math.Round(monthPercent, 1));

                    }

                    EmotionDuringYear emotionData = new EmotionDuringYear
                    {
                        Emotion = element,
                        Month = MonthCount,
                        Percentage = MonthPercentage

                    };

                    EmotionsDuringYear.Add(emotionData);
                }

                return EmotionsDuringYear;
            }
        }


        private Chart GetYearChart(EmotionDuringYear EmotionDuringYear, SKColor EmotionColor)
        {
            Chart chart = new LineChart();

            List<double> Percentages = EmotionDuringYear.Percentage;

            var entries = new[]
            {
                new Microcharts.Entry((float)Percentages[0])
                {
                    Label = "January",
                    ValueLabel = Percentages[0].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },

                new Microcharts.Entry((float)Percentages[1])
                {
                    Label = "February",
                    ValueLabel = Percentages[1].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[2])
                {
                    Label = "March",
                    ValueLabel = Percentages[2].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[3])
                {
                    Label = "April",
                    ValueLabel = Percentages[3].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[4])
                {
                    Label = "May",
                    ValueLabel = Percentages[4].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[5])
                {
                    Label = "June",
                    ValueLabel = Percentages[5].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[6])
                {
                    Label = "July",
                    ValueLabel = Percentages[6].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[7])
                {
                    Label = "August",
                    ValueLabel = Percentages[7].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[8])
                {
                    Label = "September",
                    ValueLabel = Percentages[8].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[9])
                {
                    Label = "October",
                    ValueLabel = Percentages[9].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[10])
                {
                    Label = "November",
                    ValueLabel = Percentages[10].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
                new Microcharts.Entry((float)Percentages[11])
                {
                    Label = "December",
                    ValueLabel = Percentages[11].ToString() + "%",
                    Color = EmotionColor,
                    TextColor = SKColors.White
                },
        };

            chart.Entries = entries;
            chart.BackgroundColor = SKColors.Transparent;
            chart.LabelColor = SKColors.White;
            chart.MaxValue = 100;
            chart.MinValue = 0;


            return chart;
        }



    }
}
