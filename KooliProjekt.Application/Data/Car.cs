using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    [Index(nameof(RegNumber), IsUnique = true)]
    public class Car
    {
        public int Id { get; set; }
        [Required, MaxLength(10)]
        public string RegNumber { get; set; }
        [MaxLength(50)]
        public string Brand { get; set; }

        [MaxLength(50)] 
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
