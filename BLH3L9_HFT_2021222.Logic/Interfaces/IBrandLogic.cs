using BLH3L9_HFT_2021222.Models;
using System.Collections.Generic;

namespace BLH3L9_HFT_2021222.Logic
{
    public interface IBrandLogic
    {
        void CreateBrand(Brand brand);
        Brand ReadBrand(int id);
        IEnumerable<Brand> ReadAllBrand();
        void UpdateBrand(Brand brand);
        void DeleteBrand(int id);
    }
}