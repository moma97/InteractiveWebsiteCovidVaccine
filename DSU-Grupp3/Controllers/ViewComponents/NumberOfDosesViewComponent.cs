using DSU_Grupp3.DAL;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers.ViewComponents
{
    public class NumberOfDosesViewComponent : ViewComponent
    {
        private readonly IApiRepository _repo;

        public NumberOfDosesViewComponent(IApiRepository apiRepository)
        {
            _repo = apiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var numberOfDosesperPerson = await _repo.GetVaccinatedPeople();
            var numberOfDoses = await _repo.GetVaccinationCounts();
            var model = new NumberOfDosesViewModel(numberOfDoses, numberOfDosesperPerson);
            return View(model);
        }
    }
}
