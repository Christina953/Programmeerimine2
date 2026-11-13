using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class SaveCarCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string RegNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string BodyType { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public decimal PricePerMinute { get; set; }
        public decimal PricePerKilometer { get; set; }
        public int CarTypeId { get; set; }
    }
}
