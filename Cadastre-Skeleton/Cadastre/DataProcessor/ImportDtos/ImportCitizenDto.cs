using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cadastre.DataProcessor.ImportDtos
{
    public class ImportCitizenDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [JsonProperty("LastName")]
        public string LastName { get; set; } = null!;

        [Required]
        [JsonProperty("BirthDate")]
        public string BirthDate { get; set; }

        [Required]
        [JsonProperty("MaritalStatus")]
        public string MaritalStatus { get; set; }

        [JsonProperty("Properties")]
        public virtual int[] PropertiesCitizens { get; set; }
    }
}
