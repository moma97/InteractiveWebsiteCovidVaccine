using DSU_Grupp3.DAL;
using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.DTO;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace DSU_Grupp3.Controllers
{
    public class AgeGenderCombinedViewComponent : ViewComponent
    {
        private readonly IApiRepository _repo;
        public AgeGenderCombinedViewComponent(IApiRepository repo)
        {
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vaccinatedPeople = await _repo.GetVaccinatedPeople();
            var totalPopulation = await _repo.GetTotalPopulationNumbersInMunicipality();     
            var model = new AgeGenderCombinedViewModel(vaccinatedPeople, totalPopulation);
            return View(model);
            
        }            
                       
    }
}
