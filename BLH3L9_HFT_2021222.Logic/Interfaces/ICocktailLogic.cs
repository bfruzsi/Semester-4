using BLH3L9_HFT_2021222.Models;
using System.Collections.Generic;

namespace BLH3L9_HFT_2021222.Logic
{
    public interface ICocktailLogic
    {
        void CreateCocktail(Cocktail cocktail);

        Cocktail ReadCocktail(int id);

        IEnumerable<Cocktail> ReadAllCocktail();

        void UpdateCocktail(Cocktail cocktail);

        void DeleteCocktail(int id);

        //----------NON CRUDS------------------

        IEnumerable<KeyValuePair<string, int>> MaxPrice();

        IEnumerable<KeyValuePair<string, int>> MinPrice();

        IEnumerable<KeyValuePair<string, int>> MaxPercentage();

        IEnumerable<KeyValuePair<string, double>> AVGPercentage();

        IEnumerable<KeyValuePair<string, double>> AVGPrice();
    }
}