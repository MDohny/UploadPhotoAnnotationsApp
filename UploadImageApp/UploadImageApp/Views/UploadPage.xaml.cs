using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadImageApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UploadImageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadPage : ContentPage
    {
        public UploadPage(INavigation navigation)
        {
            InitializeComponent();
            BindingContext = new UploadPageViewModel(navigation);
        }
    }
}