using BLH3L9_HFT_2021222.Models;
using System.Linq;

namespace BLH3L9_HFT_2021222.Repository
{
    public interface IBrandRepository
    {
        void CreateBrand(Brand brand);

        Brand ReadBrand(int id);

        IQueryable<Brand> ReadAllBrand();

        void DeleteBrand(int id);

        void UpdateBrand(Brand brand);
    }
}