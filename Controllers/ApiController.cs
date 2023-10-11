using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using next_generation.Models;

namespace next_generation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<NexGenUsers> GetGenUsers()
        {
            return new List<NexGenUsers>
            {
                new NexGenUsers
{
    Id = 1,
    FirstName = "John",
    LastName = "Doe",
    Email = "john.doe@example.com",
    CreationDate = new DateTime(2023, 5, 10),
    Password = "password123"
}, new NexGenUsers
{
    Id = 2,
    FirstName = "Jane",
    LastName = "Smith",
    Email = "jane.smith@example.com",
    CreationDate = new DateTime(2023, 6, 15),
    Password = "securepass"
}

        };
        }
    }
}
