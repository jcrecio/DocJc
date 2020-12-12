namespace DocJc.Model.ViewModel
{
    public class DiagnosticViewModel
    {
        public IssueViewModel Issue { get; set; }
        public SpecialisationDetailViewModel[] Specialisation { get; set; }
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
