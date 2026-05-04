namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365
{
    public static class D365ApiEndpoints
    {
        public static RouteGroupBuilder MapD365SpecEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (ID365Service service) => await service.GetAllAsync());
            group.MapGet("/{id:guid}", async (ID365Service service, Guid id) =>
                await service.GetByIdAsync(id) is Models.SageSpecs spec ? Results.Ok(spec) : Results.NotFound());
            group.MapPost("/", async (ID365Service service, Models.SageSpecs spec) =>
                Results.Created($"/api/d365/specs/{spec.Id}", await service.CreateAsync(spec)));
            group.MapPut("/{id:guid}", async (ID365Service service, Guid id, Models.SageSpecs spec) =>
                await service.UpdateAsync(id, spec) is Models.SageSpecs updated ? Results.Ok(updated) : Results.NotFound());
            group.MapDelete("/{id:guid}", async (ID365Service service, Guid id) =>
                await service.DeleteAsync(id) ? Results.NoContent() : Results.NotFound());

            return group;
        }
    }
}
