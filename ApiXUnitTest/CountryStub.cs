using System;
using ApiAplication.Domain.DTO;
namespace ApiXUnitTest
{
    public static class CountryStub
    {
        public static IList<Country> countriesOk = new List<Country>() { new Country() {
            name=new Name(){common="Colombia"},
            area=1141748,
            population=50882884,
            error=""
        } };

        public static IList<Country> countriesFail = new List<Country>() { new Country() {
            name=null,
            area=0,
            population=0,
            error="No se encontro la busqueda",
        } };

        public static IList<Log> logs = new List<Log>() { new Log() {
            Id=1,
            Descripcion="descripcion",
            Fecha=DateTime.Now,
        } };
        public static IList<Country> response = new List<Country>() { new Country()
            {
                name = new Name() { common = "Colombia" },
                area = 1141748,
                population = 50882884,
                error = ""

            }
        };

    }
}

