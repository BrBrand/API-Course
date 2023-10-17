using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace next_generation.Models
{
    public class NexGenUsers
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    // Validation Annotations
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        public string Password { get; set; }
    
    // Other relevant properties, such as roles, preferences, etc.
    }
}
