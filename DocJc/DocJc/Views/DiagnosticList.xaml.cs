namespace DocJc.Views
{
    using CommonServiceLocator;
    using ViewModels;
    using Xamarin.Forms;

    public partial class DiagnosticList : ContentPage
    {
        public DiagnosticList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // TODO: Do this without this page behind code, use MVVM behaviors
            //ServiceLocator.Current.GetInstance<DiagnosticListViewModel>().OnAppearing();
        }
    }
}