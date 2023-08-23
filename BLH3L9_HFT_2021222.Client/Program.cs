using BLH3L9_HFT_2021222.Models;
using MovieDbApp.Client;
using System;
using System.Collections.Generic;
using ConsoleTools;

namespace BLH3L9_HFT_2021222.Client
{
    internal class Program
    {
        static RestService rest;

        //ALCOHOL

        static void CreateAlcohol(string entity)
        {
            if (entity == "Alcohol")
            {
                Console.Write("Enter Alcohol Name: ");
                string name = Console.ReadLine();
                rest.Post(new Alcohol() { AlcoholName = name }, "alcohol");
            }
        }

        static void ListAlcohol(string entity)
        {
            if (entity == "Alcohol")
            {
                List<Alcohol> alcohols = rest.Get<Alcohol>("alcohol");
                foreach (var item in alcohols)
                {
                    Console.WriteLine(item.AlcoholId + ": " + item.AlcoholName);
                }
            }
            Console.ReadLine();
        }

        static void UpdateAlcohol(string entity)
        {
            if (entity == "Alcohol")
            {
                Console.Write("Enter Alcohol's Id to update: ");
                int id = int.Parse(Console.ReadLine());
                Alcohol one = rest.Get<Alcohol>(id, "alcohol");
                Console.Write($"New name [old: {one.AlcoholName}]: ");
                string name = Console.ReadLine();
                one.AlcoholName = name;
                Console.Write($"New grain [old: {one.Grain}]: ");
                string grain = Console.ReadLine();
                one.Grain = grain;
                ;
                rest.Put(one, "alcohol");
                ;
            }
        }

        static void DeleteAlcohol(string entity)
        {
            if (entity == "Alcohol")
            {
                Console.Write("Enter Alcohol's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "alcohol");
            }
        }

        //BRAND

        static void CreateBrand(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand Name: ");
                string name = Console.ReadLine();
                rest.Post(new Brand() { BrandName = name }, "brand");
            }
        }

        static void ListBrand(string entity)
        {
            if (entity == "Brand")
            {
                List<Brand> brands = rest.Get<Brand>("brand");
                foreach (var item in brands)
                {
                    Console.WriteLine(item.BrandId + ": " + item.BrandName);
                }
            }
            Console.ReadLine();
        }

        static void UpdateBrand(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand's Id to update: ");
                int id = int.Parse(Console.ReadLine());
                Brand one = rest.Get<Brand>(id, "brand");
                Console.Write($"New name [old: {one.BrandName}]: ");
                string name = Console.ReadLine();
                one.BrandName = name;
                rest.Put(one, "brand");
            }
        }

        static void DeleteBrand(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "brand");
            }
        }

        //COCKTAIL

        static void CreateCocktail(string entity)
        {
            if (entity == "Cocktail")
            {
                Console.Write("Enter Cocktail Name: ");
                string name = Console.ReadLine();
                rest.Post(new Cocktail() { CocktailName = name }, "cocktail");
            }
        }

        static void ListCocktail(string entity)
        {
            if (entity == "Cocktail")
            {
                List<Cocktail> cocktails = rest.Get<Cocktail>("cocktail");
                foreach (var item in cocktails)
                {
                    Console.WriteLine(item.CocktailId + ": " + item.CocktailName);
                }
            }
            Console.ReadLine();
        }

        static void UpdateCocktail(string entity)
        {
            if (entity == "Cocktail")
            {
                Console.Write("Enter Cocktail's Id to update: ");
                int id = int.Parse(Console.ReadLine());
                Cocktail one = rest.Get<Cocktail>(id, "cocktail");
                Console.Write($"New name [old: {one.CocktailName}]: ");
                string name = Console.ReadLine();
                one.CocktailName = name;
                rest.Put(one, "cocktail");
            }
        }

        static void DeleteCocktail(string entity)
        {
            if (entity == "Cocktail")
            {
                Console.Write("Enter Cocktail's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "cocktail");
            }
        }

        //NON-CRUD

        static void AvgPrice()
        {
            var avgprice = rest.GetSingle<IEnumerable<KeyValuePair<string, double>>>("stat/avgprice");

            foreach (var item in avgprice)
            {
                Console.WriteLine($"Average price by cocktails made with {item.Key} : {item.Value}");
            }
        }

        static void AvgPercentage()
        {
            var avgperc = rest.GetSingle<IEnumerable<KeyValuePair<string, double>>>("stat/avgpercentage");

            foreach (var item in avgperc)
            {
                Console.WriteLine($"{item.Key} : {item.Value}%");
            }
        }

        static void MaxPrice()
        {
            var maxprice = rest.GetSingle<IEnumerable<KeyValuePair<string, int>>>("stat/maxprice");

            foreach (var item in maxprice)
            {
                Console.WriteLine($"Alcohol: {item.Key}, Price: {item.Value}");
            }
        }

        static void MinPrice()
        {
            var minprice = rest.GetSingle<IEnumerable<KeyValuePair<string, double>>>("stat/minprice");

            foreach (var item in minprice)
            {
                Console.WriteLine($"Alcohol: {item.Key}, Price: {item.Value}");
            }
        }

        static void MaxPercentage()
        {
            var maxpercentage = rest.GetSingle<IEnumerable<KeyValuePair<string, double>>>("stat/maxpercentage");

            foreach (var item in maxpercentage)
            {
                Console.WriteLine($"{item.Key} : {item.Value}%");
            }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:61344/", "alcohol");

            var alcoholSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ListAlcohol("Alcohol"))
                .Add("Create", () => CreateAlcohol("Alcohol"))
                .Add("Delete", () => DeleteAlcohol("Alcohol"))
                .Add("Update", () => UpdateAlcohol("Alcohol"))
                .Add("Exit", ConsoleMenu.Close);

            var brandSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ListBrand("Brand"))
                .Add("Create", () => CreateBrand("Brand"))
                .Add("Delete", () => DeleteBrand("Brand"))
                .Add("Update", () => UpdateBrand("Brand"))
                .Add("Exit", ConsoleMenu.Close);

            var cocktailSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ListCocktail("Cocktail"))
                .Add("Create", () => CreateCocktail("Cocktail"))
                .Add("Delete", () => DeleteCocktail("Cocktail"))
                .Add("Update", () => UpdateCocktail("Cocktail"))
                .Add("Average Price", () => AvgPrice())
                .Add("Average Percentage", () => AvgPercentage())
                .Add("Maximum Price", () => MaxPrice())
                .Add("Minimum Price", () => MinPrice())
                .Add("Highest Percentage", () => MaxPercentage())               
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Alcohols", () => alcoholSubMenu.Show())
                .Add("Brands", () => brandSubMenu.Show())
                .Add("Cocktails", () => cocktailSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
