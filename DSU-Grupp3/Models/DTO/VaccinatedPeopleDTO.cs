using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DSU_Grupp3.Models.DTO
{
    public class VaccinatedPeopleData
    {
        public VaccinatedPeopleDTO Data { get; set; }
    }
    public class VaccinatedPeopleDTO
    {   
      public _Meta meta {  get; set; }
        public List<Patients> patients { get; set; }
    }

    public class _Meta
    {
        [JsonProperty(PropertyName = "total-records-patients")]
        public int totalrecordspatients { get; set; }
        [JsonProperty(PropertyName = "total-vaccinations")]
        public int totalvaccinations { get; set; }
        [JsonProperty(PropertyName = "deso-code")]
        public string desocode { get; set; }
        [JsonProperty(PropertyName = "latest-change")]
        public DateTime latestchange { get; set; }
    }

    public class Patients
    {
        [JsonProperty(PropertyName = "anonymous-id")]
        public int anonymousId { get; set; }
        [JsonProperty(PropertyName = "year-of-birth")]
        public int yearofbirth { get; set; }

        public int age
        {
            get
            {
                return DateTime.Now.Year - yearofbirth;                 
            }
            set
            {
                
            }                                                
        }

        public string gender { get; set; }

        public Vaccinations[] vaccinations { get; set; }

        [JsonProperty(PropertyName = "latest-registration-date")]
        public DateTime LatestRegistrationDate { get; set; }
        

    }

    public class Vaccinations
    {
        [JsonProperty(PropertyName = "date-of-vaccination")]
        public DateTime dateofvaccination { get; set; }
        [JsonProperty(PropertyName = "date-of-registration")]
        public DateTime DateOfRegistration { get; set; }

        [JsonProperty(PropertyName = "batch-number")]
        public string batchnumber { get; set; }
        [JsonProperty(PropertyName = "place-of-injection")]
        public string placeofinjection { get; set; }
        [JsonProperty(PropertyName = "dose-number")]
        public int dosenumber { get; set; }
        [JsonProperty(PropertyName = "vaccination-site")]
        public VaccinationSite vaccinationsite { get; set; }

    }

    public class VaccinationSite 
    {
        [JsonProperty(PropertyName = "site-id")]
        public int siteid { get; set; }
        [JsonProperty(PropertyName = "site-name")]
        public string sitename { get; set; }

    }




    
}
