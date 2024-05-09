using API.Models;
using API.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : BaseController
    {
        private readonly IWorkService _workService;

        public WorkController(IWorkService workService)
        {
            _workService = workService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorks()
        {
            var works = await _workService.GetWorksAsync();
            return Ok(works);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWork(Guid id)
        {
            var work = await _workService.GetWorkByIdAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return Ok(work);
        }

        [HttpPost]
        public async Task<IActionResult> AddWork([FromBody] Work work)
        {
            await _workService.AddWorkAsync(work);
            return CreatedAtAction(nameof(GetWork), new { id = work.Id }, work);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWork(Guid id, [FromBody] Work work)
        {
            await _workService.UpdateWorkAsync(id, work);
            return NoContent();
        }

        [HttpPost("Bulk")]
        public async Task<IActionResult> AddBulkWork([FromBody] List<Work> works)
        {
            await _workService.AddBulkWorkAsync(works);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWork([FromBody] Guid id)
        {
            await _workService.DeleteWorkAsync(id);
            return Ok();
        }
        
        [HttpDelete("Bulk")]
        public async Task<IActionResult> DeleteBulkWork([FromBody] List<Guid> guids)
        {
            await _workService.DeleteBulkWorkAsync(guids);
            return Ok();
        }
    }
}