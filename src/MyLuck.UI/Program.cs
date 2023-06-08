using Auth0.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using MyLuck.UI.Extensions;

using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
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

builder.Services.ConfigureSameSiteNoneCookies();

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