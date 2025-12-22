using BuildingBlocks.Behaviors;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehabior<,>));
});

builder.Services.AddValidatorsFromAssemblies(new[]
{
    assembly
});

builder.Services.AddMarten(ops =>
{
    ops.Connection(builder.Configuration.GetConnectionString("Database")!);
    // Register your document mappings here
    ops.Schema.For<Catalog.API.Models.Product>();

}).UseLightweightSessions();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
