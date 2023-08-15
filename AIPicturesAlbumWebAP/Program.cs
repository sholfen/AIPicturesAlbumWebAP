using AIPicturesAlbumWebAP.Data;
using BlazorBootstrap;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//setup DI
builder.Services.AddTransient(typeof(IPictureRepository), typeof(PictureRepository));
builder.Services.AddTransient(typeof(IPictureAppService), typeof(PictureAppService));

builder.Services.AddBlazorBootstrap();

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
