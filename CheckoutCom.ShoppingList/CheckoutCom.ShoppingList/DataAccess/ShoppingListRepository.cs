using CheckoutCom.ShoppingList.DataAccess.Base;

namespace CheckoutCom.ShoppingList.DataAccess
{
    public class ShoppingListRepository: Repository<Models.ShoppingListEntity>
    {
        private readonly ShoppingListContext _shoppingListContext;
        public ShoppingListRepository(ShoppingListContext shoppingListContext) : base(shoppingListContext)
        {
            _shoppingListContext = shoppingListContext;
        }
    }
}
