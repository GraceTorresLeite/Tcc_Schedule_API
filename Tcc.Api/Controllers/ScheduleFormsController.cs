using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Tcc.Api.Business;
using Tcc.Api.Data.VO;
using Tcc.Api.Hypermedia.Filters;

namespace Tcc.Api.Controllers
{
    [ApiVersion("1")]   
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ScheduleFormsController : ControllerBase
    {
        private readonly ILogger<ScheduleFormsController> _logger; //(usando underline quando a variavel é global e para não usar o this)
        private IScheduleFormsBusiness _scheduleFormsBusiness;

        public ScheduleFormsController(ILogger<ScheduleFormsController> logger, IScheduleFormsBusiness scheduleFormsBusiness)
        {
            _logger = logger;
            _scheduleFormsBusiness = scheduleFormsBusiness;
        }

        [HttpGet()]
        [ProducesResponseType((200), Type = typeof(List<ScheduleFormVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_scheduleFormsBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(ScheduleFormVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var scheduleForm = _scheduleFormsBusiness.FindByID(id);

            if (scheduleForm == null) return NotFound();

            return Ok(scheduleForm);
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(ScheduleFormVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] ScheduleFormVO scheduleFormVO)
        {
            if (scheduleFormVO == null) return BadRequest();
            var createScheduleForm = _scheduleFormsBusiness.Create(scheduleFormVO);
            return Ok(createScheduleForm);
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(ScheduleFormVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
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
