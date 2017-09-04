using System;
using System.Collections.Generic;
using System.Text;
using CheckoutCom.ShoppingList.Controllers;
using CheckoutCom.ShoppingList.DataAccess;
using CheckoutCom.ShoppingList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CheckoutCom.ShoppingList.Tests
{
    public class ShoppingListControllerTests
    {
        [Fact]
        public void given_default_list_post_drink_assume_added()
        {
            ShoppingListRepository repo = CreateRepository();
            var controller = new ShoppingListController(repo);
            
            controller.Post(new Drink {Name = "pepse", Quantity = 1});
            controller.Post(new Drink { Name = "pepse", Quantity = 1 });
            ShoppingListEntity shoppingList = repo.GetById(ShoppingListEntity.DefaultListId);
            Assert.True(shoppingList.Drinks.Count == 1);
        }

        private ShoppingListRepository CreateRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShoppingListContext>();
            optionsBuilder.UseInMemoryDatabase("TestMemoryDb");
            return new ShoppingListRepository(new ShoppingListContext(optionsBuilder.Options));
        }
    }
}
