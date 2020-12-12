namespace DocJc.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Services;
    using Extensions;
    using Mapper;
    using Model.Models;
    using Model.ViewModel;

    public class GetDiagnosticsCommandAsync: IAsyncCommand<IList<DiagnosticViewModel>>
    {
        private readonly IHealthService _healthService;
        private readonly DiagnosticMapper _diagnosticMapper;
        private readonly BaseEntityMapper _baseEntityMapper;

        public GetDiagnosticsCommandAsync(IHealthService healthService, 
            DiagnosticMapper diagnosticMapper, 
            BaseEntityMapper baseEntityMapper)
        {
            _healthService = healthService;
            _diagnosticMapper = diagnosticMapper;
            _baseEntityMapper = baseEntityMapper;
        }

        public DateTime LastCalledLoadItems = default;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object diagnosticSearchCriteriaObject)
        {
            var diagnosticSearchCriteria = diagnosticSearchCriteriaObject as IList<BaseEntityViewModel>;
            if (diagnosticSearchCriteria == null || !diagnosticSearchCriteria.Any())
            {
                return false;
            }

            var a = (DateTime.Now - LastCalledLoadItems).Milliseconds;
            return LastCalledLoadItems == default || a > 200;
        }

        public void Execute(object symptomSearchCriteriaObject)
        {
        }

        public async Task<IList<DiagnosticViewModel>> ExecuteAsync(object symptomSearchCriteriaObject)
        {
            var symptomSearchCriteriaViewModel = symptomSearchCriteriaObject as IList<BaseEntityViewModel>;
            if (symptomSearchCriteriaViewModel == null)
            {
                return new List<DiagnosticViewModel>();
            }

            try
            {
                var symptomSearchCriteria =
                    symptomSearchCriteriaViewModel.Select(_baseEntityMapper.FromViewModelToEntity);

                var response = await _healthService.GetDiagnostics(symptomSearchCriteria);

                LastCalledLoadItems = DateTime.Now;

                return response?.Select(_diagnosticMapper.FromEntityToViewModel).ToList();

            }
            catch (Exception)
            {
                // ignore
            }

            return new List<DiagnosticViewModel>();
        }
    }
}