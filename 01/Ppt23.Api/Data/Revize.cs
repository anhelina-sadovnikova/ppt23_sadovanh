using System.ComponentModel.DataAnnotations;

namespace Ppt23.Api.Data
{
    public class Revize
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
    }
}
