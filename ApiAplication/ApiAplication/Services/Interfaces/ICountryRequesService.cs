using System;
using ApiAplication.Domain.DTO;

namespace ApiAplication.Services.Interfaces
{
	public interface ICountryRequesService
	{
		Task<IList<Country>> GetRequestAsync(string url); 
	}
}

