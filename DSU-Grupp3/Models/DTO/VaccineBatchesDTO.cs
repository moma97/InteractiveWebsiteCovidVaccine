using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DSU_Grupp3.Models.DTO
{
    
    public class VaccineBatchesDTO
	{
		public List<VaccineBatchDataDTO> Batches { get; set; }
     
    }
    public class VaccineBatchDataDTO
    {
        [JsonProperty(PropertyName = "batch-number")]
        public string BatchNumber { get; set; }

        [JsonProperty(PropertyName = "vaccine-name")]
        public string VaccineName { get; set; }
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "total-uses")]
        public int Totaluses { get; set; }


    }
}
