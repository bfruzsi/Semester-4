using BLH3L9_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Repository
{
    public class BrandRepository : IBrandRepository
    {
        AlcoholDbContext db;

        public BrandRepository(AlcoholDbContext db)
        {
            this.db = db;
        }

        public void CreateBrand(Brand brand)
        {
            db.Brands.Add(brand);
            db.SaveChanges();
        }

        public Brand ReadBrand(int id)
        {
            return db.Brands.FirstOrDefault(a => a.BrandId == id);
        }

        public IQueryable<Brand> ReadAllBrand()
        {
            return db.Brands;
        }

        public void DeleteBrand(int id)
        {
            var BrandToDelete = ReadBrand(id);
            db.Brands.Remove(BrandToDelete);
            db.SaveChanges();
        }

        public void UpdateBrand(Brand brand)
        {
            var oldbrand = ReadBrand(brand.BrandId);
            oldbrand.BrandName = brand.BrandName;
            oldbrand.Percentage = brand.Percentage;
            oldbrand.Alcohol = brand.Alcohol;
            oldbrand.AlcoholId = brand.AlcoholId;
            oldbrand.BrandId = brand.BrandId;
            db.SaveChanges();
        }
    }
}
