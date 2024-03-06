using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("District")]
    public class ImportDisctrictDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(80)]
        [XmlElement("Name")]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        [RegularExpression(@"^[A-Z]{2}-\d{5}")]
        [XmlElement("PostalCode")]
        public string PostalCode { get; set; } = null!;

        [Required]
        [XmlAttribute("Region")]
        public string Region { get; set; }

        [XmlArray("Properties")]
        public ImportPropertyDto[] Properties { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	Name – text with length [2, 80] (required)
//•	PostalCode – text with length 8. All postal codes must have the following structure:starting with two capital letters, followed by e dash '-', followed by five digits. Example: SF - 10000(required)
//•	Region – Region enum (SouthEast = 0, SouthWest, NorthEast, NorthWest)(required)
//•	Properties - collection of type Property
