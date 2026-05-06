using System;
using Farbank.Pattern.Nester.SageSpecs.Models;
using Microsoft.EntityFrameworkCore;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services
{

    public class PatternNesterService
    {
        private readonly SpecsDbContext _dbContext;
        public PatternNesterService(SpecsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetExclusions()
        {
            var exclusions = await _dbContext.Exclusions.Select(e => e.Sku).ToListAsync();

            return exclusions;
        }

        public async Task<List<(string, string)>> GetProductionOrdersByDateTime(DateTime dateTime)
        {
            // First get the data from D365, filtering as we have the date asked.
            // Then filter to workorders with prod pool title
            // Serve
        }
    }
}