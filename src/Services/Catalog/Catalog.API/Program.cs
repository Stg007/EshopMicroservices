using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(ops =>
{
    ops.Connection(builder.Configuration.GetConnectionString("Database")!);
    // Register your document mappings here
    //ops.Schema.For<Catalog.API.Models.Product>();

}).UseLightweightSessions();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
