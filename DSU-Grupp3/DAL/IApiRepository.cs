using DSU_Grupp3.Models.DTO;

namespace DSU_Grupp3.DAL
{
    public interface IApiRepository
    {
        Task<VaccineBatchesDTO> GetBatchesInformation();              
        Task<DeSoDTO> GetDeSoInformation();
       
        Task<DateTime> GetMostRecentUpdateFromApi();
        DateTime GetMostRecentUpdateFromJson(string fileName);
       
        Task<List<VaccinatedPeopleDTO>> GetVaccinatedPeople();
        Task<VaccinationCountsDTO> GetVaccinationCounts();

        Task UpdateVaccinationData();

        Task<PopulationNumbersMunicipality> GetTotalPopulationNumbersInMunicipality();
        Task<DeSoDTO> GetTotalCombinedDesoData();
        Task<List<VaccinationSitesDTO>> GetSitesInformation();
       
        Task<PopulationNumbersMunicipality> GetPopulationNumbersInOstersund();

        Task<List<VaccinatedPeopleDTO>> GetVaccinatedPeopleInOneDeso(string deSOCode);

        Task UpdateScheduled();

    }
}