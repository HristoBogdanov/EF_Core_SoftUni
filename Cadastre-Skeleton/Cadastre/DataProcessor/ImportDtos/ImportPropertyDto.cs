using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("Property")]
    public class ImportPropertyDto
    {
        [Required]
        [MinLength(16)]
        [MaxLength(20)]
        [XmlElement("PropertyIdentifier")]
        public string PropertyIdentifier { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue)]
        [XmlElement("Area")]
        public int Area { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        [XmlElement("Details")]
        public string Details { get; set; } = null!;

        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        [XmlElement("Address")]
        public string Address { get; set; } = null!;

        [Required]
        [XmlElement("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; }
    }
}

//•	PropertyIdentifier – text with length [16, 20] (required)
//•	Area – int not negative (required)
//•	Details - text with length [5, 500] (not required)
//•	Address – text with length [5, 200] (required)
//•	DateOfAcquisition – DateTime (required)
