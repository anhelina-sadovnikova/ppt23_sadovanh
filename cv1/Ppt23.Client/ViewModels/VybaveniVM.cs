namespace Ppt23.Client.ViewModels;

public class VybaveniVm
{
    public string Name { get; set; }
    public DateTime dateBuy { get; set; }
    public DateTime lastRev { get; set; }
    public bool IsRevNeeded { get => DateTime.Now.AddYears(-2) < lastRev; }
    public bool isInEditMode { get; set; } = false;

    public VybaveniVm()
    {
        DateTime od = new DateTime(2010, 01, 01);
        this.Name = RandomName(10);
        this.dateBuy = GetRandomDate(od, DateTime.Now);
        this.lastRev = GetRandomDate(dateBuy, DateTime.Now);
    }
    public static List<VybaveniVm> VratRandSeznam()
    {
        List<VybaveniVm> list = new List<VybaveniVm>();

        for (int i = 0; i < 5; i++)
        {
            VybaveniVm vm = new VybaveniVm();
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