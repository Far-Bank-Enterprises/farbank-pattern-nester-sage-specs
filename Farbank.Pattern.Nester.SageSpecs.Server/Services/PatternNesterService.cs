using System;
using Farbank.Pattern.Nester.SageSpecs.Server.Models;
using Farbank.Pattern.Nester.SageSpecs.Models;
using Farbank.Pattern.Nester.SageSpecs.Server.Services.D365;
using Microsoft.EntityFrameworkCore;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services
{

    public class PatternNesterService
    {
        private readonly SpecsDbContext _dbContext;
        private readonly ID365Service _d365Service;
        public PatternNesterService(SpecsDbContext dbContext, ID365Service d365Service)
        {
            _dbContext = dbContext;
            _d365Service = d365Service;
        }

        public async Task<List<string>> GetExclusions()
        {
            var exclusions = await _dbContext.Exclusions.Select(e => e.Sku).ToListAsync();

            return exclusions;
        }

        public async Task<List<FBEProductionOrderHeader>> GetProductionOrdersByDateTime(DateTime dateTime)
        {
            try
            {
                var date = dateTime.ToString("yyyy-MM-ddT12:00:00Z");
                var pool = "Blanks";
                var prodOrders = await _d365Service.GetProdOrdersByDateAsync(pool, date);

                return prodOrders;

            }
            catch (Exception ex)
            {
                // Log the exception as needed
                Console.WriteLine($"An error occurred while retrieving production orders: {ex.Message}");
                throw;
            }
        }
    }
}