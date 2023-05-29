using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ppt23.Shared
{
    public class VybaveniSRevizemaVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";
        public Guid RevizeId { get; set; }
        public List<RevizeVM> Revisions { get; set; } = new List<RevizeVM>();
        public List<UkonVM> Ukons { get; set; } = new List<UkonVM>();
    }
}
