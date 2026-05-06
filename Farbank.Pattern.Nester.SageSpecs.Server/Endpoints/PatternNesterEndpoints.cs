using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Endpoints;

public static class PatternNesterEndpoints
{
    public static void MapPatternNesterEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/ping", async () =>
        {
            return Results.Ok(new { message = "Pattern Nester ping received" });
        })
        .WithName("PatternNesterPing");


        routes.MapGet("/exclusions", async ([FromServices] Services.PatternNesterService service) =>
        {
            try
            {
                var exclusions = await service.GetExclusions();
                return Results.Ok(exclusions);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return Results.Problem("An error occurred while retrieving exclusions.");
            }
        })
        .WithName("GetExclusions");


        routes.MapGet("/production-orders", async ([FromServices] Services.PatternNesterService service, [FromQuery][Required] DateTime dateTime) =>
        {
            try
            {
                var productionOrders = await service.GetProductionOrdersByDateTime(dateTime);

                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(productionOrders));
                return Results.Json(productionOrders);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return Results.Json(new { error = "An error occurred while retrieving production orders." }, statusCode: 500);
            }
        })
        .WithName("GetProductionOrdersByDateTime");
    }
}
