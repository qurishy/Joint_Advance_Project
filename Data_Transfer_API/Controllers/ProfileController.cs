using AutoMapper;
using Data_Transfer_API.Model;
using Data_Transfer_API.Model.DTOClasses;
using Data_Transfer_API.Repository.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MongoDB.Bson;

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
               
                // Check if the string is null or empty
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("User ID cannot be null or empty.");
                }
            

                var user = await _userService.GetByIdAsync(id);
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

                if (value == null)
                {
                    return BadRequest("User information is required.");
                }

                // Map UserInfoDTO to User_Info
                var userInfo = _mapper.Map<User_Info>(value);

                if (userInfo == null){

                    return BadRequest("There is missing values");
                }

                await _userService.CreateAsync(userInfo);


                // Return the created user (or a success message)
                return Ok(userInfo);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UserInfoDTO value)
        {
            try
            {

              

                // Step 1: Retrieve the existing entity from the database
                var userInfoEntity = await _userService.GetByIdAsync(id);

                if (userInfoEntity == null)
                {
                    return NotFound("User not found.");
                }

                if (id.ToString() != userInfoEntity.Id)
                {
                    return BadRequest("ID in the route does not match the ID in the body.");
                }


                // Step 2: Map the DTO to the existing entity
                _mapper.Map(value, userInfoEntity);

                // Step 3: Update the entity in the database
                await _userService.updateAsync(userInfoEntity);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
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