using System;

using MonoTouch.UIKit;
using System.Drawing;

using weatherxamarinshared;

namespace weatherxamarinios {
    
    public class WeatherViewController : UIViewController {

        private readonly Controller _controller = new Controller();

        private UILabel _descriptionLabel;
        private UILabel _temperatureLabel;

        public WeatherViewController() {
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.DarkGray;

            _descriptionLabel = new UILabel(new RectangleF(0, 20, View.Frame.Width, 60));
            _descriptionLabel.TextAlignment = UITextAlignment.Center;
            _descriptionLabel.Font = UIFont.FromName("Helvetica-Bold", 20f);
            _descriptionLabel.Text = "description label";
            View.AddSubview(_descriptionLabel);

            //add _temperatureLabel below _descriptionLabel
            _temperatureLabel = new UILabel(new RectangleF(0, _descriptionLabel.Frame.Bottom, View.Frame.Width, 50));
            _temperatureLabel.TextAlignment = UITextAlignment.Center;
            _temperatureLabel.Font = UIFont.FromName("Helvetica", 14f);
            _temperatureLabel.Text = "temperature label";
            View.AddSubview(_temperatureLabel);

            _controller.FetchWeather(UpdateWeather);

        }

        private void UpdateWeather(Weather weather) {
            double absoluteZeroInCelsius = 273.15;// 1
            _descriptionLabel.Text = weather.WeatherObjects[0].Description; // 2
            _temperatureLabel.Text = String.Format("{0:0.0}C°", weather.Main.Temp - absoluteZeroInCelsius); // 3
        }

    }
}

