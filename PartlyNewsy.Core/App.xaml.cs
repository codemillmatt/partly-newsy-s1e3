using System;
using PartlyNewsy.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartlyNewsy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShellPage();

            Routing.RegisterRoute("articledetail", typeof(ArticleDetailPage));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
