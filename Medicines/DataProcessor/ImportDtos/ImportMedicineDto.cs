using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportMedicineDto
    {
        [Required]
        [MaxLength(150)]
        [MinLength(3)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        [XmlElement("Price")]
        public decimal Price { get; set; }

        [Required]
        [XmlElement("ProductionDate")]
        public DateTime ProductionDate { get; set; }

        [Required]
        [XmlElement("ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        [XmlElement("Producer")]
        public string Producer { get; set; }

        [Required]
        [XmlAttribute("category")]
        public Category category { get; set; }

    }
}

//< Medicine category = "1" >

//                < Name > Ibuprofen </ Name >

//                < Price > 8.50 </ Price >

//                < ProductionDate > 2022 - 02 - 10 </ ProductionDate >

//                < ExpiryDate > 2025 - 02 - 10 </ ExpiryDate >

//                < Producer > ReliefMed Labs </ Producer >

//            </ Medicine >
