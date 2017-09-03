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
            modelBuilder.Entity<Drink>().HasKey(d => d.Id);

            modelBuilder.Entity<ShoppingListEntity>().
                HasMany(sl => sl.Drinks).
                WithOne(d => d.ShoppingList).IsRequired();
        }
    }
}
