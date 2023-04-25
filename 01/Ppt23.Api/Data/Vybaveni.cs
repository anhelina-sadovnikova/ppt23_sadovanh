using System.ComponentModel.DataAnnotations;

public class Vybaveni
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public DateTime dateBuy { get; set; }
    public DateTime lastRev { get; set; }
}