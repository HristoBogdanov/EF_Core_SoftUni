using Medicines.Data.Models.Enums;
using Medicines.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientsDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        [JsonProperty("FullName")]
        public string FullName { get; set; } = null!;

        [Required]
        [JsonProperty("AgeGroup")]
        [Range(0, 2)]
        public AgeGroup AgeGroup { get; set; }

        [Required]
        [Range(0, 1)]
        [JsonProperty("Gender")]
        public Gender Gender { get; set; }

        [JsonProperty("Medicines")]
        [Required]
        public int[] MedicinesIds { get; set; }
    }
}
