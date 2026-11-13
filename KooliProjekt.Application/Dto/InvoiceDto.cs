
using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidAt { get; set; }
        public bool IsPaid => PaidAt != null;
        public bool IsOverdue => PaidAt == null && DueDate < DateTime.Today;
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerFullName => CustomerFirstName + " " + CustomerLastName;
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public decimal GrandTotal { get; set; }

    }
}
