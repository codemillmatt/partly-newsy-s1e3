using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;

namespace PartlyNewsy.Core
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await GetData();
        }

        async Task GetData()
        {
            var svc = new NewsService();

            var articles = await svc.GetTopNews();

            newsList.ItemsSource = articles;
        }
    }
}
