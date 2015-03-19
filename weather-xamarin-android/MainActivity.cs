using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using weatherxamarinshared;

namespace weatherxamarinandroid {
    [Activity(Label = "weather-xamarin-android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity {
        
        private readonly Controller _controller = new Controller();

        private TextView _descriptionTextView;
        private TextView _temperatureTextView;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _descriptionTextView = FindViewById<TextView>(Resource.Id.txt_description);
            _temperatureTextView = FindViewById<TextView>(Resource.Id.txt_temperature);
			
           
            _controller.FetchWeather(UpdateWeather);

        }

        private void UpdateWeather(Weather weather) {
            double absoluteZeroInCelsius = 273.15;// 1
            _descriptionTextView.Text = weather.WeatherObjects[0].Description; // 2
            _temperatureTextView.Text = String.Format("{0:0.0}C°", weather.Main.Temp - absoluteZeroInCelsius); // 3
        }


    }
}


