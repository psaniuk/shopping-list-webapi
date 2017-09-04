using CheckoutCom.ShoppingList.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckoutCom.ShoppingList.DataAccess
{
    public class ShoppingListContext: DbContext
    {
        public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingListEntity> ShoppingLists { get; set; }
        public DbSet<Drink> Drinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingListEntity>().HasKey(sl => sl.Id);
            modelBuilder.Entity<Drink>(drink =>
            {
                drink.HasKey(d => d.Id);
                drink.HasOne(d => d.ShoppingList).
                    WithMany(sl => sl.Drinks).
                    HasForeignKey(d => d.ShoppingListId);
            });
        }
    }
}
