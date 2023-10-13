using System.ComponentModel.DataAnnotations;

namespace next_generation.Models.DTO
{
    public class NextGenUsersDto
    {
       

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
    }
}
