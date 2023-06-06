using System;
using ApiAplication.Domain.DTO;
using ApiAplication.Persistence;
using ApiAplication.Services.Interfaces;

namespace ApiAplication.Services.Implements
{
    public class LogService:ILogService 
    {
        private readonly LogDbContext context;

        public LogService(LogDbContext _logDbContext)
        {
            context = _logDbContext;
        }

        public Country SaveLog(string country)
        {
            Country c = new Country();
            try
            {
                context.logs.Add(new Log()
                {
                    Descripcion = country,
                    Fecha = DateTime.Now

                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.Source.ToString().Equals("System.Net.NameResolution")) {
                    c.error = ex.Source.ToString();
                }
                else
                {
                    c.error = "No se pudo conectar a la base de datos";
                }
                
            }
            return c;
        }
    }
}

