using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParsingEngine.JsonClasses
{
    internal class Address
    {
        [JsonPropertyName("streetAddress")]
        public string StreetAddress;

        [JsonPropertyName("city")]
        public string City;

        [JsonPropertyName("state")]
        public string State;

        [JsonPropertyName("postalCode")]
        public string PostalCode;
    }
}
