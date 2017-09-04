using System.Collections.Generic;
using CheckoutCom.ShoppingList.Controllers;
using CheckoutCom.ShoppingList.DataAccess.Base;
using CheckoutCom.ShoppingList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace CheckoutCom.ShoppingList.Tests
{
    public static class TestsHelper
    {
        public static ShoppingListController CreateController(IRepository<ShoppingListEntity> repo, Dictionary<string, string> routeData)
        {
            var controller = new ShoppingListController(repo)
            {
                ControllerContext = CreateControllerContext(routeData)
            };

            return controller;
        }

        public static IRepository<ShoppingListEntity> CreateRepo(ShoppingListEntity shoppingList)
        {
            var repoMock = new Mock<IRepository<ShoppingListEntity>>();
            repoMock.Setup(foo => foo.GetById(1)).Returns(shoppingList);

            return repoMock.Object;
        }

        private static ControllerContext CreateControllerContext(Dictionary<string, string> data)
        {
            var routeData = new RouteData();
            foreach (KeyValuePair<string, string> keyValuePair in data)
                routeData.Values.Add(keyValuePair.Key, keyValuePair.Value);

            return new ControllerContext { RouteData = routeData };
        }
    }
}