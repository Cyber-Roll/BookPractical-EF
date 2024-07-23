using EFCore_DbLibrary;
using InventoryHelpers;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace EFCore_Activity0302
{
    internal class Program
    {
        //        private static StreamWriter writer = new StreamWriter("ChangeTrackerInfo.txt");
        private static IConfigurationRoot _configuration;
        private static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;

        private const string _systemUserId = "2fd28110-93d0-427d-9207-d55dbca680fa";
        private const string _loggedInUserId = "e2eb8989-a81a-4151-8e86-eb95a7961da2";

        static void Main(string[] args)
        {
            //writer = new StreamWriter("ChangeTrackerInfo.txt");
            BuildOptions();
            DeleteAllItems();
            EnsureItems(); //rollcodeProgram
            UpdateItems();
            ListInventory(); //rollprint
            //writer.Close();

        }

        private static void UpdateItems()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var items = db.Items.ToList();
                foreach (var item in items)
                {
                    item.CurrentOrFinalPrice = 9.99M;
                }
                db.Items.UpdateRange(items);
                db.SaveChanges();
            }
        }

        private static void DeleteAllItems()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var items = db.Items.ToList();
                db.Items.RemoveRange(items);
                db.SaveChanges();
            }
        }


        private static void ListInventory()  //rollprint
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {

                var items = db.Items.OrderBy(x => x.Name).ToList();
                foreach (var i in items)
                {
                    // acortando los campos
                    var descrip = i.Description.Length > 20 ? i.Description.Substring(0, 20) : i.Description;
                    var notas = i.Notes.Length > 15 ? i.Notes.Substring(0, 15) : i.Notes;
                    var userid = i.CreatedByUserId.Length > 15 ? i.CreatedByUserId.Substring(0, 15) : i.CreatedByUserId;

                    //print
                    Console.WriteLine($"" +
                        $"{i.Id,-3}|" +
                        $"{i.Name,-16}|" +
                        $"{descrip,-20}|" +
                        $"{notas,-15}| " +
                        $"{i.Quantity,-3}|" +
                        $"{i.CurrentOrFinalPrice,-6}|" +
                        $"{userid,-15}|");
                }
                //items.ForEach(x => Console.WriteLine($"New Item: {x.Name}"));
            }
        }

        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }

        static void EnsureItems() //rollcodeProgram
        {
            EnsureItem("Batman", "Es un muricielago", "ta bien");
            EnsureItem("BladeRunner", "Sobre los androides", "favorita mia");
            EnsureItem("Clockwork Orange", "Algo violenta", "too favorita");
            EnsureItem("Star Wars", "Ciencia ficción", "Antigua");
            EnsureItem("Odisea 2001", "AI de 1969", "pelicula iconica");
        }
        private static void EnsureItem(string name, string descr, string notes) //rollcodeProgram
        {
            Random rd = new Random();
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                //determine if item exists:

                string s = name.ToLower();
                Item itemRoll = db.Items.FirstOrDefault();
                var existingItem = db.Items.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                if (existingItem == null)
                {
                    //doesn't exist, add it.
                    var item = new Item()
                    {
                        Name = name,
                        Description = descr,
                        Notes = notes,
                        CreatedByUserId = _loggedInUserId,
                        IsActive = true,
                        Quantity = (int)(rd.NextSingle() * 20 + 5)
                    };
                    db.Items.Add(item);
                    db.SaveChanges();
                }
            }
        }

    }
}
