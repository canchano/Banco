using System;
using System.Xml.Linq;

namespace ApiAplication.Domain.DTO
{
    public class Country
    {
        public Name name { get; set; }
        public decimal area { get; set; }
        public int population { get; set; }
        public string error {get;set;}
    }
}

