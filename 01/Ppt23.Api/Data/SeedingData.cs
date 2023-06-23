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

        readonly DataGenerator data;

        public SeedingData(PptDbContext db, DataGenerator data)
        {
            this._db = db;
            this.data = data;
        }
        public async Task SeedData()
        {
            if (!_db.Vybavenis.Any())//není žádné vybavení - nějaké přidáme
            {
                // vytvoř x vybaveních
                //.. přidej do db

                var results = data.GenerateVybavenis().Take(10).Select(x => x.Adapt<Vybaveni>());
                _db.Vybavenis.AddRange(results);

                //pracovniky
                var resultsWorkers = data.GenerateWorkers().Take(5).Select(x => x.Adapt<Pracovnik>());
                _db.Workers.AddRange(resultsWorkers);

                await _db.SaveChangesAsync();

                List<Pracovnik> pracovnikList = _db.Workers.ToList();
                List<Vybaveni> vybaveniList = _db.Vybavenis.ToList();

                //ukony
                foreach (Vybaveni vyb in vybaveniList)
                {
                        var resultsUkons = data.GenerateUkons().Select(x => x.Adapt<Ukon>());
                            foreach (Ukon uk in resultsUkons)
                            {
                                uk.VybaveniId = vyb.Id;
                                uk.PracovnikId = pracovnikList[1].Id;
                            }
                    _db.Ukons.AddRange(resultsUkons);
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
