using System;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace weatherxamarinshared {

    public class APIClient {

        private static readonly HttpClient _client = new HttpClient();
        private static readonly APIClient _instance = new APIClient();

        public static APIClient Instance {
            get { return _instance; }
        }

        public APIClient() {
            
        }

        public async void GetWeather(Action<Weather> callback) {

            string url = "http://api.openweathermap.org/data/2.5/weather?q=stockholm";

            Task<HttpResponseMessage> task = _client.GetAsync(url);

            HttpResponseMessage response = await task;
            string responseString = await response.Content.ReadAsStringAsync();

            Weather weather = JsonConvert.DeserializeObject<Weather>(responseString); 

            callback(weather);

        }

    }

}