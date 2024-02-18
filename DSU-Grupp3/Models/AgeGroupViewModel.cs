
using DSU_Grupp3.Controllers;
using DSU_Grupp3.DAL;
using DSU_Grupp3.Models.DTO;
using System;

namespace DSU_Grupp3.Models
{
    public class AgeGroupViewModel
    {  
        public Dictionary<string, int> AllAgeGroups { get; set; }
        public SortedDictionary<int, int> Agegroups { get; set; }


        public AgeGroupViewModel(List<VaccinatedPeopleDTO> vaccinatedPeopleDTO)
        {
            
            AddToAllAgeGroups(vaccinatedPeopleDTO);
            AddToDifferentAgeGroups(vaccinatedPeopleDTO);
        }


        public void AddToAllAgeGroups(List<VaccinatedPeopleDTO> patients)
        {
            AllAgeGroups = new Dictionary<string, int>
            {                
                {"20-24",0 },
                {"25-34",0 },
                {"35-44",0 },
                {"45-54",0 },
                {"55-64",0 },
                {"65-74",0 },
                {"75+",0 },
               
            };     

            foreach (var vaccinationDTO in patients)
            {                
                AllAgeGroups["20-24"] += vaccinationDTO.patients.Count(person => person.age >= 20 && person.age <= 24);
                AllAgeGroups["25-34"] += vaccinationDTO.patients.Count(person => person.age >= 25 && person.age <= 34);
                AllAgeGroups["35-44"] += vaccinationDTO.patients.Count(person => person.age >= 35 && person.age <= 44);
                AllAgeGroups["45-54"] += vaccinationDTO.patients.Count(person => person.age >= 45 && person.age <= 54);
                AllAgeGroups["55-64"] += vaccinationDTO.patients.Count(person => person.age >= 55 && person.age <= 64);
                AllAgeGroups["65-74"] += vaccinationDTO.patients.Count(person => person.age >= 65 && person.age <= 74);
                AllAgeGroups["75+"] += vaccinationDTO.patients.Count(person => person.age >= 75);
            }
        }

        public void AddToDifferentAgeGroups(List<VaccinatedPeopleDTO> patients)
        {
            Agegroups = new SortedDictionary<int, int>();
            foreach (var vaccinationDTO in patients)
            {
                foreach (var person in vaccinationDTO.patients)
                {

                    if (Agegroups.ContainsKey(person.age))
                    {

                        Agegroups[person.age]++;

                    }
                    else
                    {
                        Agegroups.Add(person.age, +1);
                    }
                }
            }
        }
    }
}
