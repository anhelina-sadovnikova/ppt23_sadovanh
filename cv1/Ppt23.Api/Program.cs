using Microsoft.AspNetCore.Mvc;
using Ppt23.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<VybaveniVm> seznamVybaveni = VybaveniVm.VratRandSeznam();

app.MapGet("/vybaveni", () =>
    {
        return seznamVybaveni;
    });

app.MapPost("/vybaveni", (VybaveniVm prichoziModel) =>
{
    prichoziModel.Id = Guid.NewGuid();
    seznamVybaveni.Insert(0, prichoziModel);
    return prichoziModel.Id;
});

app.MapPut("/vybaveni/{id}", (Guid id, [FromBody] VybaveniVm updatedItem) =>
{
    var existingItem = seznamVybaveni.FirstOrDefault(x => x.Id == id);
    if (existingItem == null)
    {
        return Results.NotFound($"Záznam s Id {id} nebyl nalezen.");
    }

    existingItem.Name = updatedItem.Name;
    existingItem.Price = updatedItem.Price;

    return Results.Ok();
});



app.Run();
