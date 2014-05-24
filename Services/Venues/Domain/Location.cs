using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Venues.Domain
{
    public sealed class Location
    {
        public string Reference { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public Location(string reference, double latitude, double longitude)
        {
            Contract.Requires<ArgumentNullException>(reference != null);

            Reference = reference;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Location;
            if (other == null || GetType() != obj.GetType())
                return false;

            return Reference == other.Reference
                && Math.Abs(Latitude - other.Latitude) < Double.Epsilon
                && Math.Abs(Longitude - other.Longitude) < Double.Epsilon;
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                var hash = 17;

                hash = hash * 23 + Reference.GetHashCode();
                hash = hash * 23 + Latitude.GetHashCode();
                hash = hash * 23 + Longitude.GetHashCode();

                return hash;
            } 
        }
    }
}