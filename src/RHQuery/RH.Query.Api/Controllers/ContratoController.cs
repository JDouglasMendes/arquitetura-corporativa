using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RH.Query.BusModel;
using RH.Query.Context;
using System.Collections.Generic;
using System.Linq;

namespace RH.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class ContratoController : ControllerBase
    {
        private readonly DatabaseQuery databaseQuery;

        public ContratoController(DatabaseQuery databaseQuery)
            => this.databaseQuery = databaseQuery;

        [HttpGet]
        public ActionResult<IEnumerable<ContratoQueryViewModel>> Get()
        {
            var collection = databaseQuery.Get().GetCollection<ContratoQueryViewModel>(ContratoQueryViewModel.ColletionName);
            var queryable = collection.AsQueryable();
            return Ok(queryable.ToList());
        }
    }
}