using Farbank.Pattern.Nester.SageSpecs.Server.Models;
using System.Reflection;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365;

public interface ID365Service
{
    Task<List<FBEProductionOrderHeader>> GetProdOrdersByDateAsync(string pool, string date);
}