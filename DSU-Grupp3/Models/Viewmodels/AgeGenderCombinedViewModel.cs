using DSU_Grupp3.Models.DTO;

namespace DSU_Grupp3.Models.Viewmodels
{
    public class AgeGenderCombinedViewModel
    {
        
        
        public Dictionary<string, Dictionary<string, int>> VaccinatedByAgeAndGender { get; set; }
        public Dictionary<string, Dictionary<string, int>> TotalPopulationByAgeAndGender { get; set; }

        public List<VaccinatedPeopleDTO> vaccinatedPeopleDTO;
        public PopulationNumbersMunicipality populationNumbersMunicipality;

        


        public AgeGenderCombinedViewModel(List<VaccinatedPeopleDTO> vaccinatedPeopleDTOs, PopulationNumbersMunicipality populationNumbersMunicipalityDto)
        {
            vaccinatedPeopleDTO = vaccinatedPeopleDTOs;
            populationNumbersMunicipality = populationNumbersMunicipalityDto;
            
            VaccinatedByAgeAndGender = new Dictionary<string, Dictionary<string, int>>()
            {
                {"20-24", new Dictionary<string, int>()},
                {"25-34", new Dictionary<string, int>()},
                {"35-44", new Dictionary<string, int>()},
                {"45-54", new Dictionary<string, int>()},
                {"55-65", new Dictionary<string, int>()},
                {"66-75", new Dictionary<string, int>()},
                {"75+", new Dictionary<string, int>()}
            };
            TotalPopulationByAgeAndGender = new Dictionary<string, Dictionary<string, int>>()
            {
                {"20-24", new Dictionary<string, int>()},
                {"25-34", new Dictionary<string, int>()},
                {"35-44", new Dictionary<string, int>()},
                {"45-54", new Dictionary<string, int>()},
                {"55-65", new Dictionary<string, int>()},
                {"66-75", new Dictionary<string, int>()},
                {"75+", new Dictionary<string, int>()}
            };



            CountTotalPopulationByAgeAndGender();
            CountVaccinatedPopulationByAgeAndGender();
            
            
        }

        public void CountVaccinatedPopulationByAgeAndGender()
        {
           
            foreach (var patient in vaccinatedPeopleDTO)
            {
                foreach (var person in patient.patients)
                {
                    var ageGroup = GetAgeGroup(person.age);
                    var gender = GetGender(person.gender);
                    if (!string.IsNullOrEmpty(ageGroup))
                    {

                        if (!VaccinatedByAgeAndGender.ContainsKey(ageGroup))
                        {
                            VaccinatedByAgeAndGender[ageGroup] = new Dictionary<string, int>();
                        }
                        if (!VaccinatedByAgeAndGender[ageGroup].ContainsKey(gender))
                        {
                            VaccinatedByAgeAndGender[ageGroup][gender] = 1;
                        }
                        else
                        {
                            VaccinatedByAgeAndGender[ageGroup][gender]++;
                        }
                    }
                }
            }

            
            
        }

        public void CountTotalPopulationByAgeAndGender()
        {
            foreach (var item in populationNumbersMunicipality.Data)
            {
                var ageGroup = GetAgeGroup(item.Key[1]);
                var gender = GetGender(item.Key[2]);
                if (!string.IsNullOrEmpty(ageGroup))
                {

                    if (!TotalPopulationByAgeAndGender.ContainsKey(ageGroup))
                    {
                        TotalPopulationByAgeAndGender[ageGroup] = new Dictionary<string, int>();
                        
                    }
                    if (!TotalPopulationByAgeAndGender[ageGroup].ContainsKey(gender))
                    {
                        TotalPopulationByAgeAndGender[ageGroup][gender] = int.Parse(item.Values[0]);
                    }
                    else
                    {
                        TotalPopulationByAgeAndGender[ageGroup][gender] += int.Parse(item.Values[0]);
                    }
                }
            }
        }

        public string GetAgeGroup(int age)
        {
            if (age >= 20 && age <= 24) return "20-24";
            if (age >= 25 && age <= 34) return "25-34";
            if (age >= 35 && age <= 44) return "35-44";
            if (age >= 45 && age <= 54) return "45-54";
            if (age >= 55 && age <= 65) return "55-65";
            if (age >= 66 && age <= 75) return "66-75";
            if (age > 75) return "75+";
            return "";
        }
        public string GetAgeGroup(string ageString)
        {
            int age;
            if (int.TryParse(ageString, out age))
            {
                if (age >= 20 && age <= 24) return "20-24";
                if (age >= 25 && age <= 34) return "25-34";
                if (age >= 35 && age <= 44) return "35-44";
                if (age >= 45 && age <= 54) return "45-54";
                if (age >= 55 && age <= 65) return "55-65";
                if (age >= 66 && age <= 75) return "66-75";
                if (age > 75) return "75+";                
            }
            else
            {
                if (ageString == "100+") return "75+";
            }
            
           return "";
        }
        public string GetGender(string gender)
        {
            if (gender == "1" || gender == "Male") return "Male";
            if ((gender == "2" || gender != "Male")) return "Female";
            return "";

        }
    }
}
