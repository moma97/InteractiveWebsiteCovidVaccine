using DSU_Grupp3.DAL;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers
{
    public class DetailStatisticsController : Controller
    {

        private readonly IApiRepository _apiRepository;
        public DetailStatisticsController(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;

        }


        public async Task<IActionResult> DetailStatistics()
        {
            var listOfAllVaccinatedPeople = await _apiRepository.GetVaccinatedPeople();
            var desoInfo = await _apiRepository.GetTotalCombinedDesoData();
            var model = new VaccineProgressViewModel(listOfAllVaccinatedPeople, desoInfo);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> YourAction(string chosenDesoCode)
        {
            int test = 0;
            var desoInfo = await _apiRepository.GetTotalCombinedDesoData();
            var vaccinatedPeopleInOneDeso = await _apiRepository.GetVaccinatedPeopleInOneDeso(chosenDesoCode);
            var model = new VaccineProgressViewModel(vaccinatedPeopleInOneDeso, desoInfo, test);
            
            return Json(new {success = true, model});
        }
    }
}
