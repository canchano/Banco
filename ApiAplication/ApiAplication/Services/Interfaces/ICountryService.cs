using System;
using ApiAplication.Domain.DTO;

namespace ApiAplication.Services.Interfaces
{
	public interface ICountryService
	{
        Task<IList<Country>> GetCountryAsync(string country);

    }
}

