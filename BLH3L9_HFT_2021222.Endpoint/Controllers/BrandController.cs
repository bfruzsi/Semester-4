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
    public class BrandController : ControllerBase
    {
        IBrandLogic logic;

        IHubContext<SignalRHub> hub;

        public BrandController(IBrandLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Brand> ReadAll()
        {
            return this.logic.ReadAllBrand();
        }

        [HttpGet("{id}")]
        public Brand Read(int id)
        {
            return this.logic.ReadBrand(id);
        }

        [HttpPost]
        public void Create([FromBody] Brand value)
        {
            this.logic.CreateBrand(value);
            hub.Clients.All.SendAsync("BrandCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Brand value)
        {
            this.logic.UpdateBrand(value);
            hub.Clients.All.SendAsync("BrandUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brandToDelete = this.logic.ReadBrand(id);
            this.logic.DeleteBrand(id);
            hub.Clients.All.SendAsync("BrandDeleted", brandToDelete);
        }
    }
}
