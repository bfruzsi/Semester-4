using BLH3L9_HFT_2021222.Models;
using BLH3L9_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Logic
{
    public class CocktailLogic : ICocktailLogic
    {
        ICocktailRepository cocktailRepo;

        IAlcoholRepository alcoholRepo;

        IBrandRepository brandRepo;

        public CocktailLogic(ICocktailRepository cocktailRepo, IAlcoholRepository alcoholRepo, IBrandRepository brandRepo)
        {
            this.cocktailRepo = cocktailRepo;
            this.alcoholRepo = alcoholRepo;
            this.brandRepo = brandRepo;
        }
        public void CreateCocktail(Cocktail cocktail)
        {
            if (cocktail.CocktailName != "")
                cocktailRepo.CreateCocktail(cocktail);
            else throw new ArgumentException("Please provide a valid name for the cocktail.");
            if (cocktail.CocktailId < 1)
            {
                throw new Exception("Please provide a number bigger than 0.");
            }
        }

        public Cocktail ReadCocktail(int id)
        {
            return cocktailRepo.ReadCocktail(id);
        }

        public IEnumerable<Cocktail> ReadAllCocktail()
        {
            return cocktailRepo.ReadAllCocktail().ToList();
        }

        public void UpdateCocktail(Cocktail cocktail)
        {
            if (cocktail.CocktailId < 1)
            {
                throw new Exception("Please, provide a valid ID.");
            }
            cocktailRepo.UpdateCocktail(cocktail);
        }

        public void DeleteCocktail(int id)
        {
            if (cocktailRepo.ReadCocktail(id) == null)
            {
                throw new NullReferenceException();
            }
            else cocktailRepo.DeleteCocktail(id);
        }

        //-------------------------------NON-CRUD METHODS----------------------------     

        public IEnumerable<KeyValuePair<string, double>> AVGPrice()
        {
            return from x in cocktailRepo.ReadAllCocktail()
                   group x by x.Brand.BrandName into g
                   select new KeyValuePair<string, double>
                   (g.Key, Math.Round(g.Average(c => c.Price)));
        }

        public IEnumerable<KeyValuePair<string, double>> AVGPercentage()
        {
            return from x in brandRepo.ReadAllBrand()
                   group x by x.Alcohol.AlcoholName into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(a => a.Percentage));
        }

        public IEnumerable<KeyValuePair<string, int>> MaxPrice()
        {
            return from x in cocktailRepo.ReadAllCocktail()
                   group x by x.Brand.BrandName into g
                   select new KeyValuePair<string, int>
                   (g.Key, g.Max(c => c.Price));
        }

        public IEnumerable<KeyValuePair<string, int>> MaxPrice2()
        {
            return from x in cocktailRepo.ReadAllCocktail()
                   group x by x.CocktailName into g
                   select new KeyValuePair<string, int>
                   (g.Key, g.Max(c => c.Price));
        }

        public IEnumerable<KeyValuePair<string, int>> MinPrice()
        {
            return from x in cocktailRepo.ReadAllCocktail()
                   group x by x.Brand.BrandName into g
                   select new KeyValuePair<string, int>
                   (g.Key, g.Min(c => c.Price));
        }

        public IEnumerable<KeyValuePair<string, int>> MaxPercentage()
        {
            return from x in brandRepo.ReadAllBrand()
                   group x by x.Alcohol.AlcoholName into g
                   select new KeyValuePair<string, int>
                   (g.Key, g.Min(b => b.Percentage));
        }
    }
}
