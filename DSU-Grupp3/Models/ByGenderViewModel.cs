using DSU_Grupp3.DAL;
using DSU_Grupp3.Models.DTO;

namespace DSU_Grupp3.Models
{
    public class ByGenderViewModel
    {       
        protected DeSoDTO DeSoDTO;
        public int FemalesVaccinated => DeSoDTO.Areas.Sum(area => area.VaccinatedFemales);
        public int MalesVaccinated => DeSoDTO.Areas.Sum(area => area.VaccinatedMales);
        public int UnVaccinatedFemales => DeSoDTO.Areas.Sum(area => area.PopulationFemales) - FemalesVaccinated;
        public int UnvaccinatedMales => DeSoDTO.Areas.Sum(area => area.PopulationMales) - MalesVaccinated;
        public int Total => DeSoDTO.Areas.Sum(area => area.TotalPopulation);

        public double FemalesProcentVac => PercentageOfTotal(FemalesVaccinated, UnVaccinatedFemales);
        public double MalesProcentVac => PercentageOfTotal(MalesVaccinated, UnvaccinatedMales);
        public double FemalesProcentUnVac => PercentageOfTotal(UnVaccinatedFemales, FemalesVaccinated);
        public double MalesProcentUnVac => PercentageOfTotal(UnvaccinatedMales, MalesVaccinated);
        public ByGenderViewModel(DeSoDTO desoInformation)
        {
            DeSoDTO = desoInformation;            
        }


        /// <summary>
        /// Calculates how big a portion of the total is and returns the percentage
        /// </summary>
        /// <param name="portion"></param>
        /// <returns></returns>
        public double PercentageOfTotal(int vaccinated, int unvaccinated)
        {
            double totalGender = vaccinated + unvaccinated;            
            double percentage = (vaccinated / totalGender ) * 100;
            return Math.Round(percentage, 2);
        }
       
        



    }
}
