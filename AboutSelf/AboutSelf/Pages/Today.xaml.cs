using AboutSelf.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AboutSelf.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Today : ContentPage
    {
        public Today()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            SwipeScrollView.SwipeLeft += async (s, e) =>
                         await this.Navigation.PushAsync(new Month());

            SwipeScrollView.SwipeRight += async (s, e) =>
             await this.Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            TodayViewModel ViewModel = new TodayViewModel();
            ViewModel.Navigation = Navigation;
            this.BindingContext = ViewModel;

        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainPage();
            return base.OnBackButtonPressed();
        }



    }
}