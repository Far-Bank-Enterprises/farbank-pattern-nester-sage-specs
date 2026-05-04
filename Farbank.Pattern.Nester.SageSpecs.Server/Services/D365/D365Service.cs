using System.Reflection;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365
{
    public class D365Service : ID365Service
    {
        private readonly List<Models.SageSpecs> _specs = new();

        public Task<IEnumerable<Models.SageSpecs>> GetAllAsync() => Task.FromResult(_specs.AsEnumerable());
        public Task<Models.SageSpecs?> GetByIdAsync(Guid id) => Task.FromResult(_specs.FirstOrDefault(s => s.Id == id));
        public Task<Models.SageSpecs> CreateAsync(Models.SageSpecs spec)
        {
            _specs.Add(spec);
            return Task.FromResult(spec);
        }
        public Task<Models.SageSpecs?> UpdateAsync(Guid id, Models.SageSpecs spec)
        {
            var existing = _specs.FirstOrDefault(s => s.Id == id);
            if (existing == null) return Task.FromResult<Models.SageSpecs?>(null);
            _specs.Remove(existing);
            _specs.Add(spec);
            return Task.FromResult<Models.SageSpecs?>(spec);
        }
        public Task<bool> DeleteAsync(Guid id)
        {
            var existing = _specs.FirstOrDefault(s => s.Id == id);
            if (existing == null) return Task.FromResult(false);
            _specs.Remove(existing);
            return Task.FromResult(true);
        }
    }
}
