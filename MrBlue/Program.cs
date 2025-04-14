using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies; // Import for cookie authentication
using MudBlazor.Services;
using MrBlue.Components;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<Random>();

//// Configure Authentication
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login"; // Set your login path
//        options.LogoutPath = "/Account/Logout"; // Set your logout path
//    });

//// Add Authorization services
//builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Handle exceptions
    app.UseHsts(); // Enforce HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static files

app.UseRouting(); // Add routing middleware

//app.UseAuthentication(); // Add authentication middleware
//app.UseAuthorization(); // Add authorization middleware
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();