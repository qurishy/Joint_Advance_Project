using Data_Transfer_API.Model;
using Data_Transfer_API.Repository.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Data_Transfer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Profile_Controller : ControllerBase
    {
        private readonly ILogger<Profile_Controller> _logger;
        private readonly IUser_Service _user_Service;

        public Profile_Controller(IUser_Service user_Service, ILogger<Profile_Controller> logger)
        {
            _user_Service = user_Service;
            _logger = logger;


        }


        [HttpGet]
        public async Task<ActionResult> GetAllTheUsers()
        {
            try
            {

                return Ok(await _user_Service.GetAllAsync());
         
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User_Info>> GetTheUser(Guid id)
        {
            try
            {
               // Guid userId = Guid.Parse(id);
                return Ok(await _user_Service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }


        // POST api/<CataController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User_Info value)
        {
            try
            {      
                await _user_Service.CreateAsync(value);
            
                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }


        }

        // PUT api/<CataController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User_Info value)
        {
            try
            {
                await _user_Service.updateAsync(value);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


            
        }

        // DELETE api/<CataController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheUser([FromRoute] User_Info value, string id)
        {
           try
            {
                Guid userId = Guid.Parse(id);
                
                if (value != null)
                {   
                    if(value.Id != userId.ToString())
                                    {
                        return BadRequest();
                    }

                    await _user_Service.DeleteAsync(value,userId);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
