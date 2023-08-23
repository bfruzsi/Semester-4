using BLH3L9_HFT_2021222.Logic;
using BLH3L9_HFT_2021222.Models;
using BLH3L9_HFT_2021222.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLH3L9_HFT_2021222.Test
{
    [TestFixture]
    public class CocktailLogicTest
    {
        private CocktailLogic CocktailLogic { get; set; }

        private AlcoholLogic AlcoholLogic { get; set; }

        private BrandLogic BrandLogic { get; set; }

        [SetUp]
        public void Setup()
        {
            Mock<IAlcoholRepository> mockedAlcoholRepo = new Mock<IAlcoholRepository>();

            Mock<ICocktailRepository> mockedCocktailRepo = new Mock<ICocktailRepository>();

            Mock<IBrandRepository> mockedBrandRepo = new Mock<IBrandRepository>();

            mockedCocktailRepo.Setup(x => x.ReadCocktail(It.IsAny<int>())).Returns(
                new Cocktail
                {
                    CocktailId = 1,
                    CocktailName = "mojito",
                    Price = 2500
                });
            mockedAlcoholRepo.Setup(x => x.ReadAlcohol(It.IsAny<int>())).Returns(
                new Alcohol
                {
                    AlcoholId = 1,
                    AlcoholName = "whisky",
                    Grain = "corn"
                });
            mockedBrandRepo.Setup(x => x.ReadBrand(It.IsAny<int>())).Returns(
                new Brand
                {
                    BrandId = 1,
                    BrandName = "Valami",
                    Percentage = 45
                });

            mockedAlcoholRepo.Setup(x => x.ReadAllAlcohol()).Returns(this.FakeAlcoholObjects);
            mockedBrandRepo.Setup(x => x.ReadAllBrand()).Returns(this.FakeBrandObjects);
            mockedCocktailRepo.Setup(x => x.ReadAllCocktail()).Returns(this.FakeCocktailObjects);

            this.CocktailLogic = new CocktailLogic(mockedCocktailRepo.Object, mockedAlcoholRepo.Object, mockedBrandRepo.Object);
            this.AlcoholLogic = new AlcoholLogic(mockedAlcoholRepo.Object, mockedBrandRepo.Object, mockedCocktailRepo.Object);
            this.BrandLogic = new BrandLogic(mockedBrandRepo.Object);
        }

        //---------------NON CRUD TESTS------------------

        [Test]
        public void AVGPercentageTest()
        {

            var r = CocktailLogic.AVGPercentage();
            var e = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("whiskey", 40),
                new KeyValuePair<string, double>("tequila", 42)
            };
            Assert.That(r, Is.EqualTo(e));
        }

        [Test]
        public void AVGPriceTest()
        {
            var r = CocktailLogic.AVGPrice();
            var e = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("Jack Daniel's", 2600),
                new KeyValuePair<string, double>("Don Julio", 2000)
            };
            Assert.That(r, Is.EqualTo(e));
        }

        [Test]
        public void MaxPercentageTest()
        {
            var r = CocktailLogic.MaxPercentage();
            var e = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("whiskey", 40),
                new KeyValuePair<string, int>("tequila", 42)
            };
            Assert.That(r, Is.EqualTo(e));
        }

        [Test]
        public void MaxPriceTest()
        {
            var r = CocktailLogic.MaxPrice();
            var e = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Jack Daniel's", 2700),
                new KeyValuePair<string, int>("Don Julio", 2000)
            };
            Assert.That(r, Is.EqualTo(e));
        }

        [Test]
        public void MinPriceTest()
        {
            var r = CocktailLogic.MinPrice();
            var e = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Jack Daniel's", 2500),
                new KeyValuePair<string, int>("Don Julio", 2000)
            };
            Assert.That(r, Is.EqualTo(e));
        }

        //---------------CREATE TESTS----------------

        [Test]
        public void CocktailCreateTest()
        {
            Cocktail c = new Cocktail();
            c.CocktailName = "";
            Assert.That(() => CocktailLogic.CreateCocktail(c), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void AlcoholCreateTest()
        {
            Alcohol a = new Alcohol();
            a.AlcoholName = "ac";
            Assert.That(() => AlcoholLogic.CreateAlcohol(a), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void BrandCreateTest()
        {
            Brand b = new Brand();
            b.BrandName = "";
            Assert.That(() => BrandLogic.CreateBrand(b), Throws.TypeOf<ArgumentException>());
        }

        //---------------------------OTHER TESTS------------------------------------

        [Test]
        public void CreateCocktailTest_WithWwrongId()
        {
            Cocktail c = new Cocktail();
            c.CocktailId = 0;
            Assert.That(() => CocktailLogic.CreateCocktail(c), Throws.TypeOf<Exception>());
        }

        [Test]
        public void UpdateCocktailTest()
        {
            Cocktail c = new Cocktail();
            c.CocktailId = -1;
            Assert.That(() => CocktailLogic.UpdateCocktail(c), Throws.TypeOf<Exception>());
        }


        private IQueryable<Alcohol> FakeAlcoholObjects()
        {
            Alcohol a0 = new Alcohol() { AlcoholId = 1, AlcoholName = "whiskey", Grain = "grain" };
            Alcohol a1 = new Alcohol() { AlcoholId = 2, AlcoholName = "tequila", Grain = "agave" };

            a0.Brands = new List<Brand>();
            a1.Brands = new List<Brand>();

            // -------------------------------------------------------------------------------------------------------

            Brand b0 = new Brand() { BrandId = 1, BrandName = "Jack Daniel's", AlcoholId = 1, Percentage = 40 };
            Brand b1 = new Brand() { BrandId = 2, AlcoholId = 2, BrandName = "Don Julio", Percentage = 42 };

            // -------------------------------------------------------------------------------------------------------

            b0.AlcoholId = a0.AlcoholId; a0.Brands.Add(b0);
            b1.AlcoholId = a1.AlcoholId; a1.Brands.Add(b1);

            List<Alcohol> items = new List<Alcohol>();

            items.Add(a0);
            items.Add(a1);

            return items.AsQueryable();
        }

        private IQueryable<Brand> FakeBrandObjects()
        {
            Alcohol a0 = new Alcohol() { AlcoholId = 1, AlcoholName = "whiskey", Grain = "grain" };
            Alcohol a1 = new Alcohol() { AlcoholId = 2, AlcoholName = "tequila", Grain = "agave" };

            a0.Brands = new List<Brand>();
            a1.Brands = new List<Brand>();

            // -------------------------------------------------------------------------------------------------------

            Brand b0 = new Brand() { BrandId = 1, BrandName = "Jack Daniel's", AlcoholId = 1, Percentage = 40, Alcohol = a0 };
            Brand b1 = new Brand() { BrandId = 2, AlcoholId = 2, BrandName = "Don Julio", Percentage = 42, Alcohol = a1 };

            b0.AlcoholId = a0.AlcoholId; a0.Brands.Add(b0);
            b1.AlcoholId = a1.AlcoholId; a1.Brands.Add(b1);


            List<Brand> items = new List<Brand>();

            items.Add(b0);
            items.Add(b1);

            return items.AsQueryable();
        }

        private IQueryable<Cocktail> FakeCocktailObjects()
        {
            Brand b0 = new Brand() { BrandId = 1, BrandName = "Jack Daniel's", AlcoholId = 1, Percentage = 40 };
            Brand b1 = new Brand() { BrandId = 2, AlcoholId = 2, BrandName = "Don Julio", Percentage = 42 };

            b0.Cocktails = new List<Cocktail>();
            b1.Cocktails = new List<Cocktail>();

            Cocktail c0 = new Cocktail() { BrandId = 1, CocktailName = "Whisky sour", CocktailId = 1, Price = 2500, Brand = b0 };
            Cocktail c1 = new Cocktail() { CocktailId = 2, BrandId = 2, CocktailName = "Margarita", Price = 2000, Brand = b1 };
            Cocktail c2 = new Cocktail() { CocktailId = 3, BrandId = 1, CocktailName = "Old fashioned", Price = 2700, Brand = b0 };

            c0.BrandId = b0.BrandId;
            c1.BrandId = b1.BrandId;
            c2.BrandId = b0.BrandId;

            List<Cocktail> items = new List<Cocktail>();

            items.Add(c0);
            items.Add(c1);
            items.Add(c2);

            return items.AsQueryable();
        }
    }
}
