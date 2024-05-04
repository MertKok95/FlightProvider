using CoreWCF;
using FlightProvider.DataProvider;
using FlightProvider.Model;
using System.Runtime.Serialization;

namespace FlightProvider
{
    [DataContract]
    public class SearchRequest
    {
        [DataMember]
        public int OriginCountryId { get; set; }
        [DataMember]
        public int DestinationCountryId { get; set; }
        [DataMember]
        public int OriginCityId { get; set; }
        [DataMember]
        public int DestinationCityId { get; set; }
        [DataMember]
        public DateTime DepartureDate { get; set; }
        [DataMember]
        public DateTime ArrivalDate { get; set; }

    }


    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public bool HasError { get; set; }
        [DataMember]
        public List<FlightOption> FlightOptions { get; set; } = new List<FlightOption>();

    }

    public class Countries
    {
        [DataMember]
        public int CountryId { get; set; }
        [DataMember]
        public string CountryName { get; set; } = string.Empty;
    }

    public class Cities
    {
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public int CountryId { get; set; }
        [DataMember]
        public string CityName { get; set; } = string.Empty;
    }

    public class FlightOption
    {
        [DataMember]
        public string FlightNumber { get; set; } = string.Empty;
        [DataMember]
        public string RouteNumber { get; set; } = string.Empty;
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string SeatNumber { get; set; }
        [DataMember]
        public string OriginAirport { get; set; } = string.Empty;
        public string DestinationAirport { get; set; } = string.Empty;
    }

    public class PlaneRoute
    {
        [DataMember]
        public string RouteNumber { get; set; } = string.Empty;
        [DataMember]
        public int OriginCountryId { get; set; }
        [DataMember]
        public int OriginCityId { get; set; }
        [DataMember]
        public int DestinationCountryId { get; set; }
        [DataMember]
        public int DestionationCityId { get; set; }
        [DataMember]
        public DateTime DepartureDateTime { get; set; }
        [DataMember]
        public DateTime ArrivalDateTime { get; set; }
    }

    public class FlightData
    {
        public FlightOption FlightOption { get; set; }
        public PlaneRoute PlaneRoute { get; set; }
    }


    public class SeatData
    {
        [DataMember]
        public string SeatNumber { get; set; } = string.Empty;
        [DataMember]
        public int SeatCount { get; set; }
        [DataMember]
        public List<string> SeatSituation { get; set; }

    }

    public class SeatSituationInfo
    {
        [DataMember]
        public int SeatId { get; set; }

        [DataMember]
        public string State { get; set; } = string.Empty;

    }

    [ServiceContract]
    public interface IAirProvider
    {
        [OperationContract]
        List<Countries> GetCountries();
        [OperationContract]
        List<Cities> GetCities(int countryId);
        [OperationContract]
        string? GetCountryById(int countryId);
        [OperationContract]
        string? GetCityById(int cityId);
        [OperationContract]
        List<FlightData> AvailabilitySearch(SearchRequest request);
        [OperationContract]
        GetTicketResponseModel? GetTicket(string flightNumber);
    }

    public class AirProvider : IAirProvider
    {
        public List<FlightData> AvailabilitySearch(SearchRequest request)
        {
            List<FlightOption> flightOptions = FlightDataAccess.flightOptionList.ToList();

            List<FlightData> query = (from flightOption in flightOptions
                                      join planeRoute in FlightDataAccess.planeRouteList on flightOption.RouteNumber equals planeRoute.RouteNumber
                                      where planeRoute.DestinationCountryId == request.DestinationCountryId &&
                                            planeRoute.DestionationCityId == request.DestinationCityId &&
                                            planeRoute.OriginCountryId == request.OriginCountryId &&
                                            planeRoute.OriginCityId == request.OriginCityId &&
                                            planeRoute.DepartureDateTime.Date >= request.DepartureDate.Date &&
                                            planeRoute.ArrivalDateTime.Date <= request.ArrivalDate.Date
                                      select new FlightData
                                      {
                                          FlightOption = flightOption,
                                          PlaneRoute = planeRoute,
                                      }).ToList();
            return query;
        }
        public GetTicketResponseModel? GetTicket(string flightNumber)
        {
            var flight = FlightDataAccess.flightOptionList.Where(x => x.FlightNumber == flightNumber).FirstOrDefault();
            if (flight != null)
            {
                // veritabanına kayıt işlemi yapılacak.
                // UserFlight olarak bir veritabanı tablosu olabilir.
                // kullanıcı bilgisi ve  FlightNumber kaydedilecek.

                var planeRoute = FlightDataAccess.planeRouteList.Where(x => x.RouteNumber == flight.RouteNumber).FirstOrDefault();

                var originCountryName = FlightDataAccess.countryList.Where(x => x.CountryId == planeRoute.OriginCountryId).Select(x => x.CountryName).FirstOrDefault();


                var originCityName = FlightDataAccess.cityList.Where(x => x.CityId == planeRoute.OriginCityId).Select(x => x.CityName).FirstOrDefault();

                var destinationCountryName = FlightDataAccess.countryList.Where(x => x.CountryId == planeRoute.OriginCountryId).Select(x => x.CountryName).FirstOrDefault();


                var destinationCityName = FlightDataAccess.cityList.Where(x => x.CityId == planeRoute.OriginCityId).Select(x => x.CityName).FirstOrDefault();

                GetTicketResponseModel model = new GetTicketResponseModel
                {
                    FlightNumber = flightNumber,
                    OriginCountry = originCountryName,
                    OriginCity = originCityName,
                    OriginAirport = flight.OriginAirport,
                    DepartureDate = planeRoute.DepartureDateTime,
                    DestinationCountry = destinationCountryName,
                    DestinationCity = destinationCityName,
                    DestinationAirport = flight.DestinationAirport,
                    DestinationDate = planeRoute.ArrivalDateTime,
                    Price = flight.Price
                };
                return model;
            }
            return null;

        }
        public List<Countries> GetCountries()
        {
            return FlightDataAccess.countryList.ToList();
        }

        public string? GetCountryById(int countryId)
        {
            return FlightDataAccess.countryList.Where(x=> x.CountryId == countryId).Select(x=> x.CountryName).FirstOrDefault();
        }

        public List<Cities> GetCities(int countryId)
        {
            return FlightDataAccess.cityList.Where(x => x.CountryId == countryId).ToList();
        }

        public string? GetCityById(int cityId)
        {
            return FlightDataAccess.cityList.Where(x => x.CityId == cityId).Select(x=> x.CityName).FirstOrDefault();
        }


   


    }
}
