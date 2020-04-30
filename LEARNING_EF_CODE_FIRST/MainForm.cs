using System.Linq;
using System.Data.Entity;

namespace LEARNING_EF_CODE_FIRST
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private Models.DatabaseContext _myDatabaseContext;
		protected virtual Models.DatabaseContext MyDatabaseContext
		{
			get
			{
				if (_myDatabaseContext == null)
				{
					_myDatabaseContext =
						new Models.DatabaseContext();
				}

				return (_myDatabaseContext);
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			try
			{
                //Mr.Hamid Jalalat
                //اگر بخواهیم چندین مقدار را در شر ط بیاوریم
                System.Guid[] Ids = {new System.Guid() , new System.Guid()};
                var countray = MyDatabaseContext.Countries.Where(C => Ids.Contains(C.Id)).ToList();
                // **************************************************

                // **************************************************
                // دو دستور ذيل کاملا با هم معادل می باشند

                MyDatabaseContext.Countries
					.Load();

				// استفاده می کنيم Local از

				var varCountries =
					MyDatabaseContext.Countries
					.ToList()
					;

				// varCountries معادل MyDatabaseContext.Countries.Local

				// "SELECT * FROM Countries"
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.Load();

				// "SELECT * FROM Countries WHERE Code >= 10"
				// **************************************************

				// **************************************************
				// دو دستور ذيل با هم معادل می باشند

				MyDatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.Where(current => current.Code <= 20)
					.Load();

				MyDatabaseContext.Countries
					.Where(current => current.Code >= 10 && current.Code <= 20)
					.Load();

				// "SELECT * FROM Countries WHERE Code >= 10 AND Code <= 20"
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Code < 10 || current.Code > 20)
					.Load();

				// "SELECT * FROM Countries WHERE Code < 10 OR Code > 20"
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Name == "ايران")
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => string.Compare(current.Name, "ايران", true) == 0)
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Name.StartsWith("ا"))
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Name.EndsWith("ن"))
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Name.Contains("را"))
					.Load();
				// **************************************************

				// **************************************************
				// Note: دقت کنيد که دستور ذيل کار نمی کند
				string strText = "علی%علوی";

				MyDatabaseContext.Countries
					.Where(current => current.Name.Contains(strText))
					.Load();

				// string strText = "علی علوی رضايی";
				// strText = strText.Replace(" ", "%");
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Name.Contains("علی"))
					.Where(current => current.Name.Contains("علوی"))
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.OrderBy(current => current.Code)
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.OrderByDescending(current => current.Code)
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.OrderBy(current => current.Code)
					.Load();
				// **************************************************

				// **************************************************
				// Note: Wrong Usage!
				MyDatabaseContext.Countries
					.OrderBy(current => current.Code)
					.OrderBy(current => current.Name)
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.OrderBy(current => current.Code)
					.ThenBy(current => current.Name)
					.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.OrderBy(current => current.Id)
					.ThenByDescending(current => current.Name)
					.Load();
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				Models.Country oCountry = null;
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Code == 1)
					.Load();

				oCountry =
					MyDatabaseContext.Countries.Local[0];

				int intStateCount = 0;

				// In Lazy Mode (If the states property with the virtual keyword):
				// States will be created and will be loaded automatically.
				intStateCount =
					oCountry.States.Count;

				// In Normal Mode: States is null!
				intStateCount =
					oCountry.States.Count;
				// **************************************************

				// **************************************************
				var varStates =
					MyDatabaseContext.States
					.Where(current => current.CountryId == oCountry.Id)
					.ToList()
					;

				// "SELECT * FROM States WHERE CountryId = " + oCountryId

				int intStateCountOfSomeCountry1 = varStates.Count;
				// **************************************************

				// **************************************************
				int intStateCountOfSomeCountry2 =
					MyDatabaseContext.States
					.Where(current => current.CountryId == oCountry.Id)
					.Count();

				// "SELECT COUNT(*) FROM States WHERE CountryId = " + oCountryId
				// **************************************************

				// **************************************************
				// Undocumented!
				int intStateCountOfSomeCountry3 =
					MyDatabaseContext.Entry(oCountry)
						.Collection(current => current.States)
						.Query()
						.Count();
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				var varStatesOfSomeCountry1 =
					oCountry.States
					.Where(current => current.Code <= 10)
					.ToList()
					;
				// **************************************************

				// **************************************************
				// Undocumented!
				var varStatesOfSomeCountry2 =
					MyDatabaseContext.Entry(oCountry)
					.Collection(current => current.States)
					.Query()
					.Where(state => state.Code <= 10)
					.ToList()
					;
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// اگر بخواهيم به ازای هر کشوری، استان‏های مربوط به آنرا، در همان بار اول، بارگذاری کنيم
				MyDatabaseContext.Countries
					.Include("States")
					.Where(current => current.Code >= 10)
					.Load();

				oCountry =
					MyDatabaseContext.Countries.Local[0];

				intStateCount =
					oCountry.States.Count;
				// **************************************************

				// **************************************************
				// Note: Strongly Typed
				MyDatabaseContext.Countries
					.Include(current => current.States)
					.Where(current => current.Code >= 10)
					.Load();

				oCountry =
					MyDatabaseContext.Countries.Local[0];

				intStateCount =
					oCountry.States.Count;
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Include("States")
					.Include("States.Cities")
					.Where(current => current.Code >= 10)
					.Load();

				oCountry =
					MyDatabaseContext.Countries.Local[0];

				intStateCount =
					oCountry.States.Count;
				// **************************************************

				// **************************************************
				// Undocumented!
				// Note: Strongly Typed
				MyDatabaseContext.Countries
					.Include(current => current.States)
					.Include(current => current.States.Select(state => state.Cities))
					.Where(current => current.Code >= 10)
					.Load();

				oCountry =
					MyDatabaseContext.Countries.Local[0];

				intStateCount =
					oCountry.States.Count;
				// **************************************************

				// **************************************************
				//MyDatabaseContext.Countries
				//	.Include(current => current.States)
				//	.Include(current => current.States.Select(state => state.Cities))
				//	.Include(current => current.States.Select(state => state.Cities.Select(city => city.Sections))
				//	.Where(current => current.Code >= 10)
				//	.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Cities
					.Include("State")
					.Include("State.Country")
					.Where(current => current.Code >= 10)
					.Load();

				string strCountryName =
					MyDatabaseContext.Cities.Local[0].State.Country.Name;
				// **************************************************

				// **************************************************
				// Note: Strongly Typed
				MyDatabaseContext.Cities
					.Include(current => current.State)
					.Include(current => current.State.Country)
					.Where(current => current.Code >= 10)
					.Load();
				// **************************************************

				// **************************************************
				// صورت مساله
				// من تمام کشورهايی را می خواهم که در لااقل نام يکی از استان های آن حرف {بی} وجود داشته باشد
				MyDatabaseContext.Countries
					// دقت کنيد که صرف شرط ذيل، نيازی به دستور
					// Include
					// نيست
					//.Include(current => current.States)
					.Where(current => current.States.Any(state => state.Name.Contains("B")))
					.Load();

				// Note: Wrong Answer
				//MyDatabaseContext.States
				//	.Where(current => current.Name.Contains("B"))
				//	.Select(current => current.Country)
				//	.Load();
				// **************************************************

				// **************************************************
				// صورت مساله
				// من تمام کشورهايی را می خواهم که در لااقل نام يکی از شهرهای آن حرف {بی} وجود داشته باشد
				MyDatabaseContext.Countries
					// دقت کنيد که صرف شرط ذيل، نيازی به دستور
					// Include
					// نيست
					//.Include(current => current.States)
					//.Include(current => current.States.Select(state => state.Cities))
					.Where(current => current.States.Any(state => state.Cities.Any(city => city.Name.Contains("B"))))
					.Load();
				// **************************************************

				//MyDatabaseContext.Countries
				//	.Where(current => current.States.Where(state => state.Cities.Contains("B")))
				//	.Load();

				// **************************************************
				// صورت مساله
				// تمام شهرهايی را می خواهيم که جمعيت کشور آنها بيش از يکصد ميليون نفر باشد
				MyDatabaseContext.Cities
					// دقت کنيد که صرف شرط ذيل، نيازی به دستور
					// Include
					// نيست
					//.Include(current => current.State)
					//.Include(current => current.State.Country)
					.Where(current => current.State.Country.Population >= 10000000)
					.Load();
				// **************************************************

				// **************************************************
				//var varHotels =
				//	MyDatabaseContext.Hotels
				//	.Where(current => current.Region.City.State.Country.Id == viewModel.CountryId)
				//	.ToList();

				//var varHotels =
				//	MyDatabaseContext.Hotels
				//	.Where(current => current.Region.City.State.Id == viewModel.StateId)
				//	.ToList();

				//var varHotels =
				//	MyDatabaseContext.Hotels
				//	.Where(current => current.Region.City.Id == viewModel.CityId)
				//	.ToList();

				//var varHotels =
				//	MyDatabaseContext.Hotels
				//	.Where(current => current.Region.Id == viewModel.RegionId)
				//	.ToList();
				// **************************************************

				// **************************************************
				int intCountryCount = 0;
				// **************************************************

				// **************************************************
				// خاطره
				//MyDatabaseContext.Countries
				//	.Where(current => current.Code => 5)
				//	.Where(current => current.Code <= 45)
				//	.Load();
				// **************************************************

				// **************************************************
				MyDatabaseContext.Countries
					.Where(current => current.Code >= 5)
					.Where(current => current.Code <= 45)
					.Load();

				intCountryCount =
					MyDatabaseContext.Countries.Local.Count;

				MyDatabaseContext.Countries
					.Where(current => current.Code >= 10)
					.Where(current => current.Code <= 50)
					.Load();

				intCountryCount =
					MyDatabaseContext.Countries.Local.Count;
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				//System.Linq.IQueryable<Models.Country> oData =
				//	MyDatabaseContext.Countries
				//		.Include(current => current.States)
				//		;

				//var varData =
				//	MyDatabaseContext.Countries
				//		.Include(current => current.States)
				//		;

				//var varData =
				//	MyDatabaseContext.Countries
				//		.Where(current => 1 == 1)
				//		;

				var varData =
					MyDatabaseContext.Countries
					.AsQueryable()
					;

				if (string.IsNullOrWhiteSpace(txtCountryName.Text) == false)
				{
					varData =
						varData
						.Where(current => current.Name.Contains(txtCountryName.Text))
						;
				}

				if (string.IsNullOrWhiteSpace(txtCountryCodeFrom.Text) == false)
				{
					// Note: Wrong Usage!
					//varData =
					//	varData
					//	.Where(current => current.Code >= System.Convert.ToInt32(txtCountryCodeFrom.Text))
					//	;
					// Note: /Wrong Usage!

					int intCountryCodeFrom =
						System.Convert.ToInt32(txtCountryCodeFrom.Text);

					varData =
						varData
						.Where(current => current.Code >= intCountryCodeFrom)
						;
				}

				if (string.IsNullOrWhiteSpace(txtCountryCodeTo.Text) == false)
				{
					int intCountryCodeTo =
						System.Convert.ToInt32(txtCountryCodeTo.Text);

					varData =
						varData
						.Where(current => current.Code <= intCountryCodeTo)
						;
				}

				varData =
					varData
					.OrderBy(current => current.Id)
					;

				varData
					.Load();

				// يا

				var varResult = varData.ToList();

				// varResult معادل MyDatabaseContext.Countries.Local
				// **************************************************

				// **************************************************
				string strSearch = "   Ali       Reza  Iran Carpet   Ali         ";

				strSearch = strSearch.Trim();

				while (strSearch.Contains("  "))
				{
					strSearch = strSearch.Replace("  ", " ");
				}

				//strSearch = "Ali Reza Iran Carpet Ali";

				var varKeywords = strSearch.Split(' ').Distinct();

				//varKeywords = { "Ali", "Reza", "Iran", "Carpet" };

				var varSomething =
					MyDatabaseContext.Countries
					.AsQueryable()
					;

				// Solution (1)
				foreach (string strKeyword in varKeywords)
				{
					varSomething =
						varSomething.Where(current => current.Name.Contains(strKeyword))
						;
				}
				// /Solution (1)

				// Solution (2)
				// Mr. Farshad Rabiei
				varSomething =
					varSomething.Where(current => varKeywords.Contains(current.Name));
				// /Solution (2)

				varSomething
					.Load();

				// يا

				varSomething
					.ToList()
					;
				// **************************************************

				// **************************************************
				// روش اول
				// دستورات ذيل کاملا با هم معادل هستند
				MyDatabaseContext.Countries
					.Load();

				// MyDatabaseContext.Countries.Local

				// روش دوم
				var varSomeData1 =
					MyDatabaseContext.Countries
					.ToList()
					;

				// روش سوم
				var varSomeData2 =
					from Country in MyDatabaseContext.Countries
					select (Country)
					;
				// **************************************************

				//// **************************************************
				//var varSomeData3 =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	select (Country)
				//	;
				//// **************************************************

				//// **************************************************
				//var varSomeData4 =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby Country.Name
				//	select Country
				//	;

				//foreach (Models.Country oCurrentCountry in varSomeData4)
				//{
				//	System.Windows.Forms.MessageBox.Show(oCurrentCountry.Name);
				//}
				//// **************************************************

				//// **************************************************
				//// ها String آرایه ای از
				//// (A)
				//var varSomeData5 =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby Country.Name
				//	select Country.Name
				//	;

				//// Select Name From Countries
				//// **************************************************

				//// **************************************************
				//// ها Object آرایه ای از
				//// (B)
				//var varSomeData6 =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby Country.Name
				//	select new { Country.Name }
				//	;

				//// Note: Wrong Usage!
				////foreach (Models.Country oCurrentCountry in varSomeData6)
				////{
				////	System.Windows.Forms.MessageBox.Show(oCurrentCountry.Name);
				////}

				//foreach (var varCurrentPartialCountry in varSomeData6)
				//{
				//	System.Windows.Forms.MessageBox.Show(varCurrentPartialCountry.Name);
				//}
				//// **************************************************

				//// **************************************************
				//// (C)
				//var varSomeData7 =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby (Country.Name)
				//	select new { Baghali = Country.Name }
				//	;

				//// Note: Wrong Usage!
				////foreach (Models.Country oCurrentCountry in varSomeData7)
				////{
				////	System.Windows.Forms.MessageBox.Show(oCurrentCountry.Name);
				////}

				//// Note: Wrong Usage!
				////foreach (var varCurrentPartialCountry in varSomeData7)
				////{
				////	System.Windows.Forms.MessageBox.Show(varCurrentPartialCountry.Name);
				////}

				//foreach (var varCurrentPartialCountry in varSomeData7)
				//{
				//	System.Windows.Forms.MessageBox.Show(varCurrentPartialCountry.Baghali);
				//}
				//// **************************************************

				//// **************************************************
				//// (D)
				//var varSomeData8 =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby (Country.Name)
				//	select new CountryViewModel() { NewName = Country.Name }
				//	;

				//foreach (CountryViewModel oCurrentCountryViewModel in varSomeData8)
				//{
				//	System.Windows.Forms.MessageBox.Show(oCurrentCountryViewModel.NewName);
				//}
				//// **************************************************

				//// **************************************************
				//// (E)
				//// Note: متاسفانه کار نمی کند
				//var varSomeData9 =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby (Country.Name)
				//	select new Models.Country() { Name = Country.Name }
				//	;

				//foreach (Models.Country oCurrentCountry in varSomeData9)
				//{
				//	System.Windows.Forms.MessageBox.Show(oCurrentCountry.Name);
				//}
				//// **************************************************
				//// **************************************************
				//// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				var varSomeData09 =
					MyDatabaseContext.Countries
					.ToList()
					;

				// "SELECT * FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (A)
				var varSomeData10 =
					MyDatabaseContext.Countries
					.Select(current => current.Name)
					.ToList()
					;

				// "SELECT Name FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (B)
				var varSomeData11 =
					MyDatabaseContext.Countries
					.Select(current => new { current.Name })
					.ToList()
					;

				// "SELECT Name FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (C)
				var varSomeData12 =
					MyDatabaseContext.Countries
					.Select(current => new { Baghali = current.Name })
					.ToList()
					;

				// "SELECT Name FROM Countries"
				// **************************************************

				// **************************************************
				// It is similar to (D)
				var varSomeData13 =
					MyDatabaseContext.Countries
					.Select(current => new CountryViewModel() { NewName = current.Name })
					.ToList()
					;
				// **************************************************

				// **************************************************
				// It is similar to (E)
				// Note: متاسفانه کار نمی کند
				var varSomeData14 =
					MyDatabaseContext.Countries
					.Select(current => new Models.Country() { Name = current.Name })
					.ToList()
					;
				// **************************************************

				// **************************************************
				var varSomeData14_1 =
					MyDatabaseContext.Countries
					.Select(current => new { Name = current.Name })
					.ToList()
					.Select(current => new Models.Country()
					{
						Name = current.Name,
					})
					.ToList()
					;
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				var varSomeData15 =
					MyDatabaseContext.Countries
					.Select(current => new
					{
						Id = current.Id,
						Name = current.Name,
						StateCount = current.States.Count,
					});
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				var varSomeData16 =
					MyDatabaseContext.Countries
					.Select(current => new
					{
						Id = current.Id,
						Name = current.Name,
						StateCount = current.States.Count,
						CityCount = current.States.Sum(state => state.Cities.Count),
					});

				var varSomeData17 =
					MyDatabaseContext.Countries
					.Select(current => new
					{
						Id = current.Id,
						Name = current.Name,
						CityCount = current.States.Select(state => state.Cities.Count).Sum(),
					});

				var varSomeData18 =
					MyDatabaseContext.Countries
					.Select(current => new
					{
						Id = current.Id,
						Name = current.Name,
						StateCount = current.States.Count,

						CityCount = current.States.Count == 0 ? 0 :
							current.States.Select(state => new { XCount = state.Cities.Count }).Sum(x => x.XCount)

						//CityCount = current.States.Count == 0 ? 0 :
						//	current.States.Select(state => state.Cities.Count).Sum()

						//CityCount = current.States == null || current.States.Count == 0 ? 0 :
						//	current.States.Select(state => new { XCount = state.Cities == null ? 0 : state.Cities.Count }).Sum(x => x.XCount)
					})
					.ToList()
					;

				// مهدی اکبری
				var varSomeData18_1 =
					MyDatabaseContext.Countries
					.Select(current => new
					{
						Id = current.Id,
						Name = current.Name,
						StateCount = current.States.Count,

						CityCount = current.States.Select(state => state.Cities.Count).DefaultIfEmpty(0).Sum(),
					})
					.ToList()
					;
				// **************************************************
				// **************************************************
				// **************************************************

				// Group By

				var varSomeData19 =
					MyDatabaseContext.Countries
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.ToList()
					;

				var varSomeData20 =
					MyDatabaseContext.Countries
					.Where(current => current.Population >= 120000000)
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.ToList()
					;

				var varSomeData21 =
					MyDatabaseContext.Countries
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.Where(current => current.Population >= 120000000)
					.ToList()
					;

				var varSomeData22 =
					MyDatabaseContext.Countries
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.Where(current => current.Count >= 30)
					.ToList()
					;

				var varSomeData23 =
					MyDatabaseContext.Countries
					.Where(current => current.Population >= 120000000)
					.GroupBy(current => current.Population)
					.Select(current => new
					{
						Population = current.Key,

						Count = current.Count(),
					})
					.Where(current => current.Count >= 30)
					.ToList()
					;

				var varSomeData24 =
					MyDatabaseContext.Countries
					.GroupBy(current => new { current.Population, current.HeadlthyRate })
					.Select(current => new
					{
						Population = current.Key.Population,
						HeadlthyRate = current.Key.HeadlthyRate,

						Count = current.Count(),
					})
					.ToList()
					;
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			finally
			{
			}
		}

		public class CountryViewModel
		{
			public CountryViewModel()
			{
			}

			public string NewName { get; set; }
		}

		private void btnGettingSqlBeforeRunning_Click(object sender, System.EventArgs e)
		{
			Models.DatabaseContext MyDatabaseContext = null;

			try
			{
				MyDatabaseContext =
					new Models.DatabaseContext();

				//var varSomeData =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains("ايران"))
				//	orderby (Country.Name)
				//	select (new PartialCountry() { Name = Country.Name })
				//	;

				//var varSomeData =
				//	from Country in MyDatabaseContext.Countries
				//	where (Country.Name.Contains(txtCountryName.Text))
				//	orderby (Country.Name)
				//	select (new PartialCountry() { Name = Country.Name })
				//	;

				string strCountryName = txtCountryName.Text;

				var varSomeData =
					from Country in MyDatabaseContext.Countries
					where (Country.Name.Contains(strCountryName))
					orderby (Country.Name)
					select (new CountryViewModel() { NewName = Country.Name })
					;

				string strQuery = varSomeData.ToString();

				foreach (CountryViewModel oPartialCountry in varSomeData)
				{
					string strName = oPartialCountry.NewName;
				}

				var varData =
					MyDatabaseContext.Countries
					.Where(current => 1 == 1)
					;

				//var varData =
				//	MyDatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//var varData =
				//	MyDatabaseContext.Countries
				//	.Select(current => new { current.Name })
				//	;

				//var varData =
				//	MyDatabaseContext.Countries
				//	.Select(current => new PartialCountry() { Name = current.Name })
				//	;

				// بررسی شود
				//var varData =
				//	MyDatabaseContext.Countries
				//	.Select(current => current.Name)
				//	;

				varData =
					varData
					.Where(current => current.Name.Contains("ايران"))
					;

				varData =
					varData
					.OrderBy(current => current.Name)
					;

				strQuery =
					varData.ToString();

				var varResult =
					varData.ToList();
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			finally
			{
				if (MyDatabaseContext != null)
				{
					MyDatabaseContext.Dispose();
					MyDatabaseContext = null;
				}
			}
		}

		private void btnCheckDifference_Click(object sender, System.EventArgs e)
		{
			Models.DatabaseContext MyDatabaseContext = null;

			try
			{
				MyDatabaseContext =
					new Models.DatabaseContext();

				// Solution (1)
				//var varData =
				//	MyDatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//varData = varData
				//	.Where(current => current.Name == "Some Name")
				//	;

				//varData = varData
				//	.Where(current => current.Population >= 100)
				//	;

				//varData = varData
				//	.OrderBy(current => current.Name)
				//	;

				//string strQuery = varData.ToString();

				//SELECT 
				//[Extent1].[Id] AS [Id], 
				//[Extent1].[Name] AS [Name], 
				//[Extent1].[Population] AS [Population]
				//FROM [dbo].[Countries] AS [Extent1]
				//WHERE (N'Some Name' = [Extent1].[Name]) AND ([Extent1].[Population] >= 100)
				//ORDER BY [Extent1].[Name] ASC

				// /Solution (1)

				// Solution (2)
				//var varData =
				//	MyDatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//varData = varData
				//	.Where(current => current.Name == "Some Name")
				//	;

				//varData = varData
				//	.OrderBy(current => current.Name)
				//	;

				//varData = varData
				//	.Where(current => current.Population >= 100)
				//	;

				//string strQuery = varData.ToString();

				//SELECT 
				//[Extent1].[Id] AS [Id], 
				//[Extent1].[Name] AS [Name], 
				//[Extent1].[Population] AS [Population]
				//FROM [dbo].[Countries] AS [Extent1]
				//WHERE (N'Some Name' = [Extent1].[Name]) AND ([Extent1].[Population] >= 100)
				//ORDER BY [Extent1].[Name] ASC

				// /Solution (2)

				// Solution (3)
				//var varData =
				//	MyDatabaseContext.Countries
				//	.AsQueryable()
				//	;

				//varData = varData
				//	.Where(current => current.Name == "Some Name")
				//	;

				//varData = varData
				//	.OrderBy(current => current.Name)
				//	.AsQueryable()
				//	;

				//varData = varData
				//	.Where(current => current.Population >= 100)
				//	;

				//string strQuery = varData.ToString();

				//SELECT 
				//[Extent1].[Id] AS [Id], 
				//[Extent1].[Name] AS [Name], 
				//[Extent1].[Population] AS [Population]
				//FROM [dbo].[Countries] AS [Extent1]
				//WHERE (N'Some Name' = [Extent1].[Name]) AND ([Extent1].[Population] >= 100)
				//ORDER BY [Extent1].[Name] ASC

				// /Solution (3)

				// Solution (4)
				var varData =
					MyDatabaseContext.Countries
					.AsQueryable()
					;

				varData = varData
					.Where(current => current.Name.StartsWith("A"))
					;

				varData = varData
					.OrderBy(current => current.Name)
					.AsQueryable()
					;

				varData = varData
					.Where(current => current.Name.EndsWith("Z"))
					;

				varData = varData
					.OrderBy(current => current.Population)
					.AsQueryable()
					;

				varData = varData
					.Where(current => current.Population >= 100)
					;

				string strQuery = varData.ToString();

				// /Solution (4)
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			finally
			{
				if (MyDatabaseContext != null)
				{
					MyDatabaseContext.Dispose();
					MyDatabaseContext = null;
				}
			}
		}

		private void MainForm_FormClosing
			(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (_myDatabaseContext != null)
			{
				_myDatabaseContext.Dispose();
				_myDatabaseContext = null;
			}
		}
	}
}
