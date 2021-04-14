using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tcc.Api.Business;
using Tcc.Api.Model;

namespace Tcc.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScheduleFormsController : ControllerBase
    {
        private readonly ILogger<ScheduleFormsController> _logger; //(usando underline quando a variavel é global e para não usar o this)
        private IScheduleFormsBusiness _scheduleFormsBusiness;

        public ScheduleFormsController(ILogger<ScheduleFormsController> logger, IScheduleFormsBusiness scheduleFormsBusiness)
        {
            _logger = logger;
            _scheduleFormsBusiness = scheduleFormsBusiness;
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_scheduleFormsBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var scheduleForm = _scheduleFormsBusiness.FindByID(id);

            if (scheduleForm == null) return NotFound();

            return Ok(scheduleForm);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] ScheduleForm scheduleForm)
        {
            if (scheduleForm == null) return BadRequest();
            var createPerson = _scheduleFormsBusiness.Create(scheduleForm);
            return Ok(createPerson);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] ScheduleForm scheduleForm)
        {
            if (scheduleForm == null) return BadRequest();
            var updateScheduleForm = _scheduleFormsBusiness.Update(scheduleForm);
            return Ok(updateScheduleForm);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _scheduleFormsBusiness.Delete(id);
            return NoContent();
        }
    }
}
