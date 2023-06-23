using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ppt23.Shared
{
    public class PracovnikVM
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public JobTitle JobTitle { get; set; }
    }
}
