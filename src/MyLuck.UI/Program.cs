using Auth0.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

using MyLuck.UI.Extensions;
using MyLuck.UI.Setup;

using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.SetupDependencyInjection(builder.Configuration);
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
// Cookie configuration for HTTP to support cookies with SameSite=None
builder.Services.ConfigureSameSiteNoneCookies();
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"]!;
    options.ClientId = builder.Configuration["Auth0:ClientId"]!;
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.OpenIdConnectEvents = new OpenIdConnectEvents
    {
        OnTokenValidated = context =>
        {
            var claimsIdentity = context?.Principal?.Identity as ClaimsIdentity;
            var roleClaims = context?.Principal?.FindAll(c => c.Type == "https://schemas.dev.auth0.com/roles");
            if (roleClaims != null && roleClaims.Any())
            {
                claimsIdentity?.AddClaims(roleClaims);
            }

            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();