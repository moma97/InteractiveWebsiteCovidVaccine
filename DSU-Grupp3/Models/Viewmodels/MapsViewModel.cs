using DSU_Grupp3.Models.DTO;

namespace DSU_Grupp3.Models.Viewmodels
{
    public class MapsViewModel
    {
        public Dictionary<string, DesoCodeInformation> DesosDict { get; set; }
        public List<string> DesoCode { get; set; }
        public List<int> TotalVaccinationsPerDeso { get; set; } = new List<int>();
        public List<VaccinationSitesDTO> SitesList { get; set; }
        public MapsViewModel(DeSoDTO desoInformation, List<VaccinationSitesDTO> sites)
        {
            SitesList = sites;
            var desos = new Dictionary<string, DesoCodeInformation>();

            foreach (var deso in desoInformation.Areas)
            {
                desos.Add(deso.Deso, deso);
            }

            DesosDict = desos;
            DesoCode = desos.Keys.ToList();
            for (int i = 0; i < DesosDict.Count; i++)
            {
                TotalVaccinationsPerDeso.Add(1);
            }
            

        }
        public double PercentageOfTotal(int vaccinated, int unvaccinated)
        {
            double totalGender = vaccinated + unvaccinated;
            double percentage = (vaccinated / totalGender) * 100;
            return Math.Round(percentage, 2);
        }
    }
}
