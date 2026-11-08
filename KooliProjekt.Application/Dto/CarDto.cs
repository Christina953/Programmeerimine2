using System;
namespace KooliProjekt.Application.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string BodyType { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public int Seats { get; set; }
        public decimal PricePerMinute { get; set; }

        public decimal PricePerHour => Math.Round(PricePerMinute * 60, 2);
        public decimal PricePerKilometer { get; set; }
    }
}
