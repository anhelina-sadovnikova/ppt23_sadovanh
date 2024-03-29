using Mapster;
using Ppt23.Shared;
using System.ComponentModel.DataAnnotations;

namespace Ppt23.Api.Data;

public class Vybaveni
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public DateTime dateBuy { get; set; }
    public List<Revize> Revizes { get; set; } = new();
    public List<Ukon> Ukons { get; set; } = new();

}