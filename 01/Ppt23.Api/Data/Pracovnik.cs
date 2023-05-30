using System.ComponentModel.DataAnnotations;

namespace Ppt23.Api.Data
{
    public class Pracovnik
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string JobTitle { get; set; } = "";
        public List<Ukon> Ukons { get; set; } = new();

    }
}
