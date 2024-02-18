using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.DTO;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Routing.Constraints;

using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace DSU_Grupp3.DAL
{
    public class ApiRepository : IApiRepository
    {
        private readonly IDbRepository _idbRepository;
        string baseUrl = "https://grupp1.dsvkurs.miun.se/api/";
        string baseUrlSCB = "https://api.scb.se/OV0104/v1/doris/sv/ssd/START/";
        string folderPath = "Cache";
        bool needsUpdate = false;

        public ApiRepository(IDbRepository idbRepository)
        {
            _idbRepository = idbRepository;
        }
        

        /// <summary>
        /// Returns object of Batches that contains: Batch-number, vaccine-name, manufacturer, total-uses
        /// </summary>
        /// <returns></returns>
    public async Task<VaccineBatchesDTO> GetBatchesInformation()
    {
          string fileName = "vaccineBatches.json";
          var filePath = Path.Combine(folderPath, fileName);
          if (!needsUpdate && File.Exists(filePath))
          {
              VaccineBatchesDTO batchesData = Open<VaccineBatchesDTO>(filePath);
            return batchesData;
          }
          else
          {
              var vaccineBatches = await ApiEngine.Fetch<VaccineBatchesDTO>(baseUrl + "batches");
              Save(vaccineBatches.Data, fileName);
                return vaccineBatches.Data;
          }                
    }
       

        /// <summary>
        /// Returns object of DeSO Areas that contains: Region-code, name, municipality, municipality-code, deso, deso-name, 
        /// </summary>
        /// <returns></returns>
        public async Task<DeSoDTO> GetDeSoInformation()
        {
            string fileName = "deSoInformation.json";
            var filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                DeSoDTO desoData = Open<DeSoDTO>(filePath);
                return desoData;               
            }
            else
            {
                var desoInfo = await ApiEngine.Fetch<DeSoDTO>(baseUrl + "deso");
                Save(desoInfo.Data, fileName);
                return desoInfo.Data; 
            }                                      
        }

        /// <summary>
        /// Returns an array of Vaccination sites that contains: Id, name of site, streetAddress, postalnumber, city, latitude, longitude
        /// </summary>
        /// <returns></returns>
        public async Task<List<VaccinationSitesDTO>> GetSitesInformation()
        {
            string fileName = "vaccinationSites.json";
            var filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
               SitesData sitesData = Open<SitesData>(filePath);
                return sitesData.DataList;
            }
            else
            {
                var sitesInfo = await ApiEngine.FetchArray<VaccinationSitesDTO>(baseUrl + "sites");
               
                 Save(sitesInfo, fileName);
                 return sitesInfo.DataList;                           
            }                       
        }

        /// <summary>
        /// Returns an object of Vaccionationcounts that contains: per deso: dose-number 1-7, count of each dose. 
        /// </summary>
        /// <returns></returns>
        public async Task<VaccinationCountsDTO> GetVaccinationCounts()
        {
            string fileName = "vaccineCountInfo.json";
            var filePath = Path.Combine(folderPath, fileName);
            if (!needsUpdate && File.Exists(filePath))
            {
                VaccineCountData vaccineCountData = Open<VaccineCountData>(filePath);
                return vaccineCountData.Data;
            }
            else
            {
                var vaccineCountInfo = await ApiEngine.Fetch<VaccinationCountsDTO>(baseUrl + "vaccinations/count");
                Save(vaccineCountInfo, fileName);
                return vaccineCountInfo.Data;
            }                    
        }

        public async Task<List<VaccinatedPeopleDTO>> GetVaccinatedPeopleInOneDeso(string deSOCode)
        {
            List<VaccinatedPeopleDTO> allVaccinatedPeopleList = new();
            string fileName = ($"vaccinatedPeopleIn{deSOCode}.json");
            var filePath = Path.Combine(folderPath, fileName);
            var deSoCode = deSOCode;
            if (File.Exists(filePath))
            {
                List<VaccinatedPeopleDTO> vaccinatedPeople = Open<List<VaccinatedPeopleDTO>>(filePath);
                return vaccinatedPeople;
            }
            else
            {
                var desoArea = await ApiEngine.Fetch<VaccinatedPeopleDTO>($"{baseUrl}vaccinations/{deSoCode}");
                allVaccinatedPeopleList.Add(desoArea.Data);

                Save(allVaccinatedPeopleList, fileName);
                return allVaccinatedPeopleList;
            }

        }
       

        /// <summary>
        /// Returns a list of VaccinatedPeopleDTO for each deso that contains: Age, gender, list of vaccinations: DateOfLatestUpdate, batch, place of injection, dose, site info
        /// </summary>
        /// <returns></returns>
        public async Task<List<VaccinatedPeopleDTO>> GetVaccinatedPeople()
        {
            List<VaccinatedPeopleDTO> allVaccinatedPeopleList = new();
            string fileName = "allVaccinatedPeopleList.json";
            var filePath = Path.Combine(folderPath, fileName);
            var deSoCodes = GetDeSoInformation();

            if (!needsUpdate && File.Exists(filePath))
            {
              List<VaccinatedPeopleDTO> vaccinatedPeople = Open<List<VaccinatedPeopleDTO>>(filePath);
              return vaccinatedPeople;
            }               
            else
            {
              var tasks = deSoCodes.Result.Areas
                    .Select(area => $"{baseUrl}vaccinations/{area.Deso}")
                    .Select(url => ApiEngine.Fetch<VaccinatedPeopleDTO>(url));


              var arrayOfVaccinatedPeople = await Task.WhenAll(tasks);

              arrayOfVaccinatedPeople.ToList().ForEach(desoArea =>
              {
                           allVaccinatedPeopleList.Add(desoArea.Data);
              });

              Save(allVaccinatedPeopleList, fileName);
              return allVaccinatedPeopleList;
            }          
        }
        #region Uppdatera MostRecentlyUpdated


        /// <summary>
        /// Updates the json cache with the most recent data from the API and saves the time of the update in a separate file
        /// </summary>
        /// <returns></returns>
        private async Task UpdateJsonCache()
        {
            await GetVaccinationCounts();
            await GetVaccinatedPeople();
            await GetBatchesInformation();
            await GetTotalCombinedDesoData();

            needsUpdate = false;        
            
            Save(DateTime.Now, "timeUpdated.json");
        }

        /// <summary>
        /// Checks if the data is older than 2 hours and run the method UpdateVaccinationData if it is
        /// </summary>
        /// <returns></returns>
        public async Task UpdateScheduled()
        {   
            string filename = "timeUpdated.json";
            var filePath = Path.Combine(folderPath, filename);
            if (!File.Exists(filePath))
            {
                Save(DateTime.Now, filename);
            }
            var lastUpdated = Open<DateTime>(filePath);
            if (lastUpdated.AddHours(2) < DateTime.Now)
            {
                await UpdateVaccinationData();
            }
        }

        /// <summary>
        /// Checks if the saved json files are older than data from the API and updates the json files if they are
        /// </summary>
        /// <returns></returns>
        public async Task UpdateVaccinationData()
        {
            string fileName = "vaccineCountInfo.json";

            DateTime lastUpdateFromJson = GetMostRecentUpdateFromJson(fileName);
            DateTime lastUpdateFromApi = await GetMostRecentUpdateFromApi();
            
            if (lastUpdateFromApi > lastUpdateFromJson && needsUpdate)
            {
                needsUpdate = true;
                await UpdateJsonCache();
                
            }
            else
            {
                needsUpdate = false;
            }            
        }

        /// <summary>
        /// Returns the most recent update from the json file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DateTime GetMostRecentUpdateFromJson(string fileName)
        {
            var filePath = Path.Combine(folderPath, fileName);
            if(File.Exists(filePath))
            {
                VaccineCountData vaccineCountData = Open<VaccineCountData>(filePath);
                if (vaccineCountData.Data != null)
                {
                    return vaccineCountData.Data.meta.LatestChanges
                    .Max(x => x.latestChange);
                }
                
            }
            //TODO error handling
            return DateTime.Now.AddDays(-2);
        }

        /// <summary>
        /// Returns the most recent update from the API
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetMostRecentUpdateFromApi()
        {
            try
            {
                var response = await ApiEngine.Fetch<VaccinationCountsDTO>(baseUrl + "vaccinations/count");
                if (response.Data != null)
                {
                    var latestChange = response.Data.meta.LatestChanges;
                    var mostRecentUpdateApi = latestChange.Max(x => x.latestChange);
                    needsUpdate = true;
                    return mostRecentUpdateApi;

                }
            }
            catch (Exception)
            {

                needsUpdate = false;
            }         
            return DateTime.Now.AddDays(2);       
        }
        #endregion 


        /// <summary>
        /// Returns an object of DeSoDTO that contains all data from all DesoAreas: Region-code, name, municipality, municipality-code, deso, deso-name, population, vaccinated
        /// </summary>
        /// <returns></returns>
        public async Task<DeSoDTO> GetTotalCombinedDesoData()
        {
            
            string filename = "TotalCombinedDesoData.json";            
            string scbDesoAgeGenderYear = "BE/BE0101/BE0101Y/FolkmDesoAldKonN";
            var filePath = Path.Combine(folderPath, filename);

            if (!needsUpdate && System.IO.File.Exists(filePath))
            {                        
                var desoData = Open<DeSoDTO>(filePath);
                return desoData;
            }

            else
            {
                var desoAreas = await GetDeSoInformation();

                var desoList = "";
                foreach (var deso in desoAreas.Areas)
                {
                    desoList += deso.Deso + ",";
                }
                desoList = desoList.TrimEnd(',', ' ');

                QueryParameters queryParameters = new QueryParameters
                {
                    DeSoCodes = desoList.Split(',').ToList(),

                    Ages = new List<string>
                    {
                        "totalt"
                    },

                    Genders = new List<string>

                    {   "1",
                        "2",
                        "1+2"
                    },

                    Years = new List<string>
                    {
                        "2022"
                    }
                };

                var query = new QueryRequest(queryParameters);
                string queryToSCB = System.Text.Json.JsonSerializer.Serialize(query);

                var populationInDeso = await ApiEngine.FetchScb<PopulationNumbersDeSo>(baseUrlSCB + scbDesoAgeGenderYear, queryToSCB);
                foreach (var deso in desoAreas.Areas)
                {
                    foreach (var population in populationInDeso.Data.Data)
                    {
                        if (deso.Deso == population.Key[0])
                        {
                            if (population.Key[2] == "1")
                            {
                                deso.PopulationMales = int.Parse(population.Values[0]);
                            }
                            if (population.Key[2] == "2")
                            {
                                deso.PopulationFemales = int.Parse(population.Values[0]);
                            }
                            if (population.Key[2] == "1+2")
                            {
                                deso.TotalPopulation = int.Parse(population.Values[0]);
                            }
                        }

                    }
                }

                var vaccinationCounts = await GetVaccinationCounts();

                foreach (var deso in desoAreas.Areas)
                {
                    foreach (var vaccination in vaccinationCounts.data)
                    {
                        if (deso.Deso == vaccination.Deso)
                        {
                            deso.TotalVaccinated = vaccination.data[0].Count;
                        }
                    }
                }

                var vaccinatedPeople = await GetVaccinatedPeople();

                foreach (var deso in desoAreas.Areas)
                {
                    foreach (var vaccinated in vaccinatedPeople)
                    {
                        if (vaccinated.meta.desocode == deso.Deso)
                        {
                            foreach (var patient in vaccinated.patients)
                            {
                                if (patient.gender == "Male")
                                {
                                    deso.VaccinatedMales++;
                                }
                                if (patient.gender == "Female")
                                {
                                    deso.VaccinatedFemales++;
                                }
                                if (patient.gender == "Other")
                                {
                                    deso.VaccinatedOtherGender++;
                                }

                            }
                        }

                    }
                }
                if (populationInDeso != null)
                {
                    Save(desoAreas, filename);
                    await _idbRepository.SaveDeSoDTO(desoAreas);
                    return desoAreas;
                }
                else
                {
                    var desoData = Open<DeSoDTO>(filePath);
                    return desoData;
                }
            }
        }
        #region API-Calls to SCB

        /// <summary>
        /// Get total population numbers in municipality for ages 0-100+ and for both genders
        /// </summary>
        /// <returns></returns>
        public async Task<PopulationNumbersMunicipality> GetTotalPopulationNumbersInMunicipality()
        {
            
            string scbRegionAgeGenderYear = "BE/BE0101/BE0101A/BefolkningNy";
            string filename = "totalPopulationNumbersInMunicipality.json";
            var filePath = Path.Combine(folderPath, filename);

            if (System.IO.File.Exists(filePath))
            {
                return Open<PopulationNumbersMunicipality>(filePath);
            }
            else
            {
                
                IEnumerable<string> ageRangeEnumerable = Enumerable.Range(0, 100).Select(i => i.ToString());
                List<string> ageRange = ageRangeEnumerable.ToList();
                if (ageRange.Count >= 100)
                {
                    ageRange.Add("100+");
                }

                QueryParameters queryParameters = new QueryParameters()
                {
                MunicipalityCode = new List<string>
                {
                    "2380"
                },
                MunicipalityFilter = "vs:RegionKommun07",
                AgeFilter = "vs:Ålder1årA",
                Ages = ageRange.ToList(),

                Genders = new List<string>
                {
                    "1",
                    "2"

                },
                ContentsCode = new List<string>
                {
                    "BE0101N1"
                },
                Years = new List<string>
                {
                     "2022"
                }

                };
                var query = new QueryRequest(queryParameters);
                string queryToSCB = System.Text.Json.JsonSerializer.Serialize(query);

                var totalPopulationMunicipality = await ApiEngine.FetchScb<PopulationNumbersMunicipality>(baseUrlSCB + scbRegionAgeGenderYear, queryToSCB);

                Save(totalPopulationMunicipality.Data, filename);

                return totalPopulationMunicipality.Data;
            }
        }

        /// <summary>
        /// Returns an object of PopulationNumbersMunicipality that contains: municipality-cod and population in Östersund
        ///</summary>                
        public async Task<PopulationNumbersMunicipality> GetPopulationNumbersInOstersund()
        {
            // Anropssträng till SCB för Folkmängd efter region, ålder, kön och år
            string scbPopulation = "/BE/BE0101/BE0101A/FolkmangdNov";
            string filename = "populationNumberInOstersund.json";
            var filePath = Path.Combine(folderPath, filename);

            if (System.IO.File.Exists(filePath))
            {
                return Open<PopulationNumbersMunicipality>(filePath);
            }
            else
            {

                QueryParameters queryParameters = new QueryParameters()
                {
                    MunicipalityCode = new List<string>
                    {
                      "2380"
                    },
                    MunicipalityFilter = "vs:RegionKommun07",
                    Years = new List<string>
                    {
                      "2022"
                    }
                };

                var query = new QueryRequest(queryParameters);
                string queryToSCB = System.Text.Json.JsonSerializer.Serialize(query);
                var totalPopulationOstersund = await ApiEngine.FetchScb<PopulationNumbersMunicipality>(baseUrlSCB + scbPopulation, queryToSCB);
                Save(totalPopulationOstersund.Data, filename);
                return totalPopulationOstersund.Data;
            }            
        }

        #endregion

        /// <summary>
        /// Save to json
        /// </summary>
        /// <param name="objectToSave"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(object objectToSave, string filename)
        {
            try
            {   Directory.CreateDirectory(folderPath);
                var filePath = Path.Combine(folderPath, filename);
                if (objectToSave != null)
                {

                    string json = JsonConvert.SerializeObject(objectToSave, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                    return true;
                }
              
                
                Console.WriteLine(ErrorMessages.CannotSaveToJson);
                return false;
            }
            catch (Exception)
            {
                Console.WriteLine(ErrorMessages.ErrorSavingToJson);
                return false;
            }            
        }

        /// <summary>
        /// Open json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public T Open<T>(string filePath)
        {
            try
            {
                string data = File.ReadAllText(filePath);            
                if (data != null)
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    Console.WriteLine(ErrorMessages.JsonNull);
                    return default(T);
                }

            }         
            catch (Exception)
            {
                Console.WriteLine(ErrorMessages.ErrorReadingJson);
                return default(T);
            }
        }
    }

}
