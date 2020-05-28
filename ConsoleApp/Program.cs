using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
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
			GetSamuraiWithBattles();
			Console.Write("Press any key...");
			Console.ReadKey();
		}

		private static void InsertNewSamuraiWithAQuote()
		{
			var samurai = new Samurai
			{
				Name = "Kyuzo",
				Quotes = new List<Quote>
				{
					new Quote { Text = "Watch out for my sharp sword!" },
					new Quote { Text = "I told you to watch out for the sharp sword! Oh well!" }
				}
			};
			_context.Samurais.Add(samurai);
			_context.SaveChanges();
		}

		private static void AddQuoteToExistingSamuraiWhileTracked()
		{
			var samurai = _context.Samurais.FirstOrDefault();
			samurai.Quotes.Add(new Quote
			{
				Text = "I bet you're happy that I've saved you!"
			});
			_context.SaveChanges();
		}

		private static void AddQuoteToExistingSamuraiNotTracked(int samuraiID)
		{
			var samurai = _context.Samurais.Find(samuraiID);
			samurai.Quotes.Add(new Quote
			{
				Text = "Now that I saved you, will you feed me dinner?"
			});
			using var newContexct = new SamuraiContext();
			newContexct.Samurais.Attach(samurai);
			newContexct.SaveChanges();
		}

		private static void AddQuoteToExistingSamuraiNotTracked_Easy(int samuraiID)
		{
			var quote = new Quote
			{
				Text = "Now that I saved you, will you feed me again?",
				SamuraiID = samuraiID
			};

			using var newContexct = new SamuraiContext();
			newContexct.Quotes.Add(quote);
			newContexct.SaveChanges();

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

		private static void EagerLoadSamuraiWithQuotes()
		{
			var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
		}

		private static void ProjectSomeProperties()
		{
			var someProperties = _context.Samurais.Select(s => new { s.ID, s.Name, HappyQuotes = s.Quotes.Where(x => x.Text.Contains("Happy")).ToList() }).ToList();
		}

		private static void ExplicitLoadQuotes()
		{
			var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Sara"));
			_context.Entry(samurai).Collection(s => s.Quotes).Load();
			_context.Entry(samurai).Reference(s => s.Horse).Load();
		}

		private static void FilteringWithRelatedData()
		{
			var samurai = _context.Samurais.Where(s => s.Quotes.Any(q => q.Text.Contains("happy"))).ToList();
		}

		private static void ModifyRelatedDataWhenTracked()
		{
			var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.ID == 2);
			samurai.Quotes[0].Text = "Did you hear that?";
			_context.SaveChanges();
		}
		
		private static void ModifyRelatedDataWhenNotTracked()
		{
			var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.ID == 5);
			samurai.Quotes[0].Text = "Did you hear that again?";

			using var newContext = new SamuraiContext();
			newContext.Entry(samurai.Quotes[0]).State = EntityState.Modified;
			newContext.SaveChanges();
		}

		private static void JoinBattleAndSamurai()
		{
			// Samurai and battle already exist and we have their ID's.
			var sbJoin = new SamuraiBattle { SamuraiID = 1, BattleID = 3 };
			_context.Add(sbJoin);
			_context.SaveChanges();
		}
		
		private static void EnlistSamuraiIntoABattle()
		{
			var battle = _context.Battles.Find(1);
			battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiID = 21 });
			_context.SaveChanges();
		}

		private static void RemoveJoinBetweenSamuraiAndBattleSimple()
		{
			var join = new SamuraiBattle { SamuraiID = 1, BattleID = 2 };
			_context.Remove(join);
			_context.SaveChanges();
		}

		private static void GetSamuraiWithBattles()
		{
			var samuraiWithBattle = _context.Samurais
				.Include(s => s.SamuraiBattles)
				.ThenInclude(sb => sb.Battle)
				.FirstOrDefault(s => s.ID == 5);

			var samuraiWithBattleCleaner = _context.Samurais.Where(s => s.ID == 5)
				.Select(s => new
				{
					Samurai = s,
					Battles = s.SamuraiBattles.Select(sb => sb.Battle)
				}).FirstOrDefault();
		}

		private static void AddNewSamuraiWithHorse()
		{
			var samurai = new Samurai
			{
				Name = "Julemanden"
			};
			samurai.Horse = new Horse { Name = "Silver" };

			_context.Samurais.Add(samurai);
			_context.SaveChanges();
		}
		
		private static void AddNewHorseToSamuraiUsingID()
		{
			var horse = new Horse { Name = "Scout", SamuraiID = 2 };

			_context.Add(horse);
			_context.SaveChanges();
		}
		
		private static void AddNewHorseToSamuraiObject()
		{
			var samurai = _context.Samurais.Find(3);
			samurai.Horse = new Horse { Name = "Black Betty" };
			_context.SaveChanges();
		}
		
		private static void AddNewHorseToDisconnectedSamuraiObject()
		{
			var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s => s.ID == 4);
			samurai.Horse = new Horse { Name = "Mr. Ed" };

			using var newContext = new SamuraiContext();

			newContext.Attach(samurai);
			newContext.SaveChanges();
		}

		private static void ReplaceHorce()
		{
			var samurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.ID == 1);
			samurai.Horse = new Horse { Name = "Trigger" };

			_context.SaveChanges();
		}
	}
}
