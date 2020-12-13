namespace DocJc.Model.ViewModel
{
    using System.Collections.ObjectModel;
    using Models;

    public class DiagnosticViewModel: IBaseEntity
    {
        public string ID => Issue.ID;
        public string Name => Issue.Name;
        public IssueViewModel Issue { get; set; }
        public ObservableCollection<SpecialisationDetailViewModel> Specialisation { get; set; }
    }

    public class IssueViewModel : SelectableItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public float Accuracy { get; set; }
        public string Icd { get; set; }
        public string IcdName { get; set; }
        public string ProfName { get; set; }
        public int Ranking { get; set; }
    }

    public class SpecialisationDetailViewModel : SelectableItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int SpecialistID { get; set; }
    }
}
