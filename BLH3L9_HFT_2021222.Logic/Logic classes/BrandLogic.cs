using BLH3L9_HFT_2021222.Models;
using BLH3L9_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Logic
{
    public class BrandLogic : IBrandLogic
    {
        IBrandRepository brandRepo;

        public BrandLogic(IBrandRepository brandRepo)
        {
            this.brandRepo = brandRepo;
        }

        public void CreateBrand(Brand brand)
        {
            if (brand.BrandName != "")
                brandRepo.CreateBrand(brand);
            else throw new ArgumentException("Please provide a valid brand name.");
        }

        public Brand ReadBrand(int id)
        {
            return brandRepo.ReadBrand(id);
        }

        public IEnumerable<Brand> ReadAllBrand()
        {
            return brandRepo.ReadAllBrand().ToList();
        }

        public void UpdateBrand(Brand brand)
        {
            brandRepo.UpdateBrand(brand);
        }

        public void DeleteBrand(int id)
        {
            if (brandRepo.ReadBrand(id) == null)
            {
                throw new NullReferenceException();
            }
            else brandRepo.DeleteBrand(id);
        }
    }
}
