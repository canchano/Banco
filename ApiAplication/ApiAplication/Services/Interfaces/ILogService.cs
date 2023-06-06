using System;
using ApiAplication.Domain.DTO;

namespace ApiAplication.Services.Interfaces
{
	public interface ILogService
	{
		Country SaveLog(string country);
	}
}

