using DSU_Grupp3.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers
{
    [Route("[controller]")]
    public class VaccinationStatsController : ControllerBase
    {
        private readonly IDbRepository _dbRepo;
        private readonly IApiRepository _apiRepo;
        public VaccinationStatsController(IDbRepository dbRepo, IApiRepository apiRepo)
        {
            _dbRepo = dbRepo;
            _apiRepo = apiRepo;
        }

        [HttpGet("GetTotalVaccineDoses")]
        public async Task<IActionResult> GetTotalVaccineDoses()
        {
            var batchesInformation = await _apiRepo.GetBatchesInformation();
            var totalVaccineDoses = batchesInformation.Batches.Sum(b => b.Totaluses);

            return Ok(totalVaccineDoses);
        }

        [HttpGet("GetTotalVaccinatedPeople")]
        public async Task<IActionResult> GetTotalVaccinatedPeople()
        {
            var numberOfPeopleWithOneDose = 0;
            var amountOfVaccinesPerDeso = await _apiRepo.GetVaccinationCounts();
            foreach (var vaccineCount in amountOfVaccinesPerDeso.data)
            {
                foreach(var vaccine in vaccineCount.data)
                {
                    if (vaccine.DoseNumber == 1)
                    {
                        numberOfPeopleWithOneDose += vaccine.Count;
                    }
                }
            }
            
            return Ok(numberOfPeopleWithOneDose);
        }

        [HttpGet("GetTotalPopulationVaccinatedInPercent")]
        public async Task<IActionResult> GetTotalPopulationVaccinatedInPercent()
        {
            var totalPopulation = await _apiRepo.GetPopulationNumbersInOstersund();
            var vaccinatedPeople = await GetTotalVaccinatedCount();
            
            var percentVaccinated = (double)vaccinatedPeople / int.Parse(totalPopulation.Data[0].Values[0]) * 100;
            double roundedPercent = Math.Round(percentVaccinated, 0);
            return Ok(roundedPercent);
        }

        private async Task<int> GetTotalVaccinatedCount()
        {
            var numberOfPeopleWithOneDose = 0;
            var amountOfVaccinesPerDeso = await _apiRepo.GetVaccinationCounts();
            foreach (var vaccineCount in amountOfVaccinesPerDeso.data)
            {
                foreach(var vaccine in vaccineCount.data)
                {
                    if (vaccine.DoseNumber == 1)
                    {
                        numberOfPeopleWithOneDose += vaccine.Count;
                    }
                }
            }
            return numberOfPeopleWithOneDose;
        }
    }
}
