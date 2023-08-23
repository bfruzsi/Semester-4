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
    public class AlcoholController : ControllerBase
    {
        IAlcoholLogic logic;

        IHubContext<SignalRHub> hub;

        public AlcoholController(IAlcoholLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Alcohol> ReadAll()
        {
            return this.logic.ReadAllAlcohol();
        }

        [HttpGet("{id}")]
        public Alcohol Read(int id)
        {
            return this.logic.ReadAlcohol(id);
        }

        [HttpPost]
        public void Create([FromBody] Alcohol value)
        {
            this.logic.CreateAlcohol(value);
            hub.Clients.All.SendAsync("AlcoholCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Alcohol value)
        {
            this.logic.UpdateAlcohol(value);
            hub.Clients.All.SendAsync("AlcoholUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var alcoholToDelete = this.logic.ReadAlcohol(id);
            this.logic.DeleteAlcohol(id);
            hub.Clients.All.SendAsync("AlcoholDeleted", alcoholToDelete);
        }
    }
}
