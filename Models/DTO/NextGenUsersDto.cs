using System.ComponentModel.DataAnnotations;

namespace next_generation.Models.DTO
{
    public class NextGenUsersDto
    {
       

        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        // Validation Annotations
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        public string Password { get; set; }

    }
}
