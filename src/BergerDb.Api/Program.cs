using BergerDb.Domain;
using BergerDb.Application;
using BergerDb.Persistance;
using BergerDb.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Mapster;
using BergerDb.Application.Users.Register;
using BergerDb.Application.Users.Login;
using BergerDb.Api.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations<BergerDbContext>();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapPost("api/register", async (
    [FromBody] RegisterRequest request,
    ISender sender,
    CancellationToken cancellationToken = default) =>
{
    var command = request.Adapt<RegisterCommand>();

    var result = await sender.Send(command, cancellationToken);

    if (result.IsFailure)
    {
        return Results.BadRequest(result.Errors);
    }

    return Results.Ok(result.Value);
});

app.MapPost("api/login", async (
    [FromBody] LoginRequest request,
    ISender sender,
    CancellationToken cancellationToken = default) =>
{
    var command = request.Adapt<LoginCommand>();

    var result = await sender.Send(command, cancellationToken);

    if (result.IsFailure)
    {
        return Results.BadRequest(result.Errors);
    }

    return Results.Ok(result.Value);
});

app.MapGet("api/test", () => 
{
    return Results.Ok("Hello");
})
.RequireAuthorization();

app.Run();

record RegisterRequest(string Email, string Password);

record LoginRequest(string Email, string Password);