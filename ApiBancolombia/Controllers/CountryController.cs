using System;
using ApiAplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiBancolombia.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CountryController : ControllerBase
	{
		private readonly ICountryService countryService; 
		public CountryController(ICountryService _countryService)
		{
			countryService = _countryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCountry([FromQuery]string country ) {
			return Ok( await countryService.GetCountryAsync(country));
		}
	}
}

