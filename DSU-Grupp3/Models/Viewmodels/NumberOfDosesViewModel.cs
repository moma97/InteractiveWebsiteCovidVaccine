
using DSU_Grupp3.Models.DTO;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.PortableExecutable;

namespace DSU_Grupp3.Models.Viewmodels;

public class NumberOfDosesViewModel
{
    public IEnumerable<int> TotalDoses { get; set; }
    public IEnumerable<string > NumberOfDoses { get; set; }
    public IEnumerable<int> TotalPersonDoses { get; set; }
    public IEnumerable<string> DoseNumberPerson { get; set; }

    public NumberOfDosesViewModel(VaccinationCountsDTO allVaccinationDoses, List<VaccinatedPeopleDTO> vaccinatedPeople)
    {
        CountDoses(allVaccinationDoses);
        CountPersonDoses(vaccinatedPeople);
    }
    public void CountDoses(VaccinationCountsDTO allVaccinations)
    {
        var vaccines = new Dictionary<string, int> {
            { "1", 0 },
            { "2", 0},
            { "3", 0 },
            { "4", 0 },
            { "5", 0 },
            { "6", 0 },
            { "7", 0 },
            { "8", 0 },
        };

        vaccines = Enumerable.Range(1, 8)
        .ToDictionary(
        doseNumber => doseNumber.ToString(),
        doseNumber => allVaccinations.data
        .SelectMany(patient => patient.data)
        .Where(countDose => countDose.DoseNumber ==doseNumber)
        .Sum(count => count.Count)
        );

        NumberOfDoses = vaccines.Keys;
        TotalDoses = vaccines.Values;

    }

    public void CountPersonDoses(List<VaccinatedPeopleDTO> vaccinatedPeople)

    {
        var vaccines = new Dictionary<string, int> {
            { "1", 0 },
            { "2", 0},
            { "3", 0 },
            { "4", 0 },
            { "5", 0 },
            { "6", 0 },
            { "7", 0 },
            { "8", 0 },

        };
        vaccines = Enumerable.Range(1, 8)
        .ToDictionary(
         doseNumber => doseNumber.ToString(),
         doseNumber => vaccinatedPeople
             .SelectMany(person => person.patients)
             .Count(doses => doses.vaccinations.Length == doseNumber)
                    );

        TotalPersonDoses = vaccines.Values;
        DoseNumberPerson = vaccines.Keys;

    } 
}
