﻿using System.Data.Entity;
using System.Globalization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using eShopLegacyWebForms.Models;
using eShopLegacyWebForms.Models.Infrastructure;
using eShopLegacyWebForms.Modules;
using eShopTelerikBlazorServer.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

using Telerik.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTelerikBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// register a custom localizer for the Telerik components, after registering the Telerik services
builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(ResxLocalizer));


var mockData = builder.Configuration.GetValue<bool>("UseMockData",true);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new ApplicationModule(mockData));
});

#region Localization

builder.Services.AddControllers();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // define the list of cultures your app will support
    var supportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("de-DE"),
        new CultureInfo("es-ES"),
        new CultureInfo("bg-BG")
    };

    // set the default culture
    options.DefaultRequestCulture = new RequestCulture("en-US");

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// the custom localizer service is registered later, after the Telerik services

#endregion
var app = builder.Build();


if (!mockData)
{
    Database.SetInitializer<CatalogDBContext>(app.Services.GetRequiredService<CatalogDBInitializer>());
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



#region Localization

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
#endregion

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
