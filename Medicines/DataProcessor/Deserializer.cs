namespace Medicines.DataProcessor
{
    using Medicines.Helpers;
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.DataProcessor.ImportDtos;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb = new();
            var patientsDtos= JsonConvert.DeserializeObject<ImportPatientsDto[]>(jsonString);

            List<Patient> patientList = new();

            foreach(var patientDto in patientsDtos)
            {
                if(!IsValid(patientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var patient = new Patient()
                {
                    FullName = patientDto.FullName,
                    AgeGroup = patientDto.AgeGroup,
                    Gender = patientDto.Gender,
                };

                foreach(var medicineId in patientDto.MedicinesIds)
                    {
                        if (patient.PatientsMedicines.Any(x => x.MedicineId == medicineId))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        PatientMedicine patientMedicine = new PatientMedicine()
                        {
                            Patient = patient,
                            MedicineId= medicineId
                        };

                        patient.PatientsMedicines.Add(patientMedicine);
                    }

                patientList.Add(patient);
                sb.AppendLine(String.Format(SuccessfullyImportedPatient,patient.FullName,patient.PatientsMedicines.Count()));

            }
            context.AddRange(patientList);
            context.SaveChanges();

            return sb.ToString().Trim();

        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            var pharmaciesDtos = XmlSerializationHelper.Deserialize<ImportPharmacyDto[]>(xmlString, "Pharmacies");

            StringBuilder sb = new();
            List<Pharmacy> pharmacyList = new();

            foreach(var pharmacyDto in pharmaciesDtos)
            {
                if(!IsValid(pharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Pharmacy pharmacy = new()
                {
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber,
                    IsNonStop = false
                };

                foreach(var medicineDto in pharmacyDto.Medicines)
                {
                    if(!IsValid(medicineDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    pharmacy.Medicines.Add(new Medicine()
                    {
                        Name=medicineDto.Name,
                        Price=medicineDto.Price,
                        ProductionDate=medicineDto.ProductionDate,
                        ExpiryDate=medicineDto.ExpiryDate,
                        Producer=medicineDto.Producer,
                    });
                }

                pharmacyList.Add(pharmacy);
                sb.AppendLine(String.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count()));

            }

            context.Pharmacies.AddRange(pharmacyList);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
