using DecisionAnalysis.Web.Client;
using DecisionAnalysis.Web.Client.Components.Service;
using DecisionAnalysis.Web.Client.UiUtilities;
using FrameworkUtilities.ConfigNames;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

string syncFusionLicenseKey = builder.Configuration[AppSettingNames.SyncFusionBlazorLicenseKeyName] ?? "";
SyncfusionLicenseProvider.RegisterLicense(syncFusionLicenseKey);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<HttpClient>(); 
builder.Services.AddScoped<IBlazorJsFacade, BlazorJsFacade>();
builder.Services.AddScoped<ISiteUtils, SiteUtils>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton<AhpStateContainerService>();

await builder.Build().RunAsync();
