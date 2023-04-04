using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ppt23.Shared;
public class RevizeViewModel
{
    public string Name { get; set; } = "";

    public Guid Id { get; set; }
    public RevizeViewModel()
    {
        this.Name = RandomName(10);
    }
    public static string RandomName(int length) =>
           new(Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next('a', 'z')).ToArray());

    public static List<RevizeViewModel> RandSeznamRevize(int numDates)
    {
        List<RevizeViewModel> list = new List<RevizeViewModel>();

        for (int i = 0; i < 5; i++)
        {
            RevizeViewModel rm = new RevizeViewModel();
            rm.Id = Guid.NewGuid();
            list.Add(rm);
        }
        return list;
    }

}