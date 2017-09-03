
namespace CheckoutCom.ShoppingList.Models
{
    public class Drink: Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public ShoppingListEntity ShoppingList { get; set; }
    }
}
