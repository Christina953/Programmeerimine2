using System;
namespace KooliProjekt.Application.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarRegNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int StartKilometers { get; set; }
        public int? EndKilometers { get; set; }

        public bool IsActive => EndTime == null;
        public int? Distance => EndKilometers - StartKilometers;
    }
}
