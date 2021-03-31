using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RH.Query.BusModel;
using RH.Query.Context;
using System;
using System.Linq;

namespace RH.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class AgendamentoDeFeriasController : ControllerBase
    {
        private readonly DatabaseQuery databaseQuery;

        public AgendamentoDeFeriasController(DatabaseQuery databaseQuery)
            => this.databaseQuery = databaseQuery;

        [HttpGet("{idContrato}")]
        public IActionResult Get(Guid idContrato)
        {
            var collection = databaseQuery.Get().GetCollection<AgendamentoDeFeriasViewModel>(AgendamentoDeFeriasViewModel.ColletionName);
            var queryable = collection.AsQueryable().Where(x => x.IdContrato == idContrato);
            return Ok(queryable.ToList());
        }
    }
}