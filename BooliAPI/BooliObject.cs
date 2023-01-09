namespace BooliAPI
{
    public class BooliObject
    {
        public class Rootobject
        {
            public int totalCount { get; set; }
            public int count { get; set; }
            public Sold[]? sold { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public Searchparams? searchParams { get; set; }
        }

        public class Searchparams
        {
            public int areaId { get; set; }
        }
        public class Sold
        {
            public Source? source { get; set; }
            public int rooms { get; set; }
            public int constructionYear { get; set; }
            public string? soldDate { get; set; }
            public Location? location { get; set; }
            public int additionalArea { get; set; }
            public int rent { get; set; }
            public int livingArea { get; set; }
            public string? soldPriceSource { get; set; }
            public int listPrice { get; set; }
            public int soldPrice { get; set; }
            public int booliId { get; set; }
            public int floor { get; set; }
            public string? objectType { get; set; }
            public string? published { get; set; }
            public string? apartmentNumber { get; set; }
            public string? url { get; set; }
        }
        public class Source
        {
            public int id { get; set; }
            public string? url { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
        }
        public class Location
        {
            public Address? address { get; set; }
            public Position? position { get; set; }
            public Region? region { get; set; }
            public Distance? distance { get; set; }
            public string[]? namedAreas { get; set; }
        }

        public class Address
        {
            public string? city { get; set; }
            public string? streetAddress { get; set; }
        }
        public class Position
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }
        public class Region
        {
            public string? municipalityName { get; set; }
            public string? countyName { get; set; }
        }
        public class Distance
        {
            public int ocean { get; set; }
        }
    }
}

