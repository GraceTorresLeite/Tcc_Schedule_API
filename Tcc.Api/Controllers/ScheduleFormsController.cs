using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tcc.Api.Business;
using Tcc.Api.Data.VO;
using Tcc.Api.Hypermedia.Filters;
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
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_scheduleFormsBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] ScheduleFormVO scheduleFormVO)
        {
            if (scheduleFormVO == null) return BadRequest();
            var createScheduleForm = _scheduleFormsBusiness.Create(scheduleFormVO);
            return Ok(createScheduleForm);
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] ScheduleFormVO scheduleFormVO)
        {
            if (scheduleFormVO == null) return BadRequest();
            var updateScheduleForm = _scheduleFormsBusiness.Update(scheduleFormVO);
            return Ok(updateScheduleForm);
        }

        [HttpDelete("{id}")]
      //  [TypeFilter(typeof(HyperMediaFilter))]
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
