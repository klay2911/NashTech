using API.Models.Models;
using API.Services.Services;
using API2.WebAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace API2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : BaseController
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        
        /// <summary>
        /// Retrieves all members 
        /// </summary>
        /// <returns> Returns a list of all members.  </returns>
        [HttpGet]
        public IActionResult GetAllMembersAsync()
        {
            var members = _memberService.GetMembersAsync();
            return Ok(members);
        }
        
        /// <summary>
        /// Get member base on id
        /// </summary>
        /// <param name="id">Id type is Guid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetMember(Guid id)
        {
            var member = _memberService.GetMemberByIdAsync(id);
            return member is null ? NotFound() : Ok(member);
        }

        /// <summary>
        /// Create new member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost("Range")]
        public IActionResult AddMember(Member member)
        {
            _memberService.AddMemberAsync(member);
            return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
        }

        /// <summary>
        /// Update member base on id
        /// </summary>
        /// <param name="id">Id type is Guid</param>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMemberAsync(Guid id, UpdateMemberRequest member)
        {
            var existingMember = await _memberService.GetMemberByIdAsync(id);
            if (existingMember == null)
            {
                return NotFound();
            }

            existingMember.FirstName = member.FirstName;
            existingMember.LastName = member.LastName;
            existingMember.Gender = member.Gender ?? existingMember.Gender;
            existingMember.Dob = member.Dob ?? existingMember.Dob;
            existingMember.PhoneNumber = member.PhoneNumber;
            existingMember.BirthPlace = member.BirthPlace;

            var updatedMember = await _memberService.UpdateMemberAsync(id, existingMember);
            return Ok(updatedMember);
        }
        /// <summary>
        /// Delete member base on id
        /// </summary>
        /// <param name="id">Id type is Guid</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(Guid id)
        {
             _memberService.DeleteMemberAsync(id);
            return NoContent();
        }
        
        /// <summary>
        /// Filter members by Name, Gender, BirthPlace
        /// </summary>
        /// <param name="filters">GenderType is an enum with 4 values {1: Unknown, 2: Male, 3: Female, 4: Other }</param>
        /// <returns> </returns>
        [HttpGet("filter")]
        public IActionResult GetByFilter([FromQuery] Dictionary<string, string> filters)
        {
            var members = _memberService.GetByFilterAsync(filters);
            return Ok(members);
        }
    }
}