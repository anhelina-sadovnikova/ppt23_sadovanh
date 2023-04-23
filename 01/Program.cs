// See https://aka.ms/new-console-template for more information
using System;

Console.WriteLine("Hello, World!");

Random nh = new Random();
int cislo = nh.Next(0, 101);
Console.WriteLine(cislo.ToString());

Console.Write("Zadej cislo: ");
int cisloUzivatele = Convert.ToInt32(Console.ReadLine());


while (true)
    {
    if (cislo > cisloUzivatele)
    {
            Console.WriteLine("Zadej vetsi cislo");
            cisloUzivatele = Convert.ToInt32(Console.ReadLine());
    }
    else if (cislo < cisloUzivatele)
    {
            Console.WriteLine("Zadej mensi cislo");
            cisloUzivatele = Convert.ToInt32(Console.ReadLine());
    }
    else if (cisloUzivatele == cislo)
    {
        Console.WriteLine($"Spravne, je to cislo {cislo}");
    }
}
