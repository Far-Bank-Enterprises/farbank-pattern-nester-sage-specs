using Farbank.Pattern.Nester.SageSpecs.Models;
using Farbank.Pattern.Nester.SageSpecs.Server.Models;
using System.Reflection;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365
{
    public class D365Service : ID365Service
    {
        private readonly ID365Api _d365Api;

        public D365Service(ID365Api d365Api)
        {
            _d365Api = d365Api;
        }

        public async Task<List<FBEProductionOrderHeader>> GetProdOrdersByDateAsync(string pool, string date)
        {
            var result = new List<FBEProductionOrderHeader>();
            try
            {
                var Odata = await _d365Api.GetProdOrdersByDateAsync(pool, date);
                if (Odata.value != null)
                {
                    result = Odata.value.ToList();
                }
            }
            catch (Exception e)
            {
                //Log.Error(e, e.Message);
                //email
                throw;
            }
            return result;
        }
    }
}
