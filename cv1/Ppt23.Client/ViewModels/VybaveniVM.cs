namespace Ppt23.Client.ViewModels;

public class VybaveniVm
{
    public string Name { get; set; }
    public DateTime dateBuy { get; set; }
    public DateTime lastRev { get; set; }
    public bool IsRevNeeded { get => lastRev + new TimeSpan(1095, 0, 0, 0) < DateTime.Now; }

    public VybaveniVm()
    {
        DateTime od = new DateTime(2010, 01, 01);
        this.Name = randName();
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
    public string randName()
    {
        Random rand = new Random();

        String str = "abcdefghijklmnopqrstuvwxyz";
        int size = 5;

        String ran = "";

        for (int i = 0; i < size; i++)
        {
            int x = rand.Next(26);
            ran = ran + str[x];
        }
        return ran;
    }

    public static DateTime GetRandomDate(DateTime from, DateTime to)
    {
        Random rand = new Random();
        var range = to - from;

        var randTimeSpan = new TimeSpan((long)(rand.NextDouble() * range.Ticks));

        return from + randTimeSpan;
    }

}