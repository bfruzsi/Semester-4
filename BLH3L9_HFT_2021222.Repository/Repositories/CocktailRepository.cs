using BLH3L9_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Repository
{
    public class CocktailRepository : ICocktailRepository
    {
        AlcoholDbContext db;
        public CocktailRepository(AlcoholDbContext db)
        {
            this.db = db;
        }

        public void CreateCocktail(Cocktail cocktail)
        {
            db.Cocktails.Add(cocktail);
            db.SaveChanges();
        }

        public Cocktail ReadCocktail(int id)
        {
            return db.Cocktails.FirstOrDefault(a => a.CocktailId == id);
        }

        public IQueryable<Cocktail> ReadAllCocktail()
        {
            return db.Cocktails;
        }

        public void UpdateCocktail(Cocktail cocktail)
        {
            var oldcocktail = ReadCocktail(cocktail.CocktailId);
            oldcocktail.CocktailName = cocktail.CocktailName;
            oldcocktail.Price = cocktail.Price;
            oldcocktail.BrandId = cocktail.BrandId;
            oldcocktail.CocktailId = cocktail.CocktailId;
            db.SaveChanges();
        }

        public void DeleteCocktail(int id)
        {
            db.Remove(ReadCocktail(id));
            db.SaveChanges();
        }
    }
}
