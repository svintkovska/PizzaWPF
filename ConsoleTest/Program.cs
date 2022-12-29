using DAL.Data;
using DAL.Data.Entities;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseSeeder.Seed();
        }
    }
}
