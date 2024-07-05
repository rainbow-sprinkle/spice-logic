using System.Text.Json.Serialization;
using BusinessDomain.BusinessLogic;
using BusinessDomain.Contracts;
using DataAccess;
using DataAccess.Entities;
using DecisionAnalysis.Web.Client.Components;
using DecisionAnalysis.Web.Client.Components.Service;
using DecisionAnalysis.Web.Client.UiUtilities;
using DecisionAnalysis.Web.Components;
using DecisionAnalysis.Web.Components.Account;
using FrameworkUtilities.ConfigNames;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton<AhpStateContainerService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddAuthorization(ops =>
{
    ops.AddPolicy(nameof(UserPolicies.SpiceAdminPolicy), policy =>
    {
        policy.RequireUserName(builder.Configuration[BusinessSettingNames.BusinessEmailAddress] ?? "");
    });
});

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString(AppSettingNames.SqlConnectionString);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString,
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddDbContextFactory<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString,
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// I added this line to support API Controller
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
    o.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
}); // This line makes the JSON output as Pascal Case, otherwise, the default is camel case.


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IBlazorJsFacade, BlazorJsFacade>();
builder.Services.AddScoped<ISubscriptionPlanManager, SubscriptionPlanManager>();
builder.Services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
builder.Services.AddScoped<IGlobalPreferencesManager, GlobalPreferencesManager>(); 
builder.Services.AddScoped<IOrganizationManager, OrganizationManager>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IPaymentProcessor, StripePaymentProcessor.StripePaymentProcessor>();
builder.Services.AddScoped<ISiteUtils, SiteUtils>();

WebApplication app = builder.Build();

string syncFusionLicenseKey = builder.Configuration[AppSettingNames.SyncFusionBlazorLicenseKeyName] ?? "";
SyncfusionLicenseProvider.RegisterLicense(syncFusionLicenseKey);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler(Error.PageUrl, createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NotFound).Assembly);

app.MapControllers(); // I added this line to support API Controller

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
