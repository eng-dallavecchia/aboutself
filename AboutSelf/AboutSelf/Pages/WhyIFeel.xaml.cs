using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AboutSelf.ViewModels;

namespace AboutSelf.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhyIFeel : ContentPage
    {
        public string Feeling { get; set; }
        public string Reason { get; set; }

        public WhyIFeel()
        {
            InitializeComponent();
        }

        public WhyIFeel(string feeling)
        {
            InitializeComponent();
            WhyIFeelViewModel ViewModel = new WhyIFeelViewModel(feeling);
            ViewModel.Navigation = this.Navigation;
            this.BindingContext = ViewModel;
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.Scale = 1.2;
        }

        private void Button_Released(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.Scale = 1;

        }
    }
}