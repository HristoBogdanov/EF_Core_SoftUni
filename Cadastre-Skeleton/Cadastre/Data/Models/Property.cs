using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastre.Data.Models
{
    public class Property
    {
        public Property()
        {
            PropertiesCitizens = new List<PropertyCitizen>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string PropertyIdentifier { get; set; } = null!;

        [Required]
        public int Area { get; set; }

        [Required]
        [MaxLength(500)]
        public string Details { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }=null!;

        [Required]
        public DateTime DateOfAcquisition { get; set; }

        [Required]
        [ForeignKey(nameof(District))]
        public int DistrictId { get; set; }

        public District District { get; set; }

        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	PropertyIdentifier – text with length [16, 20] (required)
//•	Area – int not negative (required)
//•	Details - text with length [5, 500] (not required)
//•	Address – text with length [5, 200] (required)
//•	DateOfAcquisition – DateTime (required)
//•	DistrictId – integer, foreign key (required)
//•	District – District
//•	PropertiesCitizens - collection of type PropertyCitizen

