using next_generation.Models.DTO;

namespace next_generation.Data
{
    public static class NextGenUsersData
    {
        public static List<NextGenUsersDto> UsersList = new List<NextGenUsersDto>
        {
              new NextGenUsersDto
                {
                      Id = 1,
                      FirstName = "John",

                }, new NextGenUsersDto
                {
                      Id = 2,
                      FirstName = "Jane",

                }
        }; 
    }
}
