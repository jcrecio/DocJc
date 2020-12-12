namespace DocJc.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model.Models;

    public interface IHealthService
    {
        Task<IList<BaseEntity>> GetBodyLocations();
        Task<IList<BaseEntity>> GetIssues();
        Task<IList<BaseEntity>> GetSymptoms(string keyword = null);
        Task<IList<Diagnostic>> GetDiagnostics(IEnumerable<BaseEntity> symptoms);
    }
}
