using DSU_Grupp3.DAL;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers.ViewComponents
{
    public class VaccinationCenterViewComponent : ViewComponent
    {
        private readonly IApiRepository _repo;
        public VaccinationCenterViewComponent(IApiRepository apiRepository)
        {
            _repo = apiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vaccinatedPeople = await _repo.GetVaccinatedPeople();
            var model = new VaccinationCenterViewModel(vaccinatedPeople);
            return View(model);
        }
    }
}
