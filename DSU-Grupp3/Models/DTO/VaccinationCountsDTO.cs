using DSU_Grupp3.Infrastructure;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace DSU_Grupp3.Models.DTO
{
    public class VaccineCountData
    {
        public VaccinationCountsDTO Data { get; set; }        
    } 

    public class VaccinationCountsDTO
    {
        public Meta meta { get; set; }
        public DesoCode[] data { get; set; }        
    }
    public class Meta
    {
        [JsonProperty(PropertyName = "latest-changes")]
        public List<LatestChanges> LatestChanges { get; set; }

    }
    public class LatestChanges
    {
        public string Deso { get; set; }
        [JsonProperty(PropertyName = "latest-change")]
        public DateTime latestChange { get; set; }
    }

    public class DesoCode
    {
        public string Deso { get; set; }
        public VaccinationsPerDeso[] data { get; set; }

    }

    public class VaccinationsPerDeso
    {
        [JsonProperty(PropertyName = "dose-number")]
        public int DoseNumber { get; set; }
        public int Count { get; set; }

    }


    
}
