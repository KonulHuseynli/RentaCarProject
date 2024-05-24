using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentaCar.Models;

namespace RentaCar.DAL
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<OurFeaturedCar> ourFeaturedCars { get; set; }
        public DbSet<BookService> bookServices { get; set; }
        public DbSet <Category> Categories { get; set; }
        public DbSet<CategoryComponent> CategoryComponents { get; set; }
        public DbSet<AirPortTransfer> airPortTransfers { get; set; }    

    }
}