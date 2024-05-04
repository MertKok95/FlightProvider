namespace FlightProvider.Model
{
    public class GetTicketResponseModel
    {
        public string FlightNumber { get; set; }
        public string OriginCountry { get; set; }
        public string OriginCity { get; set; }
        public string OriginAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime DestinationDate { get; set; }
        public decimal Price { get; set; }
    }
}
