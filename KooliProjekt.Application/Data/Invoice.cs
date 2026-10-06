using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatRate { get; set; } //protsentides
        public decimal VatAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime? PaidAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public IList<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    }
}
