using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Endpoints;

public static class PatternNesterEndpoints
{
    public static void MapPatternNesterEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/ping", async () =>
        {
            // TODO: Save to database or process as needed
            // For now, just echo back the submission
            return Results.Ok(new { message = "Pattern Nester ping received" });
        })
        .WithName("PatternNesterPing");

        routes.MapGet("/exclusions", async ([FromServices] Services.PatternNesterService service) =>
        {
            var exclusions = await service.GetExclusions();
            return Results.Ok(exclusions);
        })
        .WithName("GetExclusions");
    }
}
