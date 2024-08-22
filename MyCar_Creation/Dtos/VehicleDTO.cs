namespace MyCar_Creation.Dtos
{
    public class VehicleDTO
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
