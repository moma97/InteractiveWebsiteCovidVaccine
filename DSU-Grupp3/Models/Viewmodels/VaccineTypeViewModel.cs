using DSU_Grupp3.Models.DTO;
using System.Collections;

namespace DSU_Grupp3.Models.Viewmodels
{
    public class VaccineTypeViewModel
    {
        public IEnumerable<string> VaccineType { get; set; }
        public IEnumerable<int> TotalUses { get; set; }

        /// <summary>
        /// Takes a VaccineBatchesDTO and updates the VaccineType and TotalUses lists in the viewmodel
        /// </summary>
        /// <param name="vaccineBatches"></param>
        public VaccineTypeViewModel(VaccineBatchesDTO vaccineBatches)
        {
            var vaccineTypes = new Dictionary<string, int>();

            foreach (var batch in vaccineBatches.Batches)
            {
                if (!vaccineTypes.ContainsKey(batch.Manufacturer))
                {
                    vaccineTypes.Add(batch.Manufacturer, batch.Totaluses);
                }
                else
                {
                    vaccineTypes[batch.Manufacturer] += batch.Totaluses;
                }
            }

            var orderedByKeyVaccines = new SortedDictionary<string, int>(vaccineTypes);

            VaccineType = orderedByKeyVaccines.Keys.ToList();
            TotalUses = orderedByKeyVaccines.Values.ToList();

        }
    }
}
