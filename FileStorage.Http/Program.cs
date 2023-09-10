using FileStorage.HeaderAuthentication.DependencyInjection;
using System.Security.Claims;
using FileStorage.UserAuthentication;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using FileStorage.Domain.Repositories;
using FileStorage.Infrastructure.Persistance.Repositories;
using FileStorage.Application.Services.Contracts;
using FileStorage.Infrastructure.Services;
using FileStorage.Infrastructure.Services.Contracts;
using FileStorage.Application.Services;
using FileStorage.Application.Handlers;
using FileStorage.Infrastructure.Configurations;
using Azure.Core;
using Azure.Storage.Blobs;
using FileStorage.Infrastructure.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCustomAuthentication(
        options =>
        {
            options.Add(ClaimTypes.Name, "x-name", true);
            options.Add(ClaimTypes.Email, "x-email", true);
        });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(CustomAuthenticationScheme.Name)
        .RequireClaim(ClaimTypes.Name)
        .RequireClaim(ClaimTypes.Email)
        .Build();
});

builder.Services.AddSignalR();
builder.Services.AddMediatR(config => 
    config.RegisterServicesFromAssemblies(typeof(FileStatusNotificationHandler).Assembly));
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IFileRepository, InMemoryFileRepository>();
builder.Services.AddScoped<IUserConnectedService, UserConnectedService>();
builder.Services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<ICurrentUserIdProvider, CurrentUserIdProvider>();

builder.Services.AddSingleton<FileStorage.Application.Services.Contracts.IUserIdProvider, UserIdProvider>();
builder.Services.AddSingleton<Microsoft.AspNetCore.SignalR.IUserIdProvider, EmailUserIdProvider>();
builder.Services.AddSingleton<ITokenCredentialProvider, TokenCredentialProvider>();
builder.Services.AddSingleton<IBlobServiceClientProvider, BlobServiceClientProvider>();
builder.Services.AddSingleton<IBlobContainerNameProvider, BlobContainerNameProvider>();
builder.Services.AddSingleton<TokenCredential>(c => c.GetRequiredService<ITokenCredentialProvider>().Get());
builder.Services.AddSingleton<BlobServiceClient>(c => c.GetRequiredService<IBlobServiceClientProvider>().Get());

builder.Services.Configure<AzureConfiguration>(builder.Configuration.GetSection("Azure"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<UserHub>("/user");

app.Run();
