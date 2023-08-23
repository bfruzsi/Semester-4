using BLH3L9_HFT_2021222.Models;
using BLH3L9_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLH3L9_HFT_2021222.Logic
{
    public class AlcoholLogic : IAlcoholLogic
    {
        IAlcoholRepository alcoholRepo;

        IBrandRepository brandRepo;

        ICocktailRepository cocktailRepo;

        public AlcoholLogic(IAlcoholRepository alcoholRepo, IBrandRepository brandRepo, ICocktailRepository cocktailRepo)
        {
            this.alcoholRepo = alcoholRepo;
            this.brandRepo = brandRepo;
            this.cocktailRepo = cocktailRepo;
        }

        public void CreateAlcohol(Alcohol alcohol)
        {
            if (alcohol.AlcoholName.Length < 3)
            {
                throw new ArgumentException("Invalid Alcohol type");
            }
            alcoholRepo.CreateAlcohol(alcohol);
        }

        public Alcohol ReadAlcohol(int id)
        {
            return alcoholRepo.ReadAlcohol(id);
        }

        public IEnumerable<Alcohol> ReadAllAlcohol()
        {
            return alcoholRepo.ReadAllAlcohol().ToList();
        }

        public void UpdateAlcohol(Alcohol alcohol)
        {
            alcoholRepo.UpdateAlcohol(alcohol);
        }

        public void DeleteAlcohol(int id)
        {
            if (alcoholRepo.ReadAlcohol(id) == null)
            {
                throw new NullReferenceException();
            }
            else alcoholRepo.DeleteAlcohol(id);
        }
    }
}
