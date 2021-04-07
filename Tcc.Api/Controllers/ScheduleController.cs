using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tcc.Api.Business;
using Tcc.Api.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tcc.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly ILogger<ScheduleController> _logger; //(usando underline quando a variavel é global e para não usar o this)
        private IScheduleBusiness _scheduleBusiness;

        public ScheduleController(ILogger<ScheduleController> logger, IScheduleBusiness scheduleBusiness)
        {
            _logger = logger;
            _scheduleBusiness = scheduleBusiness;
        }

      
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_scheduleBusiness.FindAll());
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var schedule = _scheduleBusiness.FindByID(id);

            if (schedule == null) return NotFound();

            return Ok(schedule);
        }

        
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] Schedule schedule)
        {
            if (schedule == null) return BadRequest();
            var createSchedule = _scheduleBusiness.Create(schedule);
            return Ok(createSchedule);
        }

     
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] Schedule schedule)
        {
            if (schedule == null) return BadRequest();
            var updateSchedule = _scheduleBusiness.Update(schedule);
            return Ok(updateSchedule);
        }

       
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _scheduleBusiness.Delete(id);
            return NoContent();
        }
    }
}
