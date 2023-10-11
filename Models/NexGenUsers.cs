using System.ComponentModel.DataAnnotations;

namespace next_generation.Models
{
    public class NexGenUsers
    {
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
