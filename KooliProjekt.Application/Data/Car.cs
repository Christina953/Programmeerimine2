using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Car
    {
        public int Id { get; set; }
        public string RegNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerMinute { get; set; }
        public decimal PricePerKilometer { get; set; }
        //võimalik edasiarendus, juhul kui soovitakse rakendada päevahinda.
        //public decimal? PricePerDay { get; set; }
        public int CarTypeId { get; set; }
        public CarType CarType { get; set; }
        
    }
}
