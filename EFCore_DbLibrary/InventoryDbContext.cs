using InventoryModels;
using LibLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore_DbLibrary

{
    public class InventoryDbContext : DbContext
    {

        private Log oLog = new Log(efNamespace.Dir, efNamespace.File);

        //Roll: adicionaesto
        private static IConfigurationRoot _configuration;
        private const string _systemUserId = "2fd28110-93d0-427d-9207-d55dbca680fa";

        public DbSet<Item> Items { get; set; }  // adicionó Roll
                                                //Add a default constructor if scaffolding is needed
        public InventoryDbContext()
        {
            // no es llamado hasta ahora
            oLog.Add("InventoryDbContext()");
        }
        //Add the complex constructor for allowing Dependency Injection
        public InventoryDbContext(DbContextOptions options) : base(options)
        {

            oLog.Add("InventoryDbContext(DbContextOptions options) : base(options)");
            //writer = new StreamWriter("ChangeTrackerInfo.txt");
            //intentionally empty.
        }

        /*EsteCodigo*/
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=InventoryManagerDb; Trusted_Connection = True");
        //    }
        //}
        /*EsteCodigo*/

        //Roll: adicionaesto

        /// <summary>
        /// Es llamada automaticamente por el sistema, seguramente porque tienen una seríe de parametros
        /// que pueden desactualizarse, por ello se ajusta nuevamente los parametros de operación para EF.
        /// Hasta ahora ninguna llamada entra al if(!optionsBuilder...), porque la configuración esta
        /// activa.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            oLog.Add("OnConfiguring(DbContextOptionsBuilder optionsBuilder)");
            if (!optionsBuilder.IsConfigured)
            {
                oLog.Add("*** OnConfiguring(DbContextOptionsBuilder optionsBuilder), if (!optionsBuilder.IsConfigured)");
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                reloadOnChange: true);
                _configuration = builder.Build();
                var cnstr = _configuration.GetConnectionString("InventoryManager");
                optionsBuilder.UseSqlServer(cnstr);
            }
        }



        /*public override int SaveChanges()
        {
            var tracker = ChangeTracker;
            {
                foreach (var entry in tracker.Entries())
                {
                    oLog.Add($"   Entrada: {entry.ToString()}, {entry.Entity} tiene estado {entry.State}");
                    System.Diagnostics.Debug.WriteLine($"Valor de la entrada: {entry.ToString()}");
                    System.Diagnostics.Debug.WriteLine($"{entry.Entity} has state {entry.State}");
                }
            }
            return base.SaveChanges();
        }
        */
        public override int SaveChanges()
        {
            var tracker = ChangeTracker;
            foreach (var entry in tracker.Entries())
            {
                if (entry.Entity is FullAuditModel)
                {
                    var referenceEntity = entry.Entity as FullAuditModel;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            //ToDo Conocer mejor Change Tracking Entity Framework
                            referenceEntity.CreatedDate = DateTime.Now;
                            if (string.IsNullOrWhiteSpace(referenceEntity.CreatedByUserId))
                            {
                                referenceEntity.CreatedByUserId = _systemUserId;
                            }
                            break;
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            referenceEntity.LastModifiedDate = DateTime.Now;
                            if (string.IsNullOrWhiteSpace(referenceEntity.LastModifiedUserId))
                            {
                                referenceEntity.LastModifiedUserId = _systemUserId;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }

}
