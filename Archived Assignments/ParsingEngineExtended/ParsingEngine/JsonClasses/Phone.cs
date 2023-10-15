using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParsingEngine.JsonClasses
{
    internal class Phone
    {
        [JsonPropertyName("type")]
        public string Type;

        [JsonPropertyName("number")]
        public string Number;

        [JsonPropertyName("CanContact")]
        public bool CanContact;
    }
}
