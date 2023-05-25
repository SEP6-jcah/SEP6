using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sep6Client.Data.Movies;
using Sep6Client.Data.TMDB;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

var firebaseApiKey = builder.Configuration.GetSection("Firebase:Api.Key").Value;
var firebaseAuthDomain = builder.Configuration.GetSection("Firebase:Domain").Value;

var firebaseConfig = new FirebaseAuthConfig
{
    ApiKey = firebaseApiKey,
    AuthDomain = firebaseAuthDomain,
    Providers = new FirebaseAuthProvider[]
    {
        new EmailProvider()
    }
};


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddSingleton<FirebaseAuthClient>(new FirebaseAuthClient(firebaseConfig));
builder.Services.AddBlazoredToast();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
