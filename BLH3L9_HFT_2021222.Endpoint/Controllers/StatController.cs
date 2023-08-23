using BLH3L9_HFT_2021222.Logic;
using BLH3L9_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        ICocktailLogic cl;

        public StatController(ICocktailLogic cl)
        {
            this.cl = cl;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AVGPrice()
        {
            return cl.AVGPrice();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AVGPercentage()
        {
            return cl.AVGPercentage();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> MaxPrice()
        {
            return cl.MaxPrice();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> MinPrice()
        {
            return cl.MinPrice();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> MaxPercentage()
        {
            return cl.MaxPercentage();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
