using DSU_Grupp3.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace DSU_Grupp3.Models
{
    public class DesoDiagramViewModel
    {
        

        public IEnumerable<string> DesosNames { get; set; }
        public IEnumerable<int> DesoNumberOfVaccinated { get; set; }
        public IEnumerable<int> DesoPopulation { get; set; }
        public IEnumerable<string> DesoCategories { get; set; }
        public List<int> VaccinatedInCategories { get; set; } = new List<int>();
        public List<int> PopulationInCategories { get; set; } = new List<int>();
        public Dictionary<string, int> DesoRegionA { get; set; }
        public Dictionary<string, int> DesoRegionB { get; set; }
        public Dictionary<string, int> DesoRegionC { get; set; }
        public Dictionary<string, int> SelectedDesos { get; set; }
        public Dictionary<string, (int totalpopulation, int vaccinatedMales, int vaccinatedFemales,int vaccinatedOtherGender )> VaccinatedPopulationInDeSo { get; set; }
        public object DesoInformation { get; set; }

        public DesoDiagramViewModel(DeSoDTO deSoDTO)
        {
            DesoInformation = deSoDTO;
            DesosNames = deSoDTO.Areas.Select(x => x.DesoName);
            DesoNumberOfVaccinated = deSoDTO.Areas.Select(x => x.TotalVaccinated);
            DesoPopulation = deSoDTO.Areas.Select(x => x.TotalPopulation);
            DesoCategories = new List<string>() { "A: Glesbygd", "B: Tätort", "C: Centralort" };
            GetNumberOfVaccinatedInCategory(deSoDTO);
            GetSelectedDesosData(deSoDTO);
            GetVaccinatedGenderWithDesos(deSoDTO);
          
        }
        /// <summary>
        /// Adds data from a Deso and adds it to a Dictionary
        /// </summary>
        /// <param name="desoDTO"></param>
        public void GetVaccinatedGenderWithDesos(DeSoDTO desoDTO)
        {
            VaccinatedPopulationInDeSo = new Dictionary<string, (int, int, int, int)>();
            foreach (var deso in desoDTO.Areas)
            {
                VaccinatedPopulationInDeSo.Add(deso.DesoName, (deso.TotalPopulation, deso.VaccinatedMales, deso.VaccinatedFemales,deso.VaccinatedOtherGender));
            }

        }
       
        public void GetSelectedDesosData(DeSoDTO desoDTO)
        {
            SelectedDesos = new Dictionary<string, int>();
            foreach (var deso in desoDTO.Areas)
            {
                SelectedDesos.Add(deso.DesoName, deso.TotalVaccinated);
            }
        }

        /// <summary>
        /// Adds total vaccinated per Region and adds in to a dictionary
        /// </summary>
        /// <param name="desoDTO"></param>
        public void GetNumberOfVaccinatedInCategory(DeSoDTO desoDTO)
        {
            int vaccinatedInCategoryA = 0;
            int vaccinatedInCategoryB = 0;
            int vaccinatedInCategoryC = 0;

            int populationInCategoryA = 0;
            int populationInCategoryB = 0;
            int populationInCategoryC = 0;


            DesoRegionA = new Dictionary<string, int>();
            DesoRegionB = new Dictionary<string, int>();
            DesoRegionC = new Dictionary<string, int>();

            foreach (var deso in desoDTO.Areas)
            {

                string currentDesoCode = deso.Deso;
                if (currentDesoCode.Contains("A"))
                {
               
                        DesoRegionA.Add(deso.DesoName,deso.TotalVaccinated);

                    vaccinatedInCategoryA += deso.TotalVaccinated;
                    populationInCategoryA += deso.TotalPopulation;

                }
                else if (currentDesoCode.Contains("B"))
                {
                    DesoRegionB.Add(deso.DesoName, deso.TotalVaccinated);
                    vaccinatedInCategoryB += deso.TotalVaccinated;
                    populationInCategoryB += deso.TotalPopulation;
                }
                else
                {
                    DesoRegionC.Add(deso.DesoName, deso.TotalVaccinated);
                    vaccinatedInCategoryC+= deso.TotalVaccinated;
                    populationInCategoryC += deso.TotalPopulation;
                }

            }

            VaccinatedInCategories.Add(vaccinatedInCategoryA);
            VaccinatedInCategories.Add(vaccinatedInCategoryB);
            VaccinatedInCategories.Add(vaccinatedInCategoryC);

            PopulationInCategories.Add(populationInCategoryA);
            PopulationInCategories.Add(populationInCategoryB);
            PopulationInCategories.Add(populationInCategoryC);

        }
    }
}
