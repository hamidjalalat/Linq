using System.Linq;
using System.Data.Entity;

namespace Models
{
	internal class DatabaseContextInitializer :
		System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
	{
		public DatabaseContextInitializer()
		{
		}

		protected override void Seed(DatabaseContext databaseContext)
		{
			State oState = null;
			Country oCountry = null;

			for (int intCountryIndex = 1; intCountryIndex <= 5; intCountryIndex++)
			{
				oCountry =
					new Country()
					{
						Name =
							string.Format("Country ({0})", intCountryIndex),
					};

				// Note: It's too Important!
				oCountry.States =
					new System.Collections.Generic.List<State>();

				for (int intStateIndex = 1; intStateIndex <= 20000; intStateIndex++)
				{
					oState =
						new State()
						{
							Name =
								string.Format("Country ({0}) - State ({1})",
								intCountryIndex, intStateIndex),
						};

					oCountry.States.Add(oState);
				}

				databaseContext.Countries.Add(oCountry);

				databaseContext.SaveChanges();
			}

			// Optional
			databaseContext.SaveChanges();
		}
	}
}
