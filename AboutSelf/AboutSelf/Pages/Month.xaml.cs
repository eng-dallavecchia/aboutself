using AboutSelf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AboutSelf.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Month : ContentPage
    {
        public Month()
        {
            InitializeComponent();

            SwipeScrollView.SwipeLeft += async (s, e) =>
             await this.Navigation.PushAsync(new Year());

            SwipeScrollView.SwipeRight += async (s, e) =>
             await this.Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            MonthViewModel ViewModel = new MonthViewModel();
            ViewModel.Navigation = Navigation;
            this.BindingContext = ViewModel;
        }

        private async void SwipedRight(object sender, SwipedEventArgs e)
        {
            await this.Navigation.PopAsync();
        }

    }
}