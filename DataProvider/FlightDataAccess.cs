namespace FlightProvider.DataProvider
{
    public static class FlightDataAccess
    {
        // Bilgi Notu
        // Bu kurgu test senaryosu için yapılmıştır.
        // Normalde gerçek bir veritabanı olur.
        // methodlar static olarak değil, DI yöntemi ile program.cs AddSingleton tarzı contructor geçişi sağlanır.

        public static List<FlightOption> flightOptionList = new List<FlightOption>()
        {
            new FlightOption{ FlightNumber = "A0000001", RouteNumber = "A0000001", SeatNumber= "A0000001", OriginAirport = "HavaAlaniName_A1", DestinationAirport = "HavaAlaniName_A2", Price =  351},
            new FlightOption{ FlightNumber = "A0000002", RouteNumber = "A0000002", SeatNumber= "A0000002", OriginAirport = "HavaAlaniName_B1", DestinationAirport = "HavaAlaniName_B2", Price =  241},
            new FlightOption{ FlightNumber = "A0000003", RouteNumber = "A0000003", SeatNumber= "A0000003", OriginAirport = "HavaAlaniName_C1", DestinationAirport = "HavaAlaniName_C2", Price =  521},
            new FlightOption{ FlightNumber = "A0000004", RouteNumber = "A0000004", SeatNumber= "A0000004", OriginAirport = "HavaAlaniName_D1", DestinationAirport = "HavaAlaniName_D2", Price =  171},

        };

        public static List<PlaneRoute> planeRouteList = new List<PlaneRoute>()
        {
            new PlaneRoute{ RouteNumber = "A0000001", OriginCountryId = 1, OriginCityId = 1, DestinationCountryId = 2, DestionationCityId = 2, ArrivalDateTime= new DateTime(2024,5,4, 4,10, 5), DepartureDateTime = new DateTime(2024,5,3, 4,10, 5)},
            new PlaneRoute{ RouteNumber = "A0000002", OriginCountryId = 2, OriginCityId = 2, DestinationCountryId = 3, DestionationCityId = 3, ArrivalDateTime= new DateTime(2024,5,3, 4,10, 5), DepartureDateTime = new DateTime(2024,5,2, 4,10, 5)},
            new PlaneRoute{ RouteNumber = "A0000003", OriginCountryId = 1, OriginCityId = 1, DestinationCountryId = 3, DestionationCityId = 3, ArrivalDateTime= new DateTime(2024,5,6, 4,10, 5), DepartureDateTime = new DateTime(2024,5,5, 4,10, 5)},
            new PlaneRoute{ RouteNumber = "A0000004", OriginCountryId = 3, OriginCityId = 3, DestinationCountryId = 1, DestionationCityId = 1, ArrivalDateTime= DateTime.Now.AddDays(1), DepartureDateTime = DateTime.Now},
        };

        public static List<Countries> countryList = new List<Countries>
        {
            new Countries{ CountryId = 1, CountryName = "Türkiye"},
            new Countries{ CountryId = 2, CountryName = "Amerika"},
            new Countries { CountryId = 3, CountryName = "Almanya" },
        };

        public static List<Cities> cityList = new List<Cities>() {
            new Cities{ CityId = 1, CityName= "İstanbul", CountryId = 1},
            new Cities{ CityId = 2, CityName= "United State", CountryId = 2},
            new Cities{ CityId = 3, CityName= "Berlin", CountryId = 3}
        };
    }
}
