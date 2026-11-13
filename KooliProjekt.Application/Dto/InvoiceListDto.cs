
using System;

namespace KooliProjekt.Application.Dto
{
    public class InvoiceListDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime? PaidAt { get; set; }
        public bool IsPaid => PaidAt != null;

    }
}
