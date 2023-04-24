using Microsoft.AspNetCore.Mvc;
using Ppt23.Shared;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsAllowedOrigin = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
ArgumentNullException.ThrowIfNull(corsAllowedOrigin);

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins(corsAllowedOrigin)//👈
    .WithMethods("GET", "DELETE", "POST", "PUT")//👈 (musí být UPPERCASE)
    .AllowAnyHeader()
));

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
List<RevizeVM> seznamRevizi = RevizeVM.VratRandSeznam(100);

app.MapGet("/revize/{text}", (string text) =>
{
    var filtrovaneRevize = seznamRevizi.Where(x => x.Name.Contains(text)).ToList();
    return Results.Ok(filtrovaneRevize);
});

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
