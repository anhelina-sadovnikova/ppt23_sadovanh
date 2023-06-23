using System.ComponentModel.DataAnnotations;

public class UkonVM
{
    [Key]
    public Guid ID { get; set; }

    public string Name { get; set; } = "";

    public Guid VybaveniId { get; set; }

    public DateTime DateTime { get; set; }

    public string Detail { get; set; } = "";

    public Guid PracovnikId { get; set; }


}