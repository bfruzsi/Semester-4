using BLH3L9_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Repository
{
    public class AlcoholRepository : IAlcoholRepository
    {
        AlcoholDbContext db;

        public AlcoholRepository(AlcoholDbContext db)
        {
            this.db = db;
        }

        public void CreateAlcohol(Alcohol alcohol)
        {
            db.Alcohols.Add(alcohol);
            db.SaveChanges();
        }

        public Alcohol ReadAlcohol(int id)
        {
            return db.Alcohols.FirstOrDefault(a => a.AlcoholId == id);
        }

        public IQueryable<Alcohol> ReadAllAlcohol()
        {
            return db.Alcohols;
        }

        public void DeleteAlcohol(int id)
        {
            db.Remove(ReadAlcohol(id));
            db.SaveChanges();
        }

        public void UpdateAlcohol(Alcohol alcohol)
        {
            var oldalcohol = ReadAlcohol(alcohol.AlcoholId);
            oldalcohol.AlcoholId = alcohol.AlcoholId;
            oldalcohol.Grain = alcohol.Grain;
            oldalcohol.AlcoholName = alcohol.AlcoholName;
            db.SaveChanges();
        }
    }
}
