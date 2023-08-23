using BLH3L9_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLH3L9_HFT_2021222.Repository
{
    public class AlcoholDbContext : DbContext
    {
        public virtual DbSet<Alcohol> Alcohols { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cocktail> Cocktails { get; set; }

        public AlcoholDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("cocktailData");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Alcohol a0 = new Alcohol() { AlcoholId = 1, AlcoholName = "whiskey", Grain = "grain" };
            Alcohol a1 = new Alcohol() { AlcoholId = 2, AlcoholName = "tequila", Grain = "agave" };
            Alcohol a2 = new Alcohol() { AlcoholId = 3, AlcoholName = "vodka", Grain = "potato" };
            Alcohol a3 = new Alcohol() { AlcoholId = 4, AlcoholName = "rum", Grain = "sugarcane" };
            Alcohol a4 = new Alcohol() { AlcoholId = 5, AlcoholName = "konyak", Grain = "grape" };
            Alcohol a5 = new Alcohol() { AlcoholId = 6, AlcoholName = "gin", Grain = "juniper" };

            // -------------------------------------------------------------------------------------------------------

            Brand b0 = new Brand() { BrandId = 1, AlcoholId = 1, BrandName = "Jack Daniel's", Percentage = 40 };
            Brand b1 = new Brand() { BrandId = 2, AlcoholId = 2, BrandName = "Don Julio", Percentage = 42 };
            Brand b2 = new Brand() { BrandId = 3, AlcoholId = 3, BrandName = "Grey Goose", Percentage = 43 };
            Brand b3 = new Brand() { BrandId = 4, AlcoholId = 4, BrandName = "Sailor Jerry", Percentage = 41 };
            Brand b4 = new Brand() { BrandId = 5, AlcoholId = 5, BrandName = "Martell VSOP", Percentage = 40 };
            Brand b5 = new Brand() { BrandId = 6, AlcoholId = 6, BrandName = "Tanqueray", Percentage = 42 };


            Cocktail c0 = new Cocktail() { CocktailId = 1, BrandId = 1, CocktailName = "Whisky Sour", Price = 2500 };
            Cocktail c1 = new Cocktail() { CocktailId = 2, BrandId = 1, CocktailName = "Old Fashioned", Price = 2500 };
            Cocktail c2 = new Cocktail() { CocktailId = 3, BrandId = 1, CocktailName = "Manhattan", Price = 2300 };
            Cocktail c3 = new Cocktail() { CocktailId = 4, BrandId = 2, CocktailName = "Margarita", Price = 2000 };
            Cocktail c4 = new Cocktail() { CocktailId = 5, BrandId = 2, CocktailName = "Tequila Sunrise", Price = 1800 };
            Cocktail c5 = new Cocktail() { CocktailId = 6, BrandId = 3, CocktailName = "Bloody Mary", Price = 2600 };
            Cocktail c6 = new Cocktail() { CocktailId = 7, BrandId = 3, CocktailName = "White Russian", Price = 2300 };
            Cocktail c7 = new Cocktail() { CocktailId = 8, BrandId = 3, CocktailName = "Vodka Martini", Price = 3000 };
            Cocktail c8 = new Cocktail() { CocktailId = 9, BrandId = 4, CocktailName = "Daiquiri", Price = 2400 };
            Cocktail c9 = new Cocktail() { CocktailId = 10, BrandId = 4, CocktailName = "Hurricane", Price = 3000 };
            Cocktail c10 = new Cocktail() { CocktailId = 11, BrandId = 4, CocktailName = "Mojito", Price = 1800 };
            Cocktail c11 = new Cocktail() { CocktailId = 12, BrandId = 5, CocktailName = "Sidecar", Price = 2600 };
            Cocktail c12 = new Cocktail() { CocktailId = 13, BrandId = 5, CocktailName = "Sazerac", Price = 2600 };
            Cocktail c13 = new Cocktail() { CocktailId = 14, BrandId = 5, CocktailName = "French 75", Price = 2200 };
            Cocktail c14 = new Cocktail() { CocktailId = 15, BrandId = 6, CocktailName = "Vesper Martini", Price = 3200 };
            Cocktail c15 = new Cocktail() { CocktailId = 16, BrandId = 7, CocktailName = "Singapore Sling", Price = 3000 };
            Cocktail c16 = new Cocktail() { CocktailId = 17, BrandId = 8, CocktailName = "Gin Fizz", Price = 1700 };

            // -------------------------------------------------------------------------------------------------------

            a0.AlcoholId = b0.AlcoholId;
            a1.AlcoholId = b1.AlcoholId;
            a2.AlcoholId = b2.AlcoholId;
            a3.AlcoholId = b3.AlcoholId;
            a4.AlcoholId = b4.AlcoholId;
            a5.AlcoholId = b5.AlcoholId;

            c0.BrandId = b0.BrandId;
            c1.BrandId = b0.BrandId;
            c2.BrandId = b0.BrandId;
            c3.BrandId = b1.BrandId;
            c4.BrandId = b1.BrandId;
            c5.BrandId = b2.BrandId;
            c6.BrandId = b2.BrandId;
            c7.BrandId = b2.BrandId;
            c8.BrandId = b3.BrandId;
            c9.BrandId = b3.BrandId;
            c10.BrandId = b3.BrandId;
            c11.BrandId = b4.BrandId;
            c12.BrandId = b4.BrandId;
            c13.BrandId = b4.BrandId;
            c14.BrandId = b5.BrandId;
            c15.BrandId = b5.BrandId;
            c16.BrandId = b5.BrandId;

            // -------------------------------------------------------------------------------------------------------

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasOne(brand => brand.Alcohol)
                    .WithMany(alcohol => alcohol.Brands)
                    .HasForeignKey(brand => brand.AlcoholId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Cocktail>(entity =>
            {
                entity.HasOne(cocktails => cocktails.Brand)
                    .WithMany(brand => brand.Cocktails)
                    .HasForeignKey(cocktails => cocktails.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
                        
            modelBuilder.Entity<Alcohol>().HasData(a0, a1, a2, a3, a4, a5);
            modelBuilder.Entity<Brand>().HasData(b0, b1, b2, b3, b4, b5);
            modelBuilder.Entity<Cocktail>().HasData(c0, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16);
        }
    }
}
