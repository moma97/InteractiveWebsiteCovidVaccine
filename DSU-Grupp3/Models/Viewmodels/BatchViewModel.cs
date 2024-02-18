using DSU_Grupp3.Models.DTO;

namespace DSU_Grupp3.Models.Viewmodels
{
    public class BatchViewModel
    {
        public IEnumerable<string> BatchNumber { get; set; }
        public IEnumerable<int> TotalUses { get; set; }
        
        public BatchViewModel(VaccineBatchesDTO vaccineBatches)
        {
            CalculateBatches(vaccineBatches);            
        }
        /// <summary>
        /// Gets the names of the batches and gets how many of each batch it gets
        /// </summary>
        /// <param name="vaccineBatches"></param>
        public void CalculateBatches(VaccineBatchesDTO vaccineBatches) 
        {
            var vaccines = new Dictionary<string, int>();

            foreach (var batch in vaccineBatches.Batches)
            {
                vaccines.Add(batch.BatchNumber, batch.Totaluses);
            }
            BatchNumber = vaccines.Keys.ToList();
            TotalUses = vaccines.Values.ToList();      
        }
    }
}
