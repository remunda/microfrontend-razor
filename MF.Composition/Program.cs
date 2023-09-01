using MF.Composition;
using MF.Composition.Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.AddFragments();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseMiddleware<RoutingMiddleware>();


app.UseRouting();

app.UseAuthorization();

app.MapDynamicPageRoute<DynamicRouteTransformer>("/{**slug}");

app.MapRazorPages();

app.Run();
