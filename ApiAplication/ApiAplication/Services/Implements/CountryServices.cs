using System;
using ApiAplication.Domain.DTO;
using ApiAplication.Persistence;
using ApiAplication.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApiAplication.Services.Implements
{
    public class CountryServices : ICountryService
    {
        private readonly ICountryRequesService service;
        private readonly IConfiguration configuration;
        private readonly ILogService logService;
        private string HttpError = string.Empty;

        public CountryServices(ICountryRequesService _service, IConfiguration _configuration, ILogService _logService)
        {
            service = _service;
            configuration = _configuration;
            logService = _logService;
        }

        public async Task<IList<Country>> GetCountryAsync(string country)
        {
            IList<Country> countries = new List<Country>();
            IList<Country> countriesSearch = new List<Country>();
            try
            {
                countries = (await service.GetRequestAsync((configuration["CountryClient:Country"]).ToString().Replace("{name}", country)));
                // .Where(c => c.name.common.Equals(country.ToLower())).ToList();
                countriesSearch = GetSearch(countries, country);
                if (countries.Any())
                {
                    HttpError = logService.SaveLog(country).error;
                }
            }
            catch (Exception ex)
            {
                HttpError = "Error al conectarse al Api Country";
            }



            return EvalSearch(countriesSearch, HttpError);

        }

        private IList<Country> GetSearch(IList<Country> _country, string countrySearch)
        {
            List<Country> country = new List<Country>();

            foreach (Country c in _country)
            {
                if (c.name.common.ToLower().Equals(countrySearch.ToLower()))
                {
                    country.Add(c);
                }
            }

            return country;
        }

        private IList<Country> EvalSearch(IList<Country> countries, string error)
        {
            Country country = new Country();
            if (countries.Count() == 0 && error == null)
            {
                country.error = "No se encontro la busqueda";

            }
            else
            {
                if (countries.Count() == 0 && error.Equals(HttpError))
                {

                    country.error = HttpError;

                }
                else
                {

                    if (countries.Count() > 0)
                    {
                        return countries;
                    }

                }
            }

            countries.Add(country);



            return countries;
        }


    }
}

