using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Test_LINQ;

namespace TestConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                var a = Guid.NewGuid();
            }
            //Console.WindowHeight = 60;
            //Random rnd = new Random();
            //var cityCommonList = new List<City>();
            //for (var i = 1; i < 10; i++)
            //{
            //    var city = new City() {Id = i, Name = "City" + i};
            //    cityCommonList.Add(city);
            //}
            //var clinicList = new List<Clinic>();
            //for (var i = 1; i < 9; i++)
            //{
            //    var cityList = new List<City>();
            //    var uniqueCityList = new List<City>();
            //    var clinic = new Clinic();
            //    clinic.Id = i;
            //    for(var j = 1; j < rnd.Next(5) + 4; j++)
            //    {
            //        var city = new City();
            //        city = cityCommonList[rnd.Next(9)];
            //        cityList.Add(city);
            //        uniqueCityList = cityList.Distinct().OrderBy(x => x.Id).ToList();
            //    }
            //    clinic.City = uniqueCityList;
            //    clinicList.Add(clinic);
            //}
            //PrintClinic(clinicList);
            //Console.WriteLine("\nInput cities Id\n");
            //var str = Console.ReadLine();
            //var inputList = new List<int>();
            //for (var i = 0; i < str.Length; i++)
            //{
            //    inputList.Add((int)char.GetNumericValue(str[i]));
            //}
            //foreach (var id in inputList)
            //{
            //    Console.Write(id + " ");
            //}
            //Console.WriteLine();

            var resultList = new List<City>();
            //resultList = clinicList.Where(x=> x.City.Any(y => inputList.Contains(y.Id))).ToList();

            //PrintClinic(resultList);
            //foreach (var clinic in clinicList)
            //{
            //    var aaa = clinic.City.Where(x => inputList.Contains(x.Id)).ToList();
            //    resultList =aaa;
            //}

            var spec1 = new ClinicSpecialization() {Id = 1, Name = "Spec1"};
            var spec2 = new ClinicSpecialization() { Id = 2, Name = "Spec2" };
            var spec3 = new ClinicSpecialization() { Id = 3, Name = "Spec3" };
            var spec4 = new ClinicSpecialization() { Id = 4, Name = "Spec4" };
            var spec5 = new ClinicSpecialization() { Id = 5, Name = "Spec5" };
            var clinic1 = new Clinic() {Id=1, ClinicSpecializations = new List<ClinicSpecialization>() { spec1, spec2, spec3 } };
            var clinic2 = new Clinic() { Id = 2, ClinicSpecializations = new List<ClinicSpecialization>() { spec2, spec3, spec4 } };
            var clinic3 = new Clinic() { Id = 3, ClinicSpecializations = new List<ClinicSpecialization>() { spec3, spec4, spec5 } };
            var clinic4 = new Clinic() { Id = 4, ClinicSpecializations = new List<ClinicSpecialization>() { spec1, spec3, spec4 } };
            var clinic5 = new Clinic() { Id = 5, ClinicSpecializations = new List<ClinicSpecialization>() { spec1, spec4, spec5 } };
            var clinicList = new List<Clinic>() {clinic1, clinic2, clinic3, clinic4, clinic5};

            var searchList = new List<int> {3, 4};

            var searchResult = new List<Clinic>();
            foreach (var clinic in clinicList)
            {
                var isSubset = !searchList.Except(clinic.ClinicSpecializations.Select(x => x.Id)).Any();
                if (isSubset)
                {
                    searchResult.Add(clinic);
                }
            }
            var qqq = clinicList.Where(x => !searchList.Except(x.ClinicSpecializations.Select(y => y.Id)).Any()).ToList();
            var bbb = searchResult.Count;
        }

        public static void PrintClinic(List<Clinic> cliniList)
        {
            //Console.WriteLine();
            //foreach (var clinic in cliniList)
            //{
            //    Console.Write(clinic.Id);
            //    foreach (var city in clinic.City)
            //    {
            //        Console.WriteLine("\t" + city.Id + " " + city.Name);
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
