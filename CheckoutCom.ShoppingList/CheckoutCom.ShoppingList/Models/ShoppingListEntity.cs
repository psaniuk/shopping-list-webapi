using System.Collections.Generic;

namespace CheckoutCom.ShoppingList.Models
{
    public class ShoppingListEntity: Entity
    {
        private const string DefaultListName = "Default";
        public const int DefaultListId = 1;

        public string Name { get; set; } = string.Empty;
        public ICollection<Drink> Drinks { get; set; } = new List<Drink>();
        
        public static ShoppingListEntity Default => new ShoppingListEntity
        {
            Id = DefaultListId,
            Name = DefaultListName
        };
    }
}
