using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Venues.Domain
{
    public sealed class Location
    {
        public string Reference { get; private set; }

        public double Latitiude { get; private set; }

        public double Longitude { get; private set; }

        public Location(string reference, double latitiude, double longitude)
        {
            Contract.Requires<ArgumentNullException>(reference != null);

            Reference = reference;
            Latitiude = latitiude;
            Longitude = longitude;
        }
    }
}