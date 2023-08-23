using BLH3L9_HFT_2021222.Logic;
using BLH3L9_HFT_2021222.Models;
using F5G5IN_HFT_2021221.Endpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CocktailController : ControllerBase
    {

        ICocktailLogic logic;

        IHubContext<SignalRHub> hub;

        public CocktailController(ICocktailLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Cocktail> ReadAll()
        {
            return this.logic.ReadAllCocktail();
        }

        [HttpGet("{id}")]
        public Cocktail Read(int id)
        {
            return this.logic.ReadCocktail(id);
        }

        [HttpPost]
        public void Create([FromBody] Cocktail value)
        {
            this.logic.CreateCocktail(value);
            hub.Clients.All.SendAsync("CocktailCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Cocktail value)
        {
            this.logic.UpdateCocktail(value);
            hub.Clients.All.SendAsync("CocktailUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cocktailToDelete = this.logic.ReadCocktail(id);
            this.logic.DeleteCocktail(id);
            hub.Clients.All.SendAsync("CocktailDeleted", cocktailToDelete);
        }
    }
}
