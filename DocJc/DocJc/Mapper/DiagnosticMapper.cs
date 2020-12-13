namespace DocJc.Mapper
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Model.Models;
    using Model.ViewModel;

    public class DiagnosticMapper
    {
        public DiagnosticViewModel FromEntityToViewModel(Diagnostic diagnostic)
        {
            return new DiagnosticViewModel
            {
                Issue = new IssueViewModel
                {
                    ID = diagnostic.Issue.ID,
                    IsSelected = false,
                    Name = diagnostic.Issue.Name,
                    Accuracy = diagnostic.Issue.Accuracy,
                    Icd = diagnostic.Issue.Icd,
                    IcdName = diagnostic.Issue.IcdName,
                    ProfName = diagnostic.Issue.ProfName,
                    Ranking = diagnostic.Issue.Ranking
                }, 
                Specialisation = new ObservableCollection<SpecialisationDetailViewModel>(
                    diagnostic.Specialisation.Select(d => new SpecialisationDetailViewModel
                {
                    ID = d.ID,
                    Name = d.Name,
                    IsSelected = false,
                    SpecialistID = d.SpecialistID
                }))
            };
        }
    }
}
