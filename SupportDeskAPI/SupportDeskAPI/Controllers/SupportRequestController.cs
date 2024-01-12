using Microsoft.AspNetCore.Mvc;
using SupportDeskAPI.Models;
using SupportDeskAPI.Services;

namespace SupportDeskAPI.Controllers
{
    /// <summary>
    /// Controller for managing support requests.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SupportRequestsController : ControllerBase
    {
        private readonly ISupportRequestService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportRequestsController"/> class.
        /// </summary>
        /// <param name="service">The support request service.</param>
        public SupportRequestsController(ISupportRequestService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all support requests.
        /// </summary>
        /// <returns>The list of support requests.</returns>
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

        /// <summary>
        /// Gets a support request by its ID.
        /// </summary>
        /// <param name="id">The ID of the support request.</param>
        /// <returns>The support request with the specified ID.</returns>
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

        /// <summary>
        /// Creates a new support request.
        /// </summary>
        /// <param name="request">The support request to create.</param>
        /// <returns>The created support request.</returns>
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

        /// <summary>
        /// Updates an existing support request.
        /// </summary>
        /// <param name="request">The support request to update.</param>
        /// <returns>No content if the update is successful.</returns>
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

        /// <summary>
        /// Deletes a support request by its ID.
        /// </summary>
        /// <param name="id">The ID of the support request to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
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

        /// <summary>
        /// Resolves a support request by its ID.
        /// </summary>
        /// <param name="id">The ID of the support request to resolve.</param>
        /// <returns>No content if the resolution is successful.</returns>
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