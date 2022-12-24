using DAL.Data;
using DAL.Data.Entities;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EFAppContext context = new EFAppContext();
            CategoryEntity cat = new CategoryEntity()
            {
                Name = "Смачні стави",
                DateCreated = DateTime.Now
            };
            context.Categories.Add(cat);
            context.SaveChanges();
            Console.WriteLine("Create id {0}", cat.Id);
        }
    }
}
