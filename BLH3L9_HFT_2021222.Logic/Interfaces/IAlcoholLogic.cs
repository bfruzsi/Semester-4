using BLH3L9_HFT_2021222.Models;
using System.Collections.Generic;

namespace BLH3L9_HFT_2021222.Logic
{
    public interface IAlcoholLogic
    {
        void CreateAlcohol(Alcohol alcohol);
        Alcohol ReadAlcohol(int id);
        IEnumerable<Alcohol> ReadAllAlcohol();
        void UpdateAlcohol(Alcohol alcohol);
        void DeleteAlcohol(int id);
    }
}