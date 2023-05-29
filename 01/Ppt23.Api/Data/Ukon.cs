using Ppt23.Api.Data;
using Ppt23.Shared;
using System.ComponentModel.DataAnnotations;

namespace Ppt23.Api.Data;

public class Ukon
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime DateTime { get; set; }

    public Guid VybaveniId { get; set; }

    public Vybaveni Vybaveni { get; set; } = null!;


}
