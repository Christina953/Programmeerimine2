using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Booking
    {
        public int Id { get; set; }
        // Võimalik edasiarendus: broneerimise ja sõidu alguse eristamine.
        //Siis lisanduks BookedAt ning StartTime ja StartKilometers muutuksid nullable'iks.
        //Kui sõitu ei alustata 15 minuti jooksul, broneering tühistatakse.
        //public DateTime BookedAt { get; set; }  
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int StartKilometers { get; set; }
        public int? EndKilometers { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }

    }
}
