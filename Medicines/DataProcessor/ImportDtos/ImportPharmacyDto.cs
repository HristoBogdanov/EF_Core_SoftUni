using Medicines.Data.Models;
using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacyDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        [RegularExpression(@"\(\d{3}\) \d{3}-\d{4}")]
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("^(true|false)$")]
        [XmlAttribute("non-stop")]
        public bool IsNonStop { get; set; }

        [XmlArray("Medicines")]
        public ImportMedicineDto[] Medicines { get; set; }
    }
}
