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
    }
}