namespace DocJc.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Contracts.Services;
    using Model.ViewModel;
    using Xamarin.Forms;

    public class DiagnosticListViewModel : BaseViewModel
    {
        private readonly IMemoryStore<DiagnosticViewModel> _diagnosticMemoryStore;

        private ObservableCollection<DiagnosticViewModel> _foundDiagnostics;
        public ObservableCollection<DiagnosticViewModel> FoundDiagnostics
        {
            get => _foundDiagnostics;
            set => SetProperty(ref _foundDiagnostics, value);
        }

        public DiagnosticListViewModel(IMemoryStore<DiagnosticViewModel> diagnosticMemoryStore)
        {
            _diagnosticMemoryStore = diagnosticMemoryStore;
            _foundDiagnostics = new ObservableCollection<DiagnosticViewModel>();
            OnAppearing();
        }

        private ICommand _onAppearingCommand;
        public ICommand OnAppearingCommand
            => _onAppearingCommand ?? (_onAppearingCommand = new Command(OnAppearing));

        public void OnAppearing()
        {
            FoundDiagnostics.Clear();
            foreach (var diagnostic in _diagnosticMemoryStore.GetAll())
            {
                FoundDiagnostics.Add(diagnostic);
            }
        }
    }
}
