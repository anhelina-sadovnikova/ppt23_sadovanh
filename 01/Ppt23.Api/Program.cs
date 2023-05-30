using Microsoft.AspNetCore.Mvc;
using Ppt23.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Ppt23.Api.Data;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SeedingData>();//for seeding data

var corsAllowedOrigin = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
string? sqlDatabase = builder.Configuration.GetValue<String>("sqlDatabase");
ArgumentNullException.ThrowIfNull(corsAllowedOrigin);

Console.WriteLine($"CORS ALLOWED: {corsAllowedOrigin}");

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins(corsAllowedOrigin)//👈
    .WithMethods("GET", "DELETE", "POST", "PUT")//👈 (musí být UPPERCASE)
    .AllowAnyHeader()
));

builder.Services.AddDbContext<PptDbContext>(opt => opt.UseSqlite($"Data Source={sqlDatabase}"));

var app = builder.Build();

app.UseCors();

app.Services.CreateScope().ServiceProvider
  .GetRequiredService<PptDbContext>()
  .Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//List<VybaveniVm> seznamVybaveni = VybaveniVm.VratRandSeznam();
List<RevizeVM> seznamRevizi = RevizeVM.VratRandSeznam(100);

// DONE
app.MapPost("/vybaveni", (VybaveniVm prichoziModel, PptDbContext _db) =>
{
    prichoziModel.Id = Guid.Empty;
    Vybaveni en = prichoziModel.Adapt<Vybaveni>();

    _db.Vybavenis.Add(en);
    _db.SaveChanges();

    return en.Id;
});

app.MapPost("/revize/{Id}", (Guid Id, PptDbContext db) =>
{
    Vybaveni? vybaveni = db.Vybavenis.Include(v => v.Revizes).SingleOrDefault(v => v.Id == Id);

    Revize revize = new()
    {
        Id = Guid.Empty,
        Name = $"#{vybaveni.Revizes.Count + 1} Revize - {vybaveni.Name}",
        VybaveniId = vybaveni.Id,
        DateTime = DateTime.Now
    };

    db.Vybavenis.SingleOrDefault(v => v.Id == revize.VybaveniId)?.Revizes.Add(revize);
    db.SaveChanges();

    return Results.Ok();
});

//DONE??
app.MapGet("/revize/{text}", (string text, PptDbContext db) =>
{
    Console.WriteLine(text);
    var filtrovaneRevize = db.Revisions.Where(x => x.Name.Contains(text)).ToList();
    Console.WriteLine(filtrovaneRevize);
    return filtrovaneRevize;
});
// DONE
app.MapGet("/vybaveni", (PptDbContext db) =>
{
    //List<VybaveniVm> vybavenis = db.Vybavenis.ProjectToType<VybaveniVm>().ToList();

    //return Results.Ok(vybavenis);
    List<VybaveniVm> vybaveniListVM = new();
    List<Vybaveni> vybaveniList = db.Vybavenis.ToList();
    List<Revize> revizeList = db.Revisions.ToList();

    foreach (var item in vybaveniList)
    {
        VybaveniVm vybaveniVM = item.Adapt<VybaveniVm>();

        var vybaveniRevizeList = revizeList.Where(r => r.VybaveniId == vybaveniVM.Id).ToList().OrderByDescending(r => r.DateTime);

        if (vybaveniRevizeList.Any())
            vybaveniVM.lastRev = vybaveniRevizeList.First().DateTime;

        else
            vybaveniVM.lastRev = vybaveniVM.dateBuy;

        vybaveniListVM.Add(vybaveniVM);
    }

    return Results.Ok(vybaveniListVM);
});

// DONE
app.MapPut("/vybaveni/{id}", (Guid id, [FromBody] VybaveniVm updatedItem, PptDbContext db) =>
{
    updatedItem.Id = id;
    var existingItem = db.Vybavenis.FirstOrDefault(x => x.Id == id);
    if (existingItem == null)
    {
        return Results.NotFound($"Záznam s Id {id} nebyl nalezen.");
    }

    updatedItem.Adapt(existingItem);
    db.SaveChanges();

    return Results.Ok();
});
// DONE
app.MapGet("/vybaveni/{Id}", (Guid Id, PptDbContext db) =>
{
    Vybaveni? hledany = db.Vybavenis
    .Include(x => x.Revizes)
    .Include(x => x.Ukons).ThenInclude(x => x.Pracovnik)
    .SingleOrDefault(x => x.Id == Id);

    var en = hledany?.Adapt<VybaveniSRevizemaVm>();
    return en;
}
);

//DONE
app.MapDelete("/vybaveni/{Id}", (Guid Id, PptDbContext db) =>
{
    var item = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (item == null)
        return Results.NotFound("Tato položka nebyla nalezena!!");
    db.Vybavenis.Remove(item);
    db.SaveChanges();

    return Results.Ok();
}
);

await app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingData>().SeedData();

app.Run();
