using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            // _context.Database.EnsureCreated();
            // AddSamurais("Shimada", "Okamoto","Kikuchio", "Hayashida" );
            // AddVariousTypes();
            // QueryFind(2);
            // RetrieveAndUpdateMultipleSamurais();
            // GetSamurais();
            // MultipleDatabaseOperations();
            RetrieveAndDeleteSamurai();
            Console.Write("Press any key...");
            Console.ReadKey();
        }
        private static void AddVariousTypes()
        {
            _context.AddRange(new Samurai { Name = "Shimada" },
                                new Samurai { Name = "Okamoto" },
                                new Battle { Name = "Battle of Anegawa" },
                                new Battle { Name = "Battle of Nagashino" });
            _context.SaveChanges();
        }
        private static void AddSamuraisByName(params string[] names)
        {
            foreach (string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
            _context.SaveChanges();
        }
        private static void AddSamurais(Samurai[] samurais)
        {
            _context.Samurais.AddRange(samurais);
            _context.SaveChanges();
        }
        private static void GetSamurais()
        {
            var samurais = _context.Samurais
                .TagWith("ConsoleApp.Program.GetSamurais method")
                .ToList();
            Console.WriteLine($"Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
        private static void QueryFilters()
        {
            var filter = "J%";
            var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, filter)).ToList();
        }
        private static void QueryAggregates()
        {
            var name = "Sampson";
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == name);
        }
        private static void QueryFind(int key)
        {
            var samurai = _context.Samurais.Find(key);
        }
        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(4).ToList();
            samurais.ForEach(s => s.Name += "-San");
            _context.SaveChanges();
        }
        private static void MultipleDatabaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai { Name = "Shino" });
            _context.SaveChanges();
        }
        private static void RetrieveAndDeleteSamurai()
        {
            var samurai = _context.Samurais.Find(1);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
    }
}
