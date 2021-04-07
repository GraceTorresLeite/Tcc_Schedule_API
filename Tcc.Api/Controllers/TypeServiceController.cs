using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tcc.Api.Business;
using Tcc.Api.Model;

namespace Tcc.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1{version:apiVersion}/[controller]")]
    [ApiController]
    public class TypeServiceController : ControllerBase
    {

        private readonly ILogger<TypeServiceController> _logger; 
        private ITypeServiceBusiness _typeServiceBusiness;

        public TypeServiceController(ILogger<TypeServiceController> logger, ITypeServiceBusiness typeServiceBusiness)
        {
            _logger = logger;
            _typeServiceBusiness = typeServiceBusiness;
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_typeServiceBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var type = _typeServiceBusiness.FindByID(id);

            if (type == null) return NotFound();

            return Ok(type);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] TypeService typeService)
        {
            if (typeService == null) return BadRequest();
            var createTypeService = _typeServiceBusiness.Create(typeService);
            return Ok(createTypeService);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] TypeService typeService)
        {
            if (typeService == null) return BadRequest();
            var updateTypeService = _typeServiceBusiness.Update(typeService);
            return Ok(updateTypeService);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _typeServiceBusiness.Delete(id);
            return NoContent();
        }

    }
}
    

