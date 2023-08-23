using BLH3L9_HFT_2021222.Models;
using System.Linq;

namespace BLH3L9_HFT_2021222.Repository
{
    public interface IAlcoholRepository
    {
        void CreateAlcohol(Alcohol alcohol);

        Alcohol ReadAlcohol(int id);

        IQueryable<Alcohol> ReadAllAlcohol();

        void DeleteAlcohol(int id);

        void UpdateAlcohol(Alcohol alcohol);
    }
}