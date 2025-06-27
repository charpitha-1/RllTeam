namespace FoodieApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class FoodieDbContext:DbContext
    {
        public FoodieDbContext(DbContextOptions<FoodieDbContext> options):base(options) { }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addressesss { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
      //  public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
       // public DbSet<Driver> Drivers { get; set; }
       public DbSet<Review> Reviews { get; set; }


}

