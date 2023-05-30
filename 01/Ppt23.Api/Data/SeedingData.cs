using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ppt23.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace Ppt23.Api.Data
{
    public class SeedingData
    {
        readonly PptDbContext _db;

        public SeedingData(PptDbContext db)
        {
            this._db = db;
        }
        public async Task SeedData()
        {
            if (!_db.Vybavenis.Any())//není žádné vybavení - nějaké přidáme
            {
                // vytvoř x vybaveních
                //.. přidej do db

                var vybaveniLis = VybaveniVm.VratRandSeznam(10, isEmtpyId: false).Select(x => x.Adapt<Vybaveni>());
                _db.Vybavenis.AddRange(vybaveniLis);

                int pocetPracovniku = Random.Shared.Next(2, 5);
                  for (int i = 0; i < pocetPracovniku; i++)
                    {
                        Pracovnik pracovnik = new()
                        {
                            Name = "Ukon Name",
                            JobTitle = RandomString(56).Replace("x", " "),

                        };
                        _db.Workers.Add(pracovnik);
                    }

                await _db.SaveChangesAsync();

                List<Pracovnik> pracovnikList = _db.Workers.ToList();
                List<Vybaveni> vybaveniList = _db.Vybavenis.ToList();

                foreach (Vybaveni vyb in vybaveniList)
                {
                    int pocetUkonu = Random.Shared.Next(5, 10);
                    for (int i = 0; i < pocetUkonu; i++)
                    {
                        Ukon uk = new()
                        {
                            Name = "Ukon Name",
                            DateTime = DateTime.UtcNow.AddDays(Random.Shared.Next(-100, -1)),
                            Detail = RandomString(56),
                            VybaveniId = vyb.Id,
                            PracovnikId = pracovnikList[Random.Shared.Next(pocetPracovniku)].Id
                        };
                        _db.Ukons.Add(uk);
                    }
                }
            }
            await _db.SaveChangesAsync();
        }
        public static string RandomString(int length) =>
           new(Enumerable.Range(0, length).Select(_ =>
           Random.Shared.Next(0, 5) == 0 ? ' ' : //randomly add spaces
           (char)Random.Shared.Next('a', 'z')).ToArray());//add random chars
    }
}
