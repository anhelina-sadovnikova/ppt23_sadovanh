using Bogus;
using Bogus.Extensions.Denmark;

namespace Ppt23.Shared;

public class DataGenerator
    {
        Faker<VybaveniVm> VybaveniFakeDev;
        Faker<VybaveniVm> VybaveniFakeProd;
        Faker<PracovnikVM> PracovnikFake;
        Faker<UkonVM> UkonFake;

    public DataGenerator()
    {
            Randomizer.Seed = new Random(123);

             VybaveniFakeDev = new Faker<VybaveniVm>()
                .RuleFor(u => u.Name, f => f.Random.String(5, 10)) // jiny pismena
                .RuleFor(u => u.Price, f => f.Random.Int(5000, 10000))
                .RuleFor(u => u.dateBuy, f => f.Date.Past(2));

              VybaveniFakeProd = new Faker<VybaveniVm>()
                .RuleFor(u => u.Name, f => f.Random.String2(5, 10))  // latinsky pismena
                .RuleFor(u => u.Price, f => f.Random.Int(5000, 10000))
                .RuleFor(u => u.dateBuy, f => f.Date.Past(2));

            PracovnikFake = new Faker<PracovnikVM>()
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.JobTitle, f => f.PickRandom<JobTitle>());

            UkonFake = new Faker<UkonVM>()
                .RuleFor(u => u.Name, f => f.Random.String2(5, 10))
                .RuleFor(u => u.DateTime, f => f.Date.Past(1))  //   OPRAVIT?
                .RuleFor(u => u.Detail, f => f.Lorem.Word());
    }


        public IEnumerable<VybaveniVm> GenerateVybavenis()
        {
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            return VybaveniFakeDev.GenerateForever();

        }
        else
        {
            return VybaveniFakeProd.GenerateForever();
        }
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

