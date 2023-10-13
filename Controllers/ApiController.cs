using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using next_generation.Data;
using next_generation.Models;
using next_generation.Models.DTO;

namespace next_generation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<NextGenUsersDto>> GetGenUsers()
        {
            return Ok(NextGenUsersData.UsersList);


        }

        [HttpGet("{id:int}", Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<NextGenUsersDto> GetUsers(int id)
        {
            if (id == 0)
            {
                return BadRequest("El valor del ID no puede ser 0"); // Devuelve un código 400 (Bad Request) con un mensaje de error
            }

            var user = NextGenUsersData.UsersList.FirstOrDefault(v => v.Id == id);

            if (user != null)
            {
                return Ok(user); // Devuelve un código 200 (OK) con el objeto encontrado
            }
            else
            {
                return NotFound(); // Devuelve un código 404 (Not Found) si no se encuentra el usuario
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<NextGenUsersDto> AddGenUser([FromBody] NextGenUsersDto nextGenUsersDto)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            if(NextGenUsersData.UsersList.FirstOrDefault(v=>v.FirstName.ToLower() == nextGenUsersDto.FirstName.ToLower()) !=null)
            {
                ModelState.AddModelError("The user already registered", "That user already exists!");
                return BadRequest(ModelState);
            }
            if (nextGenUsersDto == null)
            {
                return BadRequest("El objeto de usuario no puede ser nulo");
            }

            if (nextGenUsersDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Suponiendo que NextGenUsersData.UsersList es la lista de usuarios
            nextGenUsersDto.Id = NextGenUsersData.UsersList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            NextGenUsersData.UsersList.Add(nextGenUsersDto);

            return CreatedAtRoute("GetUsers", new {id = nextGenUsersDto.Id}, nextGenUsersDto);

           

           
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {
            var user = NextGenUsersData.UsersList.FirstOrDefault(v => v.Id == id);

            if (user == null)
            {
                return NotFound(); // Return a 404 (Not Found) status code if the user is not found.
            }

            NextGenUsersData.UsersList.Remove(user); // Remove the user from the list.

            return NoContent(); // Return a 204 (No Content) status code to indicate a successful deletion.
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateUser(int id, [FromBody] NextGenUsersDto updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("The user object cannot be null"); // Return a 400 (Bad Request) status code if the user object is null.
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors if the model state is not valid.
            }

            var existingUser = NextGenUsersData.UsersList.FirstOrDefault(v => v.Id == id);

            if (existingUser == null)
            {
                return NotFound(); // Return a 404 (Not Found) status code if the user to update is not found.
            }

            // Perform the user update here.
            existingUser.FirstName = updatedUser.FirstName;

            return NoContent(); // Return a 204 (No Content) status code to indicate a successful update.
        }

        // ...

        [HttpPatch("{id:int}", Name = "PartiallyUpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PartiallyUpdateUser(int id, [FromBody] JsonPatchDocument<NextGenUsersDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("The patch document cannot be null");
            }

            var existingUser = NextGenUsersData.UsersList.FirstOrDefault(v => v.Id == id);

            if (existingUser == null)
            {
                return NotFound();
            }

            var userToPatch = new NextGenUsersDto();

            // Utiliza un delegado para manejar los errores de JsonPatchDocument.ApplyTo
            patchDocument.ApplyTo(userToPatch, (Microsoft.AspNetCore.JsonPatch.JsonPatchError err) =>
            {
                // Puedes realizar acciones específicas en caso de error aquí.
                // Por ejemplo, puedes agregar el error al ModelState si es necesario.
                ModelState.AddModelError("JsonPatchError", err.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Realiza la operación de parcheo (partial update) con el objeto userToPatch.

            return NoContent();
        }




    }
}
