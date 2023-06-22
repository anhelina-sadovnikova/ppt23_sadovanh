using Bogus;

namespace Ppt23.Shared;

public class DataGenerator
    {
        Faker<VybaveniVm> VybaveniFake;

        public DataGenerator()
        {
            Randomizer.Seed = new Random(123);

             VybaveniFake = new Faker<VybaveniVm>()
                .RuleFor(u => u.Name, f => f.Random.String(5, 10))
                .RuleFor(u => u.Price, f => f.Random.Int(5000, 10000))
                .RuleFor(u => u.dateBuy, f => f.Date.Past(2));
        }

        public VybaveniVm GenerateFakeVybaveni()
        {
            return VybaveniFake.Generate();
        }

        public IEnumerable<VybaveniVm> GenerateVybavenis()
        {
            return VybaveniFake.GenerateForever();
        }

    }

