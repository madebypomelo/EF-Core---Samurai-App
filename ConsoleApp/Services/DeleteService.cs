using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace ConsoleApp.Services
{
    public class DeleteService
    {
        private static readonly SamuraiContext _context = new SamuraiContext();

        public static void DeleteProfile(int samuraiID)
        {
			#region 1. Best practice/normal procedure.
			// Fetch samurai and horse.
			var samurai = _context.Samurais.Find(samuraiID);

            _context.RemoveRange(samurai, samurai.Horse);
			#endregion

			#region 2. Bad practice/may have side effects.
			// Construct fake object, only holding the PrimaryKey.
			samurai = new Samurai()
			{
				ID = samuraiID
			};

			_context.RemoveRange(samurai, samurai.Horse);
			#endregion

			_context.SaveChanges();
		}
	}
}
