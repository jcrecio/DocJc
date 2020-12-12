namespace DocJc.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Services;
    using Mapper;
    using Extensions;
    using Model.ViewModel;

    public class GetSymptomsCommandAsync: IAsyncCommand<IList<BaseEntityViewModel>>
    {
        private readonly IHealthService _healthService;
        private readonly BaseEntityMapper _basicEntityMapper;

        public GetSymptomsCommandAsync(IHealthService healthService, BaseEntityMapper basicEntityMapper)
        {
            _healthService = healthService;
            _basicEntityMapper = basicEntityMapper;
        }

        public TimeSpan LastCalledLoadItems;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object symptomSearchCriteriaObject)
        {
            var symptomSearchCriteria = symptomSearchCriteriaObject as string;
            if (symptomSearchCriteria == null)
            {
                return false;
            }

            return symptomSearchCriteria.Length >= 3 
                   && (DateTime.Now - LastCalledLoadItems).Millisecond > 1;
        }

        public void Execute(object symptomSearchCriteriaObject)
        {
        }

        public async Task<IList<BaseEntityViewModel>> ExecuteAsync(object symptomSearchCriteriaObject)
        {
            var symptomSearchCriteria = symptomSearchCriteriaObject as string;
            if (symptomSearchCriteria == null)
            {
                return new List<BaseEntityViewModel>();
            }

            try
            {
                var response = await _healthService.GetSymptoms(symptomSearchCriteria);
                LastCalledLoadItems = DateTime.Now.TimeOfDay;

                return response?.Select(_basicEntityMapper.FromEntityToViewModel).ToList();

            }
            catch (Exception)
            {
                // ignore
            }

            return new List<BaseEntityViewModel>();
        }
    }
}