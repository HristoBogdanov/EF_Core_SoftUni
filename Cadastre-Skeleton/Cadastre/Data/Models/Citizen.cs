using Cadastre.Data.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace Cadastre.Data.Models
{
    public class Citizen
    {
        public Citizen()
        {
            PropertiesCitizens=new List<PropertyCitizen>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	FirstName – text with length [2, 30] (required)
//•	LastName – text with length [2, 30] (required)
//•	BirthDate – DateTime (required)
//•	MaritalStatus - MaritalStatus enum (Unmarried = 0, Married, Divorced, Widowed)(required)
//•	PropertiesCitizens - collection of type PropertyCitizen

