using InventoryModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;



namespace InventoryModels
{

    // Esta parte dio error (indicaba que no copia convertir ItemId a int (parecia ser string)
    // Original asi: (286
    // [Index(nameof(ItemId), nameof(GenreId), IsUnique=true)]
    // esta modificacion es la sugerencia de MVS    
    // Esto esta creando un indice compuesto: la suma de ItemId y GenreId
    [Microsoft.EntityFrameworkCore.Index(nameof(ItemId), nameof(GenreId), IsUnique = true)]
    [Table("ItemGenres")]  // Roll: para que cambie apropiadamente el nombre en SS (SQL Server)
    public class ItemGenre : IIdentityModel
    {
        public int Id { get; set; }
        public virtual int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
