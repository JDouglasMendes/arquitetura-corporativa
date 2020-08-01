using Codeizi.Curso.RH.Query.Api.BusModel;
using Codeizi.Curso.RH.Query.Api.Context;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Codeizi.Curso.RH.Query.Api.Controllers
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