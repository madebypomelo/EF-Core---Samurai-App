using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace ConsoleApp
{
	internal class Program
	{
		private static SamuraiContext _context = new SamuraiContext();

		static void Main(string[] args)
		{
			//_context.Database.EnsureCreated();

			//AddSamurai();
			//GetSamurais("After Add:");
			//InsertMultipleSamurais();
			//QueryFilters("Sara");
			QueryAndUpdateBattle_Disconnected();
			Console.Write("Press any key...");
			Console.ReadKey();
		}

		private static void InsertBattle()
		{
			_context.Battles.Add(new Battle()
			{
				Name = "Battle of Okehazama",
				StartDate = new DateTime(1560, 05, 01),
				EndDate = new DateTime(1560, 06, 15)
			});
			_context.SaveChanges();
		}

		private static void QueryAndUpdateBattle_Disconnected()
		{
			var battle = _context.Battles.AsNoTracking().FirstOrDefault();
			battle.EndDate = new DateTime(1560, 06, 30);

			using (var newContextInstance = new SamuraiContext())
			{
				newContextInstance.Battles.Update(battle);
				newContextInstance.SaveChanges();
			}
		}

		private static void InsertMultipleSamurais()
		{
			var samurai = new Samurai()
			{
				Name = "Andreas"
			};
			var samurai2 = new Samurai()
			{
				Name = "Sara"
			};
			_context.Samurais.AddRange(samurai, samurai2);
			_context.SaveChanges();
		}

		private static void AddSamurai()
		{
			var samurai = new Samurai()
			{
				Name = "Andreas"
			};
			_context.Samurais.Add(samurai);
			_context.SaveChanges();
		}

		private static void GetSamurais(string text)
		{
			var samurais = _context.Samurais.ToList();

			Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
			foreach (var samurai in samurais)
			{
				Console.WriteLine(samurai.Name);
			}
		}

		private static void QueryFilters(string name)
		{
			//var samurais = _context.Samurais.Where(s => s.Name == name).ToList();
			//var samurais = _context.Samurais.Find(2);
			var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "S%")).ToList();
		}
	}
}
