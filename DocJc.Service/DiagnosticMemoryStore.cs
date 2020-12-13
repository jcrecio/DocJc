namespace DocJc.Service
{
    using Contracts.Services;
    using Model.ViewModel;
    using System.Collections.Generic;

    public class DiagnosticMemoryStore: IMemoryStore<DiagnosticViewModel>
    {
        private readonly List<DiagnosticViewModel> _diagnostics = new List<DiagnosticViewModel>();

        public IList<DiagnosticViewModel> GetAll()
        {
            return _diagnostics;
        }

        public void Add(DiagnosticViewModel item)
        {
            _diagnostics.Add(item);
        }

        public void AddRange(IList<DiagnosticViewModel> items)
        {
            _diagnostics.AddRange(items);
        }

        public void Remove(DiagnosticViewModel item)
        {
            _diagnostics.Remove(item);
        }

        public void Clear()
        {
            _diagnostics.Clear();
        }
    }
}
