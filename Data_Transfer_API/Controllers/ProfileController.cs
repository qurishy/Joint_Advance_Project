using AutoMapper;
using Data_Transfer_API.Model;
using Data_Transfer_API.Model.DTOClasses;
using Data_Transfer_API.Repository.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Data_Transfer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IUser_Service _userService;
        private readonly IMapper _mapper;

        public ProfileController(IUser_Service userService, ILogger<ProfileController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User_Info>> GetUser([FromRoute] string id)
        {
            try
            {
                  if (!Guid.TryParse(id, out Guid userId))
                  {
                    return BadRequest("Invalid user ID format.");
                    
                    }

                var user = await _userService.GetByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfoDTO value)
        {
            try
            {
                if(value != null)
                {
                    User_Info temp_user = _mapper.Map<User_Info>(value);git

                    

                
                await _userService.CreateAsync(value);

                }
                
                return CreatedAtAction(nameof(GetUser), new { id = value.Id }, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] User_Info value)
        {
            try
            {
                if (id.ToString() != value.Id)
                {
                    return BadRequest("ID in the route does not match the ID in the body.");
                }

                await _userService.updateAsync(value);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                await _userService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}