using System.Collections.Generic;
using System.Linq;
using CheckoutCom.ShoppingList.Controllers;
using CheckoutCom.ShoppingList.Models;
using Xunit;

namespace CheckoutCom.ShoppingList.Tests
{
    public class ShoppingListControllerTests
    {
        [Fact]
        public void given_default_list_post_pepsi_assume_added()
        {
            ShoppingListEntity shoppingList = ShoppingListEntity.Default;
            var routeData = new Dictionary<string, string>{ {"id", "1"} };

            ShoppingListController controller = TestsHelper.CreateController(TestsHelper.CreateRepo(shoppingList), routeData);
            controller.Post(new Drink {Name = "pepsi", Quantity = 1});
            
            Assert.Equal("pepsi", shoppingList.Drinks.ToArray()[0].Name);
        }

        [Fact]
        public void given_default_list_post_pepsi_twice_assume_drinks_count_equals_1()
        {
            ShoppingListEntity shoppingList = ShoppingListEntity.Default;
            var routeData = new Dictionary<string, string> { { "id", "1" } };

            ShoppingListController controller = TestsHelper.CreateController(TestsHelper.CreateRepo(shoppingList), routeData);
            controller.Post(new Drink { Name = "pepsi", Quantity = 1 });
            controller.Post(new Drink { Name = "pepsi", Quantity = 1 });

            Assert.Equal(1, shoppingList.Drinks.Count);
        }

        [Fact]
        public void given_default_list_post_pepsi_and_cola_assume_drinks_count_equals_2()
        {
            ShoppingListEntity shoppingList = ShoppingListEntity.Default;
            var routeData = new Dictionary<string, string> { { "id", "1" } };

            ShoppingListController controller = TestsHelper.CreateController(TestsHelper.CreateRepo(shoppingList), routeData);
            controller.Post(new Drink { Name = "pepsi", Quantity = 1 });
            controller.Post(new Drink { Name = "cola", Quantity = 1 });

            Assert.Equal(2, shoppingList.Drinks.Count);
        }

        [Fact]
        public void given_list_with_3_drinks_execute_get_assume_drinks_are_in_result()
        {
            ShoppingListEntity shoppingList = ShoppingListEntity.Default;
            shoppingList.Drinks.Add(new Drink
            {
                Id = 1,
                Name = "Pepsi",
                Quantity = 1
            });

            shoppingList.Drinks.Add(new Drink
            {
                Id = 2,
                Name = "Cola",
                Quantity = 1
            });

            shoppingList.Drinks.Add(new Drink
            {
                Id = 3,
                Name = "Fritz Cola",
                Quantity = 1
            });

            var routeData = new Dictionary<string, string> { { "id", "1" } };
            ShoppingListController controller = TestsHelper.CreateController(TestsHelper.CreateRepo(shoppingList), routeData);
            IEnumerable<Drink> result = controller.Get();

            Drink[] drinks = result.ToArray();

            Assert.Equal("Pepsi", drinks[0].Name);
            Assert.Equal("Cola", drinks[1].Name);
            Assert.Equal("Fritz Cola", drinks[2].Name);
        }

        [Fact]
        public void given_list_with_single_pepsi_execute_put_with_quantity_3_assume_pepsi_quantity_equals_3()
        {
            ShoppingListEntity shoppingList = ShoppingListEntity.Default;
            var pepsi = new Drink
            {
                Id = 1,
                Name = "Pepsi",
                Quantity = 1
            };

            shoppingList.Drinks.Add(pepsi);

            var routeData = new Dictionary<string, string> { { "id", "1" } };

            ShoppingListController controller = TestsHelper.CreateController(TestsHelper.CreateRepo(shoppingList), routeData);
            controller.Put(new Drink
            {
                Id = 1,
                Name = "pepsi",
                Quantity = 2
            });

            Assert.Equal(2, pepsi.Quantity);
        }
    }
}
