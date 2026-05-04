using System.Reflection;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365;

public interface ID365Service
{
    Task<IEnumerable<Models.SageSpecs>> GetAllAsync();
    Task<Models.SageSpecs?> GetByIdAsync(Guid id);
    Task<Models.SageSpecs> CreateAsync(Models.SageSpecs spec);
    Task<Models.SageSpecs?> UpdateAsync(Guid id, Models.SageSpecs spec);
    Task<bool> DeleteAsync(Guid id);
}