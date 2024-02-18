using DSU_Grupp3.Models.DTO;

namespace DSU_Grupp3.Models.Viewmodels
{
    public class VaccineProgressViewModel
    {   
        public Dictionary<int , Dictionary<int, double>> VaccinatedPeopleByYear = new Dictionary<int, Dictionary<int, double>>();
        public Dictionary<int, Dictionary<int, double>> VaccinatedPeopleByYearInDeso { get; set; } = new Dictionary<int, Dictionary<int, double>>();
        public Dictionary<string, string> SelectedDeso { get; set; }
        public List<VaccinatedPeopleDTO> AllVaccinatedPeople { get; set; }
        public List<VaccinatedPeopleDTO> VaccinatedPeopleInOneDeso { get; set; }

        public IEnumerable<string> DesoNames { get; set; }
        public double[] ArrayOfPercent { get; set; }

        public string ChosenDesoCode {  get; set; }

        public double NewVaccinated = 0;
       
        public int TotalPopulation { get; set; }
             
        public DeSoDTO DesoData { get; set; }


        public VaccineProgressViewModel(List<VaccinatedPeopleDTO> vaccinated, DeSoDTO deSoDTO)
        {
            InitializeMonthlyCounts();
            AllVaccinatedPeople = vaccinated;
            DesoNames = deSoDTO.Areas.Select(x => x.DesoName);
            TotalPopulation = deSoDTO.Areas.Sum(area => area.TotalPopulation);
            GoThroughEachYearInDictionary(vaccinated);
            GetCorrespondingDesoCodeToDesoName(deSoDTO);
            ArrayOfPercent = GetPercentIncrease(TotalPopulation);
        }
        public VaccineProgressViewModel(List<VaccinatedPeopleDTO> PeopleInOneDeso, DeSoDTO desoDTO, int test)
        {
            InitializeMonthlyCounts();
            DesoData = desoDTO;
            VaccinatedPeopleInOneDeso = PeopleInOneDeso;
            var totalPopulationForSingleDeso = GetPopulationForSingleDeso();
            GoThroughEachYearInDictionary(PeopleInOneDeso);           
            ArrayOfPercent = GetPercentIncrease(totalPopulationForSingleDeso);
        }

        
        public int GetPopulationForSingleDeso()
        {
            var selectedDesoCode = VaccinatedPeopleInOneDeso[0].meta.desocode;
            int populationInDeso = 0;

            for (int i = 0; i < DesoData.Areas.Count; i++)
            {
                if (DesoData.Areas[i].Deso == selectedDesoCode)
                {
                    populationInDeso = DesoData.Areas[i].TotalPopulation;                  
                }
            }
            return populationInDeso;
        }

        /// <summary>
        /// Populates the dictionary with DesoNames and the corresponding DesoCode
        /// </summary>
        /// <param name="desoDTO"></param>
        public void GetCorrespondingDesoCodeToDesoName(DeSoDTO desoDTO)
        {
            SelectedDeso = new Dictionary<string, string>();
            foreach (var deso in desoDTO.Areas)
            {
                SelectedDeso.Add(deso.DesoName, deso.Deso);
            }
        }


        private void InitializeMonthlyCounts()
        {
            for (int year = 2020; year <= 2024 ; year++)
            {
                VaccinatedPeopleByYear[year] = new Dictionary<int, double>();
                VaccinatedPeopleByYearInDeso[year] = new Dictionary<int, double>();

                for (int month = 1; month <= 12; month++)
                {
                    VaccinatedPeopleByYear[year][month] = 0;
                    VaccinatedPeopleByYearInDeso[year][month] = 0;
                } 
            }
        }

        public void GoThroughEachYearInDictionary(List<VaccinatedPeopleDTO> allVaccinatedInAllAreas)
        {
            foreach (int year in VaccinatedPeopleByYear.Keys)
            {
                AddVaccinatedPeopleToDictionary(allVaccinatedInAllAreas, year);                
            }
        }

        public void AddVaccinatedPeopleToDictionary(List<VaccinatedPeopleDTO> allVaccinatedInAllAreas, int year)
        {   
            
            if (!VaccinatedPeopleByYear.ContainsKey(year))
            {
                VaccinatedPeopleByYear[year] = new Dictionary<int, double>();
                VaccinatedPeopleByYearInDeso[year] = new Dictionary<int, double>();
            }
            var innerDictionary = VaccinatedPeopleByYear[year];
            var innerDictionaryDeso = VaccinatedPeopleByYearInDeso[year];

            foreach (var area in allVaccinatedInAllAreas)
            {
                foreach (var patient in area.patients)
                {
                     foreach (var vaccination in patient.vaccinations)
                     {                       
                        if (vaccination.dosenumber == 1 && vaccination.dateofvaccination.Year == year)
                        {
                            int month = vaccination.dateofvaccination.Month;
                            if (!innerDictionary.ContainsKey(month))
                            {
                                innerDictionary[month] = 0;
                                innerDictionaryDeso[month] = 0;
                            }
                            innerDictionary[month]++;
                            innerDictionaryDeso[month] ++;
                        }
                    }
                }
            }           
        }


        public double[] GetPercentIncrease(int totalPopulation)
        {         
            double newVaccinatedPeople = 0;
            List<double> listOfPercent = new List<double>();

            foreach (var year in VaccinatedPeopleByYear.Keys)
                foreach (var month in VaccinatedPeopleByYear[year])
                    
                    {
                        newVaccinatedPeople += month.Value;

                        var percent = newVaccinatedPeople / totalPopulation * 100;
                        listOfPercent.Add(Math.Round(percent,2));
                                            
                    }
            return listOfPercent.ToArray();
        }


       
    }
}
