using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using next_generation.Data;
using next_generation.Models;
using next_generation.Models.DTO;
using next_generation.Repositorio.IRepositorio;
using System.Net;

namespace next_generation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        private readonly ILogger<ApiController> _logger; // Agrega una propiedad de tipo ILogger
        private readonly IGenUserRepository _genRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public ApiController(ILogger<ApiController> logger, IGenUserRepository genRepo, IMapper mapper)
        {
            _logger = logger; // Inyecta el ILogger en el constructor
            _genRepo= genRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetGenUsers()
        {

            try
            {
                _logger.LogInformation("Obtain users");


                IEnumerable<NexGenUsers> userList = await _genRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NextGenUsersDto>>(userList);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex) {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
              
            }

            return _response;

        }

        [HttpGet("{id:int}", Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<NextGenUsersDto>> GetUsers(int id)
        {
            if (id == 0)
            {
                return BadRequest("El valor del ID no puede ser 0"); // Devuelve un código 400 (Bad Request) con un mensaje de error
            }

            //var user = NextGenUsersData.UsersList.FirstOrDefault(v => v.Id == id);
            var user = await _genRepo.Obtener(v => v.Id == id);

            if (user == null)
            {
                return NotFound(); // Devuelve un código 200 (OK) con el objeto encontrado
            }
           
           

            return Ok(_mapper.Map<IEnumerable<NextGenUsersDto>>(user));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<NextGenUsersDto>> AddGenUser([FromBody] NextGenUsersCreateDto createDto)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            if(await _genRepo.Obtener(v=>v.FirstName.ToLower() == createDto.FirstName.ToLower()) !=null)
            {
                ModelState.AddModelError("The user already registered", "That user already exists!");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            NexGenUsers model = _mapper.Map<NexGenUsers>(createDto);
          

            await _genRepo.Crear(model);
           

            return CreatedAtRoute("GetUsers", new {id = model.Id}, model);

           

           
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {

            if (id == 0)
            {
                return NotFound(); // Return a 404 (Not Found) status code if the user is not found.
            }

            var user = await _db.NextGenUser.FirstOrDefaultAsync(v => v.Id == id);

            if(user == null)
            {
                return NotFound();
            }
            
            _db.NextGenUser.Remove(user); // Remove the user from the list.
            await _db.SaveChangesAsync();

            return NoContent(); // Return a 204 (No Content) status code to indicate a successful deletion.
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] NextGenUsersUpdateDto nextGenUsersDto)
        {  
            if (nextGenUsersDto == null || id!= nextGenUsersDto.Id )
            {
                return BadRequest("The user object cannot be null"); // Return a 400 (Bad Request) status code if the user object is null.
            }

            NexGenUsers model = new()
            {
                Id = nextGenUsersDto.Id,
                FirstName = nextGenUsersDto.FirstName,
                LastName = nextGenUsersDto.LastName,
                Email = nextGenUsersDto.Email,
                CreationDate = nextGenUsersDto.CreationDate,
                Password = nextGenUsersDto.Password
            };

            _db.NextGenUser.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();

          
        }

        // ...

        [HttpPatch("{id:int}", Name = "PartiallyUpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PartiallyUpdateUser(int id, [FromBody] JsonPatchDocument<NextGenUsersUpdateDto> patchDto)
        {
            if (patchDto == null)
            {
                return BadRequest("The patch document cannot be null");
            }

            var user = await _db.NextGenUser.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);


            var userToPatch = new NextGenUsersDto();

            NextGenUsersUpdateDto nextGenUsersDto = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreationDate = user.CreationDate,
                Password = user.Password
            };

            if (user == null) return BadRequest();

            patchDto.ApplyTo(nextGenUsersDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NexGenUsers model = new()
            {
                Id = nextGenUsersDto.Id ,
                FirstName = nextGenUsersDto.FirstName,
                LastName = nextGenUsersDto.LastName,
                Email = nextGenUsersDto.Email,
                CreationDate = nextGenUsersDto.CreationDate,
                Password = nextGenUsersDto.Password
            };

            _db.NextGenUser.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }




    }
}
