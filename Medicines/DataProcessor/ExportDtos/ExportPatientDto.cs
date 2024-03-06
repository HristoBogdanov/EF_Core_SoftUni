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
    [XmlType("Patient")]
    public class ExportPatientDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("AgeGroup")]
        public AgeGroup AgeGroup { get; set; }

        [Required]
        [XmlAttribute("Gender")]
        public Gender Gender { get; set; }

        [XmlArray("Medicines")]
         public ExportMedicineOfPatientDto[] Medicines { get;set; }
    }
}
