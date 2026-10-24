using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    [Index(nameof(InvoiceNumber), IsUnique = true)]
    public class Invoice
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
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
