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
    public class ExportMedicineDto
    {
        [Required]
        [MaxLength(150)]
        [MinLength(3)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("Pharmacy")]
        public ExportPharmacyDto Pharmacy { get; set; }
    }
}
