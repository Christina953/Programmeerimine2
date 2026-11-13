
namespace KooliProjekt.Application.Dto
{
    public class InvoiceLineDto
    {
        public int Id { get; set; }
        public int? BookingId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public int InvoiceId { get; set; }

    }
}
