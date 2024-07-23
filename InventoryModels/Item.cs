using System.ComponentModel.DataAnnotations;

namespace InventoryModels
{
    public class Item : FullAuditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(InventoryModelsConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        [Range(InventoryModelsConstants.MINIMUM_QUANTITY, InventoryModelsConstants.MAXIMUM_QUANTITY)]
        public int Quantity { get; set; }

        [StringLength(InventoryModelsConstants.MAX_DESCRIPTION_LENGTH)]
        public string? Description { get; set; }

        [StringLength(InventoryModelsConstants.MAX_NOTES_LENGTH, MinimumLength = 10)]
        public string? Notes { get; set; }

        public bool IsOnSale { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public DateTime? SoldDate { get; set; }

        [Range(InventoryModelsConstants.MINIMUM_PRICE, InventoryModelsConstants.MAXIMUM_PRICE)]
        public decimal? PurchasePrice { get; set; }

        [Range(InventoryModelsConstants.MINIMUM_PRICE, InventoryModelsConstants.MAXIMUM_PRICE)]
        public decimal? CurrentOrFinalPrice { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        // roll: R n-n (Actividad 5-2)
        public virtual List<Player> Players { get; set; } = new List<Player>();

        // Roll: R n-n (Activity 5-3)
        public virtual List<ItemGenre> ItemGenres { get; set; } = new List<ItemGenre>();

    }
}
