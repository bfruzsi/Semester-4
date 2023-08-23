using BLH3L9_HFT_2021222.Models;
using System.Linq;

namespace BLH3L9_HFT_2021222.Repository
{
    public interface ICocktailRepository
    {
        void CreateCocktail(Cocktail type);

        Cocktail ReadCocktail(int id);

        IQueryable<Cocktail> ReadAllCocktail();

        void UpdateCocktail(Cocktail type);

        void DeleteCocktail(int id);
    }
}