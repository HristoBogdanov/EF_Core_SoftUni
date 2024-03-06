using Medicines.Data.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Medicine")]
    public class ExportMedicineOfPatientDto
    {
        [Required]
        [MaxLength(150)]
        [MinLength(3)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlAttribute("Category")]
        public Category Category { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        [XmlElement("Price")]
        public string Price { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        [XmlElement("Producer")]
        public string Producer { get; set; }

        [Required]
        [XmlElement("BestBefore")]
        public DateTime BestBefore { get; set; }


    }
}
