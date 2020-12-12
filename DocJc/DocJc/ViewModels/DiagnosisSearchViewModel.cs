namespace DocJc.ViewModels
{
    using System;
    using Contracts.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Commands;
    using Extensions;
    using Mapper;
    using Model.ViewModel;
    using Xamarin.Forms;

    public class DiagnosisSearchViewModel : BaseViewModel
    {
        public ObservableCollection<BaseEntityViewModel> FoundSymptoms { get; set; }
        public ObservableCollection<DiagnosticViewModel> FoundDiagnostics { get; set; }

        public bool AreSymptomsReady { get; set; } = true;
        public bool IsLoading => !AreSymptomsReady;

        public IAsyncCommand<IList<BaseEntityViewModel>> LoadItemsCommand { get; }
        public IAsyncCommand<IList<DiagnosticViewModel>> LoadDiagnosticsCommand { get; }

        public DiagnosisSearchViewModel(IHealthService healthService, 
            DiagnosticMapper diagnosticMapper, 
            BaseEntityMapper baseEntityMapper)
        {
            Title = "Diagnosis";
            FoundSymptoms = new ObservableCollection<BaseEntityViewModel>();
            FoundDiagnostics = new ObservableCollection<DiagnosticViewModel>();
            LoadItemsCommand = new GetSymptomsCommandAsync(healthService, baseEntityMapper);
            LoadDiagnosticsCommand = new GetDiagnosticsCommandAsync(healthService, diagnosticMapper, baseEntityMapper);
            SelectedSymptoms = new ObservableCollection<BaseEntityViewModel>();
        }

        private Image _tick;
        public Image Tick
        {
            get
            {
                if (_tick == null)
                {
                    _tick = new Image { Source = ImageSource.FromFile("Assets/Images/tick.png"), Margin = new Thickness(0, -5, 0, 10), HeightRequest = 20, WidthRequest = 20 };
                }
                return _tick;
            }
        }

        private string _symptomSearchCriteria;
        public string SymptomSearchCriteria
        {
            get => _symptomSearchCriteria;
            set
            {
                SetProperty(ref _symptomSearchCriteria, value);
                _ = ExecuteLoadItems();
            }
        }

        private async Task ExecuteLoadItems()
        {
            if (LoadItemsCommand.CanExecute(_symptomSearchCriteria))
            {
                AreSymptomsReady = false;
                FoundSymptoms.Clear();

                var symptoms = await LoadItemsCommand.ExecuteAsync(_symptomSearchCriteria);
                foreach (var symptom in symptoms)
                {
                    try
                    {
                        FoundSymptoms.Add(symptom);

                    }
                    catch (Exception)
                    {
                    }
                }

                AreSymptomsReady = true;
            }
        }

        private IList<BaseEntityViewModel> _selectedSymptoms;
        public IList<BaseEntityViewModel> SelectedSymptoms
        {
            get => _selectedSymptoms;
            set => SetProperty(ref _selectedSymptoms, value);
        }


        private ICommand _onSymptomTappedCommand;

        public ICommand SymptomTappedCommand 
            => _onSymptomTappedCommand ?? (_onSymptomTappedCommand = new Command(OnSymptomTapped));


        public async void OnSymptomTapped(object item)
        {
            var itemSelected = (BaseEntityViewModel)item;
            if (itemSelected == null)
            {
                return;
            }

            var isAlreadySelected = SelectedSymptoms.Contains(itemSelected);
            itemSelected.IsSelected = !isAlreadySelected;

            if (itemSelected.IsSelected)
            {
                SelectedSymptoms.Add(itemSelected);
            }
            else
            {
                SelectedSymptoms.Remove(itemSelected);
            }

            if (SelectedSymptoms.Any())
            {
                await ExecuteLoadDiagnostics();
            }
            else
            {
                FoundDiagnostics.Clear();
            }
        }

        private async Task ExecuteLoadDiagnostics()
        {
            if (LoadDiagnosticsCommand.CanExecute(FoundSymptoms))
            {
                FoundDiagnostics.Clear();

                var diagnostics = await LoadDiagnosticsCommand.ExecuteAsync(SelectedSymptoms);
                foreach (var diagnostic in diagnostics)
                {
                    FoundDiagnostics.Add(diagnostic);
                }
            }
        }
    }
}