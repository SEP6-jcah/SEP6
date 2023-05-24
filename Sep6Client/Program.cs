using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sep6Client.Data.Movies;
using Sep6Client.Data.TMDB;
﻿using Firebase.Auth;

var firebaseConfig = new FirebaseAuthConfig
{
    ApiKey = "AIzaSyC9n3veZPuF3v2qS5QVKk9l681jzFVcoOA",
    AuthDomain = "sep6-jcah.firebaseapp.com",
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddSingleton<FirebaseAuthClient>(new FirebaseAuthClient(firebaseConfig));
//builder.Services.AddSingleton<FirebaseAuth>(new FirebaseAuthProvider(builder.Configuration.GetSection("FIREBASE_API_KEY")));


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
