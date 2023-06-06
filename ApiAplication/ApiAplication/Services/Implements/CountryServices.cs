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
        private readonly LogDbContext context;
        private string HttpError=string.Empty;
        public CountryServices(ICountryRequesService _service, IConfiguration _configuration, LogDbContext _logDbContext)
        {
            service = _service;
            configuration = _configuration;
            context = _logDbContext;
        }

        public async Task<IList<Country>> GetCountryAsync(string country)
        {
            IList<Country> countries = new List<Country>();
            IList<Country> countriesSearch = new List<Country>();
            try
            {
                if (context.Database.CanConnect())
                {
                    await context.Database.BeginTransactionAsync();
                    countries = await service.GetRequestAsync((configuration["CountryClient:Country"]).ToString().Replace("{name}", country));

                    countriesSearch = GetSearch(countries, country);

                    context.logs.Add(new Log()
                    {
                        Descripcion = country,
                        Fecha = DateTime.Now

                    });

                    await context.SaveChangesAsync();

                    await context.Database.CommitTransactionAsync();
                }
                else {
                    HttpError = "No se pudo conectar a la base de datos";
                }
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                if (ex.Source.ToString().Equals("System.Net.NameResolution"))
                {
                    HttpError = ex.Source.ToString();
                }

            }
            finally {

                context.Dispose();
            }
            
            return EvalSearch(countriesSearch,HttpError);

        }

        private IList<Country> GetSearch(IList<Country> _country, string countrySearch) {
            List<Country> country = new List<Country>();

              foreach (Country c in _country)
              { 
                if (c.name.common.ToLower().Equals(countrySearch.ToLower())) {
                    country.Add(c);
                }
              }
            
            return country;
        }

        private IList<Country> EvalSearch(IList<Country> countries,string error) {
            Country country;
            if (countries.Count == 0)
            {
                country = new Country();
                if (error == null)
                {
                    country.error = error;
                }
                else
                {
                    if (error.Count() > 0)
                    {
                        country.error = HttpError;
                    }
                    else
                    {
                        country.error = "No se encontro la busqueda";
                    }
                }
                countries.Add(country);
            }


            return countries;
        }


    }
}

