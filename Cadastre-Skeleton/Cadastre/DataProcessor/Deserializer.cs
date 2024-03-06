namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using Cadastre.DataProcessor.ImportDtos;
    using Cadastre.Helpers;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            var districtsDtos = XmlSerializationHelper.Deserialize<ImportDisctrictDto[]>(xmlDocument, "Districts");

            StringBuilder sb = new();

            List<District> districtList = new();

            foreach(var dDto in districtsDtos)
            {
                if(!IsValid(dDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(dbContext.Districts.Any(d=>d.Name == dDto.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                District district = new()
                {
                    Name=dDto.Name,
                    PostalCode=dDto.PostalCode,
                    Region= (Region)Enum.Parse(typeof(Region),dDto.Region)
                };

                foreach(var pDto in dDto.Properties)
                {
                    if(!IsValid(pDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if(district.Properties.Any(p=>p.PropertyIdentifier==pDto.PropertyIdentifier)
                        || dbContext.Properties.Any(p=>p.PropertyIdentifier==pDto.PropertyIdentifier))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if(district.Properties.Any(p => p.Address == pDto.Address)
                        || dbContext.Properties.Any(p => p.Address == pDto.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Property property = new()
                    {
                        PropertyIdentifier=pDto.PropertyIdentifier,
                        Area=pDto.Area,
                        Details=pDto.Details,
                        Address=pDto.Address,
                        DateOfAcquisition=DateTime.ParseExact(pDto.DateOfAcquisition, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                    };

                    district.Properties.Add(property);
                }

                districtList.Add(district);
                sb.AppendLine(String.Format(SuccessfullyImportedDistrict, district.Name, district.Properties.Count()));

            }

            dbContext.Districts.AddRange(districtList);
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new();
            
            var citizenDtos=JsonConvert.DeserializeObject<ImportCitizenDto[]>(jsonDocument);
            List<Citizen> citizenList = new();

            var uniquePropIds = dbContext.Properties
                .Select(p => p.Id)
                .ToArray();

            foreach (var cDto in citizenDtos)
            {
                if(!IsValid(cDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(cDto.MaritalStatus != "Unmarried" &&
                   cDto.MaritalStatus != "Married" &&
                   cDto.MaritalStatus != "Divorced" &&
                   cDto.MaritalStatus != "Widowed")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Citizen citizen = new()
                {
                    FirstName = cDto.FirstName,
                    LastName = cDto.LastName,
                    BirthDate= DateTime.ParseExact(cDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    MaritalStatus= (MaritalStatus)Enum.Parse(typeof(MaritalStatus), cDto.MaritalStatus)
                };

                foreach (var propertyId in cDto.PropertiesCitizens.Distinct())
                {
                    //if (!uniquePropIds.Contains(propertyId))
                    //{
                    //    sb.AppendLine(ErrorMessage);
                    //    continue;
                    //}

                    PropertyCitizen pc = new()
                    {
                        Citizen=citizen,
                        PropertyId=propertyId
                    };

                    citizen.PropertiesCitizens.Add(pc);
                }
                citizenList.Add(citizen);
                sb.AppendLine(String.Format(SuccessfullyImportedCitizen,
                    citizen.FirstName,
                    citizen.LastName,
                    citizen.PropertiesCitizens.Count()));
            }
            
            dbContext.Citizens.AddRange(citizenList);
            dbContext.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
