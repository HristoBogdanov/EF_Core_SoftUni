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
    public class ExportPharmacyDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        [RegularExpression(@"\(\d{3}\) \d{3}-\d{4}")]
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

    }
}
