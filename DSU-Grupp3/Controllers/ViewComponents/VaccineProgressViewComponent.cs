using DSU_Grupp3.DAL;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers.ViewComponents
{
    public class VaccineProgressViewComponent : ViewComponent
    {
        private readonly IApiRepository _apiRepository;
        public VaccineProgressViewComponent(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
           
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {            
            var getTotal = await _apiRepository.GetTotalCombinedDesoData();
         
            var listOfAllVaccinatedPeople = await _apiRepository.GetVaccinatedPeople();
            var model = new VaccineProgressViewModel(listOfAllVaccinatedPeople, getTotal);
            return View(model);
        }


     
    }
}
