using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DSU_Grupp3.Models.DTO
{
    
    public class DeSoDTO
	{
        [Key]
        public int Id { get; set; }
        public List<DesoCodeInformation>? Areas { get; set; }		
    }

	public class DesoCodeInformation
	{
        public int Id { get; set; }
		[JsonProperty(PropertyName = "region-code")]
		public int RegionCode { get; set; }
        public string Name { get; set; }
        public string Municipality { get; set; }

		[JsonProperty(PropertyName = "municipality-code")]
		public int MunicipalityCode { get; set; }
        public string Deso { get; set; }

        [JsonProperty(PropertyName = "deso-name")]
        public string DesoName { get; set; }

        public int TotalPopulation { get; set; }
        public int PopulationMales { get; set; }
        public int PopulationFemales { get; set; }

        public int TotalVaccinated { get; set; }
        public int VaccinatedFemales { get; set; }
        public int VaccinatedMales { get; set; }
        public int VaccinatedOtherGender { get; set; }


    }
}
