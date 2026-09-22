using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Options;
using S3FileApi.helper;
using S3FileApi.Repository;
using S3FileApi.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// S3 Settings
builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("S3Settings"));

// AWS S3 Client
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;
    var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
    return new AmazonS3Client(credentials,RegionEndpoint.GetBySystemName(settings.RegionName));
});

// Repository Registration
builder.Services.AddScoped<IS3AamazoneGET,S3AamazoneGET>();
builder.Services.AddScoped<IS3AamazonePost,S3AamazonePost>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();