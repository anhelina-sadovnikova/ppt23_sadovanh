using Bogus;
using Bogus.Extensions.Denmark;

namespace Ppt23.Shared;

public class DataGenerator
    {
        Faker<VybaveniVm> VybaveniFake;
        Faker<PracovnikVM> PracovnikFake;
        Faker<UkonVM> UkonFake;

    public DataGenerator()
    {
            Randomizer.Seed = new Random(123);

             VybaveniFake = new Faker<VybaveniVm>()
                .RuleFor(u => u.Name, f => f.Random.String2(5, 10))
                .RuleFor(u => u.Price, f => f.Random.Int(5000, 10000))
                .RuleFor(u => u.dateBuy, f => f.Date.Past(2));

            PracovnikFake = new Faker<PracovnikVM>()
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.JobTitle, f => f.PickRandom<JobTitle>());

            UkonFake = new Faker<UkonVM>()
                .RuleFor(u => u.Name, f => f.Random.String(5, 10))
                .RuleFor(u => u.DateTime, f => f.Date.Past(1))  //   OPRAVIT?
                .RuleFor(u => u.Detail, f => f.Lorem.Word());
    }

        public VybaveniVm GenerateFakeVybaveni()
        {
            return VybaveniFake.Generate();
        }

        public IEnumerable<VybaveniVm> GenerateVybavenis()
        {
            return VybaveniFake.GenerateForever();
        }

        public IEnumerable<PracovnikVM> GenerateWorkers()
        {
            return PracovnikFake.GenerateForever();
        }
        public IEnumerable<UkonVM> GenerateUkons()
        {
            return UkonFake.GenerateForever();
        }
}

