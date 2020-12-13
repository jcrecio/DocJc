namespace DocJc.ViewModels
{
    using System;
    using Contracts.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Commands;
    using Extensions;
    using Mapper;
    using Model.ViewModel;
    using Views;
    using Xamarin.Forms;

    public class DiagnosisSearchViewModel : BaseViewModel
    {
        private readonly IMemoryStore<DiagnosticViewModel> _diagnosticMemoryStore;
        public ObservableCollection<BaseEntityViewModel> FoundSymptoms { get; set; }
        public ObservableCollection<DiagnosticViewModel> FoundDiagnostics { get; set; }

        public bool AreSymptomsReady { get; set; } = true;
        public bool IsLoading => !AreSymptomsReady;

        public IAsyncCommand<IList<BaseEntityViewModel>> LoadItemsCommand { get; }
        public IAsyncCommand<IList<DiagnosticViewModel>> LoadDiagnosticsCommand { get; }

        public DiagnosisSearchViewModel(IHealthService healthService,
            DiagnosticMapper diagnosticMapper,
            BaseEntityMapper baseEntityMapper,
            IMemoryStore<DiagnosticViewModel> diagnosticMemoryStore)
        {
            _diagnosticMemoryStore = diagnosticMemoryStore;
            Title = "Diagnosis";
            FoundSymptoms = new ObservableCollection<BaseEntityViewModel>();
            FoundDiagnostics = new ObservableCollection<DiagnosticViewModel>();
            LoadItemsCommand = new GetSymptomsCommandAsync(healthService, baseEntityMapper);
            LoadDiagnosticsCommand = new GetDiagnosticsCommandAsync(healthService, diagnosticMapper, baseEntityMapper);
            SelectedSymptoms = new ObservableCollection<BaseEntityViewModel>();
            SelectedSymptoms.CollectionChanged += (sender, e) =>
            {
                if (SelectedSymptoms.Any())
                {
                    ExecuteLoadDiagnostics();
                }
                else
                {
                    FoundDiagnostics.Clear();
                }
            };
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
                        // ignored
                    }
                }

                AreSymptomsReady = true;
            }
        }

        private ObservableCollection<BaseEntityViewModel> _selectedSymptoms;
        public ObservableCollection<BaseEntityViewModel> SelectedSymptoms
        {
            get => _selectedSymptoms;
            set => SetProperty(ref _selectedSymptoms, value);
        }

        private ICommand _onSymptomTappedCommand;
        public ICommand SymptomTappedCommand
            => _onSymptomTappedCommand ?? (_onSymptomTappedCommand = new Command(OnSymptomTapped));

        public void OnSymptomTapped(object item)
        {
            var itemTapped = (BaseEntityViewModel)item;
            if (itemTapped == null)
            {
                return;
            }

            SelectSymptom(itemTapped);
        }

        private ICommand _removeSymptomCommand;
        public ICommand RemoveSymptomCommand
            => _removeSymptomCommand ?? (_removeSymptomCommand = new Command(OnRemoveSymptom));

        public void OnRemoveSymptom(object item)
        {
            var itemSelected = (BaseEntityViewModel)item;
            if (itemSelected == null)
            {
                return;
            }

            RemoveSymptom(itemSelected);
        }

        public void SelectSymptom(BaseEntityViewModel item)
        {
            if (!SelectedSymptoms.Contains(item))
            {
                AddSymptom(item);
            }
            else
            {
                RemoveSymptom(item);
            }
        }

        private void AddSymptom(BaseEntityViewModel item)
        {
            item.IsSelected = true;
            SelectedSymptoms.Add(item);
        }

        private void RemoveSymptom(BaseEntityViewModel item)
        {
            item.IsSelected = false;
            SelectedSymptoms.Remove(item);
        }

        private async void ExecuteLoadDiagnostics()
        {
            if (LoadDiagnosticsCommand.CanExecute(FoundSymptoms))
            {
                ClearDiagnostics();

                var diagnostics = await LoadDiagnosticsCommand.ExecuteAsync(SelectedSymptoms);
                foreach (var diagnostic in diagnostics)
                {
                    AddDiagnostic(diagnostic);
                }
            }
        }

        private void ClearDiagnostics()
        {
            _diagnosticMemoryStore.Clear();
            FoundDiagnostics.Clear();
        }

        private void AddDiagnostic(DiagnosticViewModel diagnosticViewModel)
        {
            _diagnosticMemoryStore.Add(diagnosticViewModel);
            FoundDiagnostics.Add(diagnosticViewModel);
        }

        private ICommand _goToDiagnosticListCommand;
        public ICommand GoToDiagnosticListCommand
            => _goToDiagnosticListCommand ?? (_goToDiagnosticListCommand = new Command(GoToDiagnosticList));
        public async void GoToDiagnosticList(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(DiagnosticList)}");
        }
    }
}