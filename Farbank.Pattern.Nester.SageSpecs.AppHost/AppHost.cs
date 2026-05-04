var builder = DistributedApplication.CreateBuilder(args);

var server = builder.AddProject<Projects.Farbank_Pattern_Nester_SageSpecs_Server>("server")
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(server)
    .WaitFor(server)
    .WithHttpEndpoint(port: 5000, name: "https");

server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.Build().Run();
