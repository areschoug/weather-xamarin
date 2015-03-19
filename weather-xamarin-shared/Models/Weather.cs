using System;

using System.Collections.Generic;
using Newtonsoft.Json;

namespace weatherxamarinshared {

    public class Weather {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "main")]
        public WeatherMain Main { get; set; }
        [JsonProperty(PropertyName = "weather")]
        public List<WeatherObject> WeatherObjects { get; set; }

        public Weather () { }


        public class WeatherObject {

            [JsonProperty(PropertyName = "description")]
            public string Description{ get; set; }

            public WeatherObject () {}

        }

        public class WeatherMain {

            [JsonProperty(PropertyName = "temp")]
            public double Temp { get; set; }

            [JsonProperty(PropertyName = "pressure")]
            public double Pressure { get; set; }

            [JsonProperty(PropertyName = "humidity")]
            public double Humidity { get; set; }

            public WeatherMain () {}

        }

    }

}

