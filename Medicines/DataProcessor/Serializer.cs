namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.DataProcessor.ExportDtos;
    using Medicines.Helpers;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            string format = "yyyy-MM-dd";
            DateTime newDate = DateTime.ParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture);

            var medicinesAfterDateIds=context.Medicines
                .Where(m=>m.ProductionDate>newDate)
                .Select(m=>m.Id)
                .ToList();

            var patientsPurchased = context.PatientsMedicines
                .Where(pm => medicinesAfterDateIds.Contains(pm.MedicineId))
                .Select(pm => pm.PatientId)
                .ToList();

            var patients=context.Patients
                .Include(p=>p.PatientsMedicines)
                .ThenInclude(pm=>pm.Medicine)
                .Select(p=> new ExportPatientDto()
                {
                    Name=p.FullName,
                    AgeGroup=p.AgeGroup,
                    Gender=p.Gender,
                    Medicines= new ExportMedicineOfPatientDto[] {}
                });

            return XmlSerializationHelper.Serialize(patients, "Patients");
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var nonStopPharmacies=context.Pharmacies
                .Where(p=>p.IsNonStop==true)
                .Select(m=>m.Id)
                .ToList();


            var medicines = context.Medicines
                .Where(m => (int)m.Category == medicineCategory
                && nonStopPharmacies.Contains(m.PharmacyId))
                .Select(m => new ExportMedicineDto()
                {
                    Name= m.Name,
                    Price= m.Price,
                    Pharmacy=new ExportPharmacyDto()
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber=m.Pharmacy.PhoneNumber
                    }
                })
                .OrderBy(m=>m.Price)
                .ThenBy(m=>m.Name)
                .ToList();

          return JsonConvert.SerializeObject(medicines,Formatting.Indented);      
        }
    }
}
