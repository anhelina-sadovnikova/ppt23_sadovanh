using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ppt23.Shared;

public class VybaveniVm
{
    [Required(ErrorMessage = "Pole nesmí být prázdné")]
    [MinLength(5, ErrorMessage = "Délka u pole \"{0}\" musí být alespoň {1} znaků")]
    [Display(Name = "Název")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pole nesmí být prázdné")]
    [Range(0, 10000000, ErrorMessage = "Cena musí být v rozmezí 0 až 10 000 000")]
    public int Price { get; set; }
    public DateTime dateBuy { get; set; }
    public DateTime lastRev { get; set; }
    public bool IsRevNeeded { get => DateTime.Now.AddYears(-2) > lastRev; }
    public Guid Id { get; set; }
    public VybaveniVm()
    {
        DateTime od = new DateTime(2010, 01, 01);
        this.Name = RandomName(10);
        Random rnd = new Random();
        this.Price = rnd.Next(0, 10000000);
        this.dateBuy = GetRandomDate(od, DateTime.Now);
        this.lastRev = GetRandomDate(dateBuy, DateTime.Now);
    }
    public VybaveniVm Copy()
    {
        VybaveniVm to = new();
        to.Name = Name;
        to.Price = Price;
        to.dateBuy = dateBuy;
        to.lastRev = lastRev;
        return to;
    }
    public void MapTo(VybaveniVm? to)
    {
        if (to == null) return;
        to.Name = Name;
        to.Price = Price;
        to.dateBuy = dateBuy;
        to.lastRev = lastRev;
    }
    public static List<VybaveniVm> VratRandSeznam()
    {
        List<VybaveniVm> list = new List<VybaveniVm>();

        for (int i = 0; i < 5; i++)
        {
            VybaveniVm vm = new VybaveniVm();
            vm.Id = Guid.NewGuid();
            list.Add(vm);
        }
        return list;
    }
    public static void PridatJednoVybaveni(List<VybaveniVm> list)
    {
        VybaveniVm vm = new VybaveniVm();
        list.Add(vm);
    }
    public static string RandomName(int length) =>
           new(Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next('a', 'z')).ToArray());

    public static DateTime GetRandomDate(DateTime from, DateTime to)
    {
        Random rand = new Random();
        var range = to - from;

        var randTimeSpan = new TimeSpan((long)(rand.NextDouble() * range.Ticks));

        return from + randTimeSpan;
    }



}