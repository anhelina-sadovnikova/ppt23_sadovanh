using System.ComponentModel.DataAnnotations;

public class UkonVM
{
    [Key]
    public Guid ID { get; set; }

    public string Name { get; set; } = "";

    public Guid VybaveniID { get; set; }

    public DateTime DateTime { get; set; }

}