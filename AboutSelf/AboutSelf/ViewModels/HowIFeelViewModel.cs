using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AboutSelf.Pages;

namespace AboutSelf.ViewModels
{
    public class HowIFeelViewModel : ReactiveObject
    {
        public INavigation Navigation { get; set; }

        public Command JoyButtonCommand { get; set; }
        public Command AngerButtonCommand { get; set; }
        public Command DisgustButtonCommand { get; set; }
        public Command ContemptButtonCommand { get; set; }
        public Command SadnessButtonCommand { get; set; }
        public Command FearButtonCommand { get; set; }
        public Command SurpriseButtonCommand { get; set; }

        public HowIFeelViewModel()
        {
            JoyButtonCommand = new Command(async () => await JoyClick());
            AngerButtonCommand = new Command(async () => await AngerClick());
            DisgustButtonCommand = new Command(async () => await DisgustClick());
            ContemptButtonCommand = new Command(async () => await ContemptClick());
            SadnessButtonCommand = new Command(async () => await SadnessClick());
            FearButtonCommand = new Command(async () => await FearClick());
            SurpriseButtonCommand = new Command(async () => await SurpriseClick());
        }

        private async Task JoyClick()
        {
            await Navigation.PushAsync(new WhyIFeel("joy"));
        }

        private async Task AngerClick()
        {
            await Navigation.PushAsync(new WhyIFeel("anger"));
        }

        private async Task DisgustClick()
        {
            await Navigation.PushAsync(new WhyIFeel("disgust"));
        }

        private async Task ContemptClick()
        {
            await Navigation.PushAsync(new WhyIFeel("contempt"));
        }

        private async Task SadnessClick()
        {
            await Navigation.PushAsync(new WhyIFeel("sadness"));
        }

        private async Task FearClick()
        {
            await Navigation.PushAsync(new WhyIFeel("fear"));
        }

        private async Task SurpriseClick()
        {
            await Navigation.PushAsync(new WhyIFeel("surprise"));

        }

    }
}
