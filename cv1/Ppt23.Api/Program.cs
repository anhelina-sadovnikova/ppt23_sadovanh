using Microsoft.AspNetCore.Mvc;
using Ppt23.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins("https://localhost:1111")//👈
    .WithMethods("GET", "DELETE")//👈 (musí být UPPERCASE)
    .AllowAnyHeader()
));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

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
    existingItem.dateBuy = updatedItem.dateBuy;
    existingItem.lastRev = updatedItem.lastRev;

    return Results.Ok();
});

app.MapGet("/vybaveni/{Id}", (Guid Id) =>
{
    var item = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    return item;
}
);

app.MapDelete("/vybaveni/{Id}", (Guid Id) =>
{
    var item = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (item == null)
        return Results.NotFound("Tato položka nebyla nalezena!!");
    seznamVybaveni.Remove(item);
    return Results.Ok();
}
);

app.Run();
