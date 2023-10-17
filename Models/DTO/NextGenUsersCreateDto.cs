using System.ComponentModel.DataAnnotations;

namespace next_generation.Models.DTO
{
    public class NextGenUsersCreateDto
    {

    

            
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

