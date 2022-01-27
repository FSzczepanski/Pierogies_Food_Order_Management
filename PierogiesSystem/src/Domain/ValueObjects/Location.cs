namespace CleanArchitecture.Domain.ValueObjects
{
    public class Location
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        public Location()
        {
        }

        public Location(string name,string description, string street, string zipCode, string cityName, string countryName)
        {
            Name = name;
            Street = street;
            ZipCode = zipCode;
            CityName = cityName;
            CountryName = countryName;
        }
    }
}