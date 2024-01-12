using Microsoft.AspNetCore.Mvc;
using SupportDeskAPI.Models;
using SupportDeskAPI.Services;

namespace SupportDeskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportRequestsController : ControllerBase
    {
        private readonly ISupportRequestService _service;

        public SupportRequestsController(ISupportRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var request = _service.GetById(id);
                if (request == null)
                {
                    return NotFound();
                }

                return Ok(request);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] SupportRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdRequest = _service.Create(request);
                return CreatedAtAction(nameof(Get), new { id = createdRequest.Id }, createdRequest);
            }
            catch (Exception)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] SupportRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _service.Update(request);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (_service.Delete(id))
                {
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}/resolve")]
        public IActionResult Resolve(Guid id)
        {
            try
            {
                if (_service.Resolve(id))
                {
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error");
            }
        }
    }
}