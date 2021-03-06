﻿namespace DocJc
{
    using DocJc.Contracts.Services;
    using DocJc.Service;
    using DocJc.Views;
    using System;
    using Xamarin.Forms;

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            InitializeRouting();
        }

        private void InitializeRouting()
        {
            Routing.RegisterRoute(nameof(DiagnosticList), typeof(DiagnosticList));
            Routing.RegisterRoute(nameof(DiagnosisSearch), typeof(DiagnosisSearch));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnLogoutItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
