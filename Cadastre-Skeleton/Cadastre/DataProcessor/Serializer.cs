using Cadastre.Data;
using Cadastre.Data.Enumerations;
using Cadastre.DataProcessor.ExportDtos;
using Cadastre.Helpers;
using Newtonsoft.Json;
using System.Globalization;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            DateTime date = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

            var properties = dbContext.Properties
                 .Where(p => p.DateOfAcquisition >= date)
                 .Select(p => new
                 {
                     PropertyIdentifier = p.PropertyIdentifier,
                     Area = p.Area,
                     Address = p.Address,
                     DateOfAcquisition = DateTime.ParseExact(p.DateOfAcquisition.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString(),
                     Owners = p.PropertiesCitizens
                         .Where(pc => pc.PropertyId == p.Id)
                         .Select(pc => new
                         {
                             LastName = pc.Citizen.LastName,
                             MaritalStatus = pc.Citizen.MaritalStatus.ToString(),
                         })
                         .OrderBy(o => o.LastName)
                         .ToArray()
                 })
                 .OrderByDescending(p => p.DateOfAcquisition)
                 .ThenBy(p => p.PropertyIdentifier)
                 .ToArray();

            return JsonConvert.SerializeObject(properties, Formatting.Indented);
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            var properties = dbContext.Properties
                .Where(p => p.Area >= 100)
                .Select(p => new ExportProperyDto()
                {
                    PostalCode = p.District.PostalCode,
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,
                    DateOfAcquisition = DateTime.ParseExact(p.DateOfAcquisition.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString()
                })
                .OrderByDescending(p=>p.Area)
                .ThenBy(p=>p.DateOfAcquisition)
                .ToArray();

            return XmlSerializationHelper.Serialize(properties, "Properties");
        }
    }
}
