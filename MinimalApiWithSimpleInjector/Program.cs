using MinimalApiWithSimpleInjector;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);

var container = new Container();
container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();

builder.Services.AddSimpleInjector(container, options =>
{
    options.AddAspNetCore();
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

((IApplicationBuilder)app).UseSimpleInjector(container);

var svcAssembly = typeof(ApplicationService.Product.ProductGetHandler).Assembly;
container.Register(typeof(ApplicationService.ICommandHandler<,>), new[] { svcAssembly });
container.RegisterDecorator(typeof(ApplicationService.ICommandHandler<,>), typeof(ApplicationService.LogProductGetHandler<,>));

var apiAssembly = typeof(EndpointRegistration).Assembly;
var types = apiAssembly.GetTypes().Where(t => t.IsClass
    && t.Namespace != null && t.Namespace.StartsWith("MinimalApiWithSimpleInjector.Endpoint"));
foreach (var type in types)
{
    container.Register(type, type, Lifestyle.Transient);
}

container.Verify();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

EndpointRegistration.RegisterEndpoints(app, container);

await app.RunAsync();
