using DSU_Grupp3.Models.DTO;
using System.Collections;

namespace DSU_Grupp3.Models.Viewmodels
{
    public class VaccinationCenterViewModel
    {
        public IEnumerable <String> SiteName { get; set; }
        public IEnumerable<int> Vaccinations { get; set; }
        public IEnumerable<int> UniquePatients { get; set; } // Tomma kommentarer för allt som är relaterat till unika patienter

        public VaccinationCenterViewModel(List<VaccinatedPeopleDTO> vaccinatedPeopleDTO)
        {
            var vaccineSites = new Dictionary<string, int>();
            var uniqueSiteVaccinations = new Dictionary<string, int>(); //


            foreach (var deso in vaccinatedPeopleDTO)
            {
                foreach(var patient in deso.patients)
                {
                    var uniqueSites = new List<string>(); //

                    foreach (var vaccination in patient.vaccinations)
                    {
                        if (!vaccineSites.ContainsKey(vaccination.vaccinationsite.sitename))
                        {
                            vaccineSites.Add(vaccination.vaccinationsite.sitename, 1);
                            uniqueSiteVaccinations.Add(vaccination.vaccinationsite.sitename, 0); //
                        }
                        else
                        {
                            vaccineSites[vaccination.vaccinationsite.sitename]++;
                        }

                        if (!uniqueSites.Contains(vaccination.vaccinationsite.sitename)) //
                        {
                            uniqueSites.Add(vaccination.vaccinationsite.sitename);
                        }

                    }
                    foreach(var site in uniqueSites)//
                    {
                        if (uniqueSiteVaccinations.ContainsKey(site)) //
                        {
                            uniqueSiteVaccinations[site]++;
                        }
                    }
                    
                }
            }

            var orderedByKeySites = new SortedDictionary<string, int>(vaccineSites);
            var orderedByKeyUniqueSites = new SortedDictionary<string, int>(uniqueSiteVaccinations);//
            

            SiteName = orderedByKeySites.Keys.ToList();
            Vaccinations = orderedByKeySites.Values.ToList();

            
            UniquePatients = orderedByKeyUniqueSites.Values.ToList();
        }
    }
}
