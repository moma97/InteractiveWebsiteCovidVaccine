namespace DSU_Grupp3.Models.DTO
{
    public class SitesData
    {
        public List<VaccinationSitesDTO> DataList { get; set; }
    }

    public class VaccinationSitesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalNumber { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
