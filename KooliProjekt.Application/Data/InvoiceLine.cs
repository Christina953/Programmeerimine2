using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public string Description { get; set; }    // "Renditeenus 123ABC"

        // Ülesanne eeldab ühte rida broneeringu kohta (kogus 1, ühikuhind = valemiga arvutatud summa).
        // Kogus ja ühikuhind on lisatud, et vajadusel saaks aja ja läbisõidu kajastada eraldi ridadena.
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        // Võimalik edasiarendus: allahindlus protsentides, nt 10
        // public decimal DiscountPercent { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int? BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
