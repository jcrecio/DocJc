namespace DocJc.ViewModels
{
    using Model.ViewModel;

    public class BaseViewModel: ObservableViewModel
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
