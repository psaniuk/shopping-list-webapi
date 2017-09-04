using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CheckoutCom.ShoppingList.DataAccess.Base;
using CheckoutCom.ShoppingList.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutCom.ShoppingList.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {
        private readonly IRepository<ShoppingListEntity> _repository;

        public ShoppingListController(IRepository<ShoppingListEntity> repository)
        {
            _repository = repository;

            ShoppingListEntity defaultShoppingList = _repository.GetById(ShoppingListEntity.DefaultListId);
            if (defaultShoppingList == null)
            {
                defaultShoppingList = ShoppingListEntity.Default;
                _repository.Insert(defaultShoppingList);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<Drink> Get() => _repository.GetById(ShoppingListEntity.DefaultListId).Drinks;
        
        [HttpGet]
        [Route("{id}/drinks", Name = "GetByName")]
        public IActionResult Get([FromQuery]string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest();

            ShoppingListEntity shoppingList = GetShoppingList();
            if (shoppingList == null)
                return BadRequest();

            Drink drink = shoppingList.Drinks.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (drink == null)
                return NotFound();

            return Ok(drink);
        }

        [HttpPost]
        [Route("{id}/drinks")]
        public IActionResult Post([FromBody]Drink drink)
        {
            if (string.IsNullOrWhiteSpace(drink?.Name) || drink.Quantity == 0)
                return BadRequest();

            ShoppingListEntity shoppingList = GetShoppingList();
            if (shoppingList == null)
                return NotFound();

            if (shoppingList.Drinks.Any(d => string.Equals(d.Name, drink.Name, StringComparison.OrdinalIgnoreCase)))
                return new StatusCodeResult((int)HttpStatusCode.Conflict);

            drink.ShoppingList = shoppingList;
            shoppingList.Drinks.Add(drink);
            _repository.Update(shoppingList);

            return CreatedAtRoute("GetByName", new {Id = shoppingList.Id, Name= drink.Name}, drink);
        }

        [HttpPut]
        [Route("drinks")]
        public IActionResult Put([FromBody]Drink drink)
        {
            if (drink?.Id == null)
                return BadRequest();

            ShoppingListEntity shoppingList = _repository.GetById(drink.ShoppingListId);
            if (shoppingList == null)
                return NotFound();

            Drink listDrink = shoppingList.Drinks.FirstOrDefault(d => d.Id == drink.Id);
            if (listDrink == null)
                return NotFound();

            listDrink.Quantity = drink.Quantity;
            _repository.Update(shoppingList);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}/drinks/{drinkId}")]
        public IActionResult Delete(int drinkId)
        {
            ShoppingListEntity shoppingList = GetShoppingList();
            if (shoppingList == null)
                return NotFound();

            Drink drinkToDelete = shoppingList.Drinks.FirstOrDefault(d => d.Id == drinkId);
            if (drinkToDelete == null)
                return BadRequest();

            shoppingList.Drinks.Remove(drinkToDelete);
            _repository.Update(shoppingList);

            return Ok();
        }

        private ShoppingListEntity GetShoppingList() =>
            RouteData.Values.TryGetValue("id", out object listIdValue) && int.TryParse(listIdValue?.ToString(), out int listid) ? _repository.GetById(listid) : null;
     }
}
