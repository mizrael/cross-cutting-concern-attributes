using System;
using System.Threading.Tasks;
using cross_cutting_concern_attributes.Extensions;
using cross_cutting_concern_attributes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cross_cutting_concern_attributes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ValuesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new ValuesArchive());
            return this.OkOrNotFound(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}
