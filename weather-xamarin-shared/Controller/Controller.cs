using System;

namespace weatherxamarinshared {
    public class Controller {

        public Weather Weather;

        public Controller() {
        }

        public void FetchWeather(Action<Weather> callback) {
            
            APIClient.Instance.GetWeather((w => {
                Weather = w;        
                callback(Weather);
            }));

        }
    }
}

