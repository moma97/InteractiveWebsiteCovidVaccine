using DSU_Grupp3.Models.DTO;

namespace DSU_Grupp3.DAL
{
    public interface IDbRepository
    {
        
        Task<List<DeSoDTO>> GetDeSoDTO();
        Task SaveDeSoDTO(DeSoDTO desoInformation);
        
    }
}