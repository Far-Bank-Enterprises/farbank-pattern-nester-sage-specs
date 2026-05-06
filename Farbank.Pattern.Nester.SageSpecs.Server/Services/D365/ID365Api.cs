
using Farbank.Pattern.Nester.SageSpecs.Server.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365
{
    [Headers("Authorization: Bearer")]
    public interface ID365Api
    {


        //[Post("/api/services/FBESageSpecsServiceGroup/FBESageSpecsService/CreateBlankProductionV3")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<string> CreateBlankWorkOrder([Body(BodySerializationMethod.Serialized)] CreateBlankWorkOrderData workOrderData);
        /// <summary>
        /// OData Calls
        /// </summary>
        /// <returns></returns>
        [Get("/data/PlannedOrders?$filter=ProductGroupId eq '101' and RequirementPlanId eq 'Dyn V2' and RequirementWarehouseId eq 'Rods'")]
        Task<ODataResult<PlannedOrder>> GetBlankDynamicPlannedOrders();
        [Get("/data/PlannedOrders?$filter=ProductGroupId eq '101' and RequirementPlanId eq 'Static V2' and RequirementWarehouseId eq 'Rods'")]
        Task<ODataResult<PlannedOrder>> GetBlankStaticPlannedOrders();
        [Get("/data/PlannedOrders?$filter=ProductGroupId eq '101' and RequirementPlanId eq 'Dyn V2' and RequirementWarehouseId eq 'Repairs'")]
        Task<ODataResult<PlannedOrder>> GetRepairDynamicPlannedOrders();
        [Get("/data/PlannedOrders?$filter=ProductGroupId eq '101' and RequirementPlanId eq 'Static V2' and RequirementWarehouseId eq 'Repairs'")]
        Task<ODataResult<PlannedOrder>> GetRepairStaticPlannedOrders();
        [Get("/data/PlannedOrders?$filter=RefId eq '{id}'")]
        Task<ODataResult<PlannedOrder>> GetPlannedOrder(string id);
        //need to full quality the prod status type for the string
        //[Get("/data/FBEProductionOrderHeaders?$filter=ProdPoolId eq 'Blanks'")]
        [Get("/data/FBEProductionOrderHeaders?$filter=ProdStatus eq Microsoft.Dynamics.DataEntities.ProdStatus'StartedUp'")]
        Task<ODataResult<FBEProductionOrderHeader>> GetStartedWorkOrders();
        [Get("/data/FBEProductionOrderHeaders")]
        Task<ODataResult<FBEProductionOrderHeader>> GetWorkOrders();
        [Get("/data/FBEProductionOrderHeaders?$filter=ProdPoolId eq '{pool}' and DlvDate eq cast({dateTime},Edm.DateTimeOffset)")]
        Task<ODataResult<FBEProductionOrderHeader>> GetProdOrdersByDateAsync(string pool, string dateTime);
        [Get("/data/FBEProductionOrderHeaders?$filter=ProdStatus eq Microsoft.Dynamics.DataEntities.ProdStatus'CostEstimated' and ProdPoolId eq 'WARR'")]
        Task<ODataResult<FBEProductionOrderHeader>> GetWarrWorkOrders();
    }
}