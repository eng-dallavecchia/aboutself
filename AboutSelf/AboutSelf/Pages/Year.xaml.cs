using AboutSelf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AboutSelf.Components;

namespace AboutSelf.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Year : ContentPage
    {
        public Year()
        {
            InitializeComponent();

            SwipeScrollView.SwipeRight += async (s, e) =>
                await this.Navigation.PopAsync();

        }

        protected override void OnAppearing()
        {
            YearViewModel ViewModel = new YearViewModel();
            ViewModel.Navigation = this.Navigation;
            this.BindingContext = ViewModel;
        }
    }
}