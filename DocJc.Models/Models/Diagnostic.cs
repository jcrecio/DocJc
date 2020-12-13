namespace DocJc.Model.Models
{
    using DocJc.Model.Models;

    public class Diagnostic: IBaseEntity
    {
        public string ID => Issue.ID;
        public string Name => Issue.Name;
        public Issue Issue { get; set; }
        public SpecialisationDetail[] Specialisation { get; set; }
    }

    public class Issue: BaseEntity
    {
        public float Accuracy { get; set; }
        public string Icd { get; set; }
        public string IcdName { get; set; }
        public string ProfName { get; set; }
        public int Ranking { get; set; }
    }

    public class SpecialisationDetail: BaseEntity
    {
        public int SpecialistID { get; set; }
    }
}
