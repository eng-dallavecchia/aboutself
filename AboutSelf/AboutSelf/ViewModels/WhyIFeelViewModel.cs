using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;
using AboutSelf.Models;
using LiteDB;
using AboutSelf.Interfaces;
using System.Diagnostics;
using System.IO;
using AboutSelf.Pages;
using LiteDB.Engine;

namespace AboutSelf.ViewModels
{
    public class WhyIFeelViewModel : ReactiveObject
    {
        public INavigation Navigation { get; set; }

        private string Feeling { get; set; }
        private string Reason { get; set; }

        public Command FamilyButtonCommand { get; set; }
        public Command FinancesButtonCommand { get; set; }
        public Command WorkButtonCommand { get; set; }
        public Command LoveButtonCommand { get; set; }
        public Command MeaningButtonCommand { get; set; }
        public Command HealthButtonCommand { get; set; }
        public Command SociallifeButtonCommand { get; set; }
        public Command SelfesteemButtonCommand { get; set; }

        public WhyIFeelViewModel(string feeling)
        {
            Feeling = feeling;

            FamilyButtonCommand = new Command(async () => await FamilyClicked());
            FinancesButtonCommand = new Command(async () => await FinancesClicked());
            WorkButtonCommand = new Command(async () => await WorkClicked());
            LoveButtonCommand = new Command(async () => await LoveClicked());
            MeaningButtonCommand = new Command(async () => await MeaningClicked());
            HealthButtonCommand = new Command(async () => await HealthClicked());
            SociallifeButtonCommand = new Command(async () => await SociallifeClicked());
            SelfesteemButtonCommand = new Command(async () => await SelfesteemClicked());
        }

        private async Task FamilyClicked()
        {
            Reason = "family";
            InsertData();
            await Navigation.PushAsync(new Today());

        }

        private async Task FinancesClicked()
        {
            Reason = "finances";
            InsertData();
            await Navigation.PushAsync(new Today());

        }

        private async Task WorkClicked()
        {
            Reason = "work";
            InsertData();
            await Navigation.PushAsync(new Today());

        }

        private async Task LoveClicked()
        {
            Reason = "love";
            InsertData();
            await Navigation.PushAsync(new Today());

        }

        private async Task MeaningClicked()
        {
            Reason = "meaning";
            InsertData();
            await Navigation.PushAsync(new Today());

        }

        private async Task HealthClicked()
        {
            Reason = "health";
            InsertData();
            await Navigation.PushAsync(new Today());

        }

        private async Task SociallifeClicked()
        {
            Reason = "social life";
            InsertData();
            await Navigation.PushAsync(new Today());

        }

        private async Task SelfesteemClicked()
        {
            Reason = "self esteem";
            InsertData();
            await Navigation.PushAsync(new Today());

        }


        public void InsertData()
        {
            Emotion emotion = new Emotion
            {
                Feeling = Feeling,
                Reason = Reason,
                Time = DateTime.Now
            };

            string connectionString = DependencyService.Get<IDataBaseAccess>().DataBasePath();

            Debug.WriteLine(DependencyService.Get<IDataBaseAccess>().DataBasePath());

            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<Emotion>("emotions");
                collection.Insert(emotion);
            }


        }


    }
}
