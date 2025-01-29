using AIPicturesAlbumWebAP.Data;
using AIPicturesAlbumWebAP.Data.Model;
using BlazorBootstrap;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAuthentication("Cookies").AddCookie();
builder.Services.AddHttpContextAccessor();

//setup DI
builder.Services.AddTransient(typeof(IPictureRepository), typeof(PictureRepository));
builder.Services.AddTransient(typeof(IPictureAppService), typeof(PictureAppService));
builder.Services.AddTransient(typeof(IUserAppService), typeof(UserAppService));

builder.Services.AddBlazorBootstrap();
builder.Services.AddMvc();
//builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["Azure:SignalR:ConnectionString"]!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//load config file
var config = app.Configuration;
string containerName = config.GetValue<string>("AzureBlobContainerName");
PictureRepository.ContainerName = containerName;
PictureRepository.BlobConnectionString = config.GetConnectionString("AzureBlobContainer");
PictureRepository.TableConnectionString = config.GetConnectionString("AzureTable");
PictureRepository.CDNDomain= config.GetValue<string>("PicDomain");
UserData.UserName = config.GetSection("AdminUser").GetValue<string>("User");
UserData.UserPassword = config.GetSection("AdminUser").GetValue<string>("Password");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
