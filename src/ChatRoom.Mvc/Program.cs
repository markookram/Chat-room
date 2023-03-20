using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatRoom.Mvc.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Logging
builder.Host.UseSerilog((ctx, lc)
    => lc.ReadFrom.Configuration(ctx.Configuration));

var composer = new Composition(builder.Configuration);
composer.ConfigureServices(builder.Services);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(composer.ConfigureContainer);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ChatRoom}/{action=Index}/{id?}");

app.Run();

//Integration tests purpose only!
public partial class Program { }
