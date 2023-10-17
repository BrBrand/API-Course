using System.ComponentModel.DataAnnotations;

namespace next_generation.Models.DTO
{
    public class NextGenUsersUpdateDto
    {

        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }


        // Validation Annotations
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
