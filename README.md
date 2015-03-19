#Xamarin Tutorial - Lets make an Weather app YO!

##Before you start
Download and install Xamarin Studio. This tutorial is for mac with Xamarin Studio, but most steps should be the same on other platforms. 

##Setup

###Create solution

#####Protable Library
Create a Portable Library (PCL - Portable Class Library).

1. Name it `weather-xamarin-shared`
2. Pick location
3. Solution name should be `weather-xamarin`. Remove `-shared` because only the PCL will be shared. 

![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/Screen%20Shot%202015-03-18%20at%2019.12.38.png =640x0)

After creating the PCL you can create the iOS and Android projects.

#####iOS

1. Right-click your solution in the solution navigator(?)
2. Go to `Add`. And `Add New Project...`

![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/Screen%20Shot%202015-03-18%20at%2019.56.15.png =640x0)

1. Create empty iPhone project.
2. Name it `weather-xamarin-ios`

![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/Screen%20Shot%202015-03-18%20at%2019.47.34.png =640x0)

#####Android
1. Right-click your solution in the solution navigator(?)
2. Go to `Add`. And `Add New Project...`

![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/Screen%20Shot%202015-03-18%20at%2019.56.15.png =640x0)

1. Create Android Application
2. Name it `weather-xamarin-android`
![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/Screen%20Shot%202015-03-18%20at%2020.01.33.png =640x0)

###Correct formating *(optional)*
To make auto formating look the way I want it you should open your `.sln` file in the rootfolder of your project in a text editor of choise.

Remove this if you have it: *(Most likely you won't have it)*

```
GlobalSection(MonoDevelopProperties) = preSolution
	//Some rows
EndGlobalSection
```

Add this to the botom of the file, just before `EndGlobal`.

```

GlobalSection(MonoDevelopProperties) = preSolution
		Policies = $0
		$0.DotNetNamingPolicy = $1
		$1.DirectoryNamespaceAssociation = None
		$1.ResourceNamePolicy = FileFormatDefault
		$0.TextStylePolicy = $2
		$2.FileWidth = 120
		$2.inheritsSet = VisualStudio
		$2.inheritsScope = text/plain
		$2.scope = text/x-csharp
		$0.CSharpFormattingPolicy = $3
		$3.NamespaceBraceStyle = EndOfLine
		$3.ClassBraceStyle = EndOfLine
		$3.InterfaceBraceStyle = EndOfLine
		$3.StructBraceStyle = EndOfLine
		$3.EnumBraceStyle = EndOfLine
		$3.MethodBraceStyle = EndOfLine
		$3.ConstructorBraceStyle = EndOfLine
		$3.DestructorBraceStyle = EndOfLine
		$3.ElseNewLinePlacement = DoNotCare
		$3.ElseIfNewLinePlacement = DoNotCare
		$3.EmbeddedStatementPlacement = SameLine
		$3.BeforeMethodDeclarationParentheses = False
		$3.BeforeMethodCallParentheses = False
		$3.BeforeConstructorDeclarationParentheses = False
		$3.NewLineBeforeConstructorInitializerColon = SameLine
		$3.NewLineAfterConstructorInitializerColon = SameLine
		$3.BeforeDelegateDeclarationParentheses = False
		$3.AfterDelegateDeclarationParameterComma = True
		$3.NewParentheses = False
		$3.SpacesBeforeBrackets = False
		$3.inheritsSet = Mono
		$3.inheritsScope = text/x-csharp
		$3.scope = text/x-csharp
		$0.TextStylePolicy = $4
		$4.FileWidth = 120
		$4.TabsToSpaces = False
		$4.inheritsSet = VisualStudio
		$4.inheritsScope = text/plain
		$4.scope = text/plain
		$0.TextStylePolicy = $5
		$5.inheritsSet = null
		$5.scope = application/config+xml
		$0.XmlFormattingPolicy = $6
		$6.inheritsSet = null
		$6.scope = application/config+xml
	EndGlobalSection
	
```

###Set start project
Right-click on the projcet you want to use as start project (ios or android) select `Set as Startup Project`

##Portable library

In the shared library we have as much of the sharable code as possible. In this case we will have **APIClient**, **Controller**, **Weather**.

**APIClient** will have all the API-connection code.

**Controller** will be the connection between `ios`/`android` and `shared`

**Weather** will be the data model.

#####Add packages
To make requests and parse JSON we need to add some packages.

1. Right-click the `weather-xamarin-shared`
2. Go to `Add`. And `Add NuGet Packages...`![IMAGE](/images/Screen%20Shot%202015-03-18%20at%2020.22.02.png =640x0)
3. Select `Json.NET` and `Microsoft HTTP Client Libraries`
4. Click `Add Packages`

#####Weather Data Model
1. Add a folder to your PCL. Name it `Models`
2. Right-click it and `Add File..`. 
3. Create a General > Empty Class. Name it `Weather`.

It should look like this

```cs
using System;

namespace weatherxamarinshared {

    public class Weather {

        public Weather() {
        }

    }
}

```

We will use Open Weather Map to get our weather data. (http://openweathermap.org/api)

Example data:

```
{
  "coord": {
    "lon": 18.06,
    "lat": 59.33
  },
  "sys": {
    "type": 1,
    "id": 5420,
    "message": 0.0144,
    "country": "SE",
    "sunrise": 1426654463,
    "sunset": 1426697827
  },
  "weather": [
    {
      "id": 800,
      "main": "Clear",
      "description": "Sky is Clear",
      "icon": "01n"
    }
  ],
  "base": "cmc stations",
  "main": {
    "temp": 276.05,
    "pressure": 1033,
    "humidity": 69,
    "temp_min": 274.82,
    "temp_max": 278.15
  },
  "wind": {
    "speed": 0.5,
    "deg": 0
  },
  "clouds": {
    "all": 0
  },
  "dt": 1426708086,
  "id": 2673730,
  "name": "Stockholm",
  "cod": 200
}
```

To parse this json we need to add `using Newtonsoft.Json;` to the top of the file.

And then map the json to C#-properties like this.

```
[JsonProperty(PropertyName = "id")]
public int Id { get; set; }
		
```

For neasted json objects we need to add an new object to parse it. For example `main`. You could add a new file and add the class `WeatherMain` or you could just add the class inside the `Weather` Like this:

```
namespace fmtkxamarinshared {

  public class Weather {
  
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "main")]
    public WeatherMain Main { get; set; }

    public Weather () { }

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

```

When you need to parse an json-array like `"weather"` just add `using System.Collections.Generic;` to the top. And add `List<WeatherObject>` as object for the property.

```
[JsonProperty(PropertyName = "weather")]
public List<WeatherObject> WeatherObjects { get; set; }

public class WeatherObject {

  [JsonProperty(PropertyName = "description")]
  public string Description{ get; set; }

  public WeatherObject () {}

}
```

Full `Weather.cs`

```
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

        public Weather() {
        }

        public class WeatherObject {

            [JsonProperty(PropertyName = "description")]
            public string Description{ get; set; }

            public WeatherObject() {
            }

        }

        public class WeatherMain {

            [JsonProperty(PropertyName = "temp")]
            public double Temp { get; set; }

            [JsonProperty(PropertyName = "pressure")]
            public double Pressure { get; set; }

            [JsonProperty(PropertyName = "humidity")]
            public double Humidity { get; set; }

            public WeatherMain() {
            }

        }

    }

}
```

#####APIClient
1. Add a folder to your PCL. Name it `API`
2. Right-click it and `Add File..`. 
3. Create a General > Empty Class. Name it `APIClient`.

Your file should look like this

```
using System;

namespace weatherxamarinshared {

    public class APIClient {

        public APIClient() {
            
        }
    }
}

```

We want our `APIClient` to be a Singleton. Lets do that.

```
private static readonly APIClient _instance = new APIClient();

public static APIClient Instance {
	get { return _instance; }
}        

```

To make the get request we need to import some stuff to our client. Add these:

```

using System.Net.Http; 			//to make http requests
using System.Threading.Tasks; 	//to make async requests
using Newtonsoft.Json; 			//to parse json
```
(If `.HTTP` or `.JSON` make sure that they are in the packages folder, if not add them with NuGet)

We need a `HttpClient` to make requests, so make a private `HttpClient` in your `APIClient` like this.

```
private static readonly HttpClient _client = new HttpClient();
```

Lets do our request.

```

public async void GetWeather(Action<Weather> callback) { // 1

    string url = "http://api.openweathermap.org/data/2.5/weather?q=stockholm"; // 2

    Task<HttpResponseMessage> task = _client.GetAsync(url); // 3

    HttpResponseMessage response = await task; // 4
    string responseString = await response.Content.ReadAsStringAsync(); // 5

    Weather weather = JsonConvert.DeserializeObject<Weather>(responseString); // 6

    callback(weather); // 7

}
        
```

1. Name our method `GetWeather` it takes one parameter `Action<Weather> callback` it's the callback action that we want to fire when the request is done. `async` is to tell the program not to wait until function is finnished.
2. Url for weather in Stockholm 
3. This line makes the get call.
4. `await` tells the thread that it should wait for the `task` to finish before carrying on. And we will get the `HttpResponseMessage` with all the good stuff.
5. To parse the response to an string we need to (a)wait again.
6. With our response string we can deserialize it to our `Weather`-object
7. Return the weather object in the callback action.


Full `APIClient.cs`

```
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

```


#####Controller
In this class we should make all the connections between the app-projects and the API. (And possible other non-platform specific code)

1. Create a folder called `Controllers` and add a Empty class called `Controller`.
2. Add a `public` `Weather` property called `Weather` 
3. Create a method called `FetchWeather` with an `Action<Weather>` as parameter.
4. In the method call the `APIClient`s `GetWeather` method and in the callback set `Weather` to the object from the callback.

Your `Controller` should look like this:


```
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

```
##Platform specific

###Reference your PLC
We need to add the PCL reference to our ios and android projects. Just drag-and-drop the shared project into the `Reference` folder in the ios and android project. It should look like this.

![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/Screen%20Shot%202015-03-18%20at%2021.56.13.png =320x0)

###iOS

#####Create view controller
Add a `Empty class` called `WeatherViewController` in `Classes/UI/ViewControllers`. (You need to create the folders as well)

Make your `WeatherViewController` a sub-class of `UIViewController`. Add `using MonoTouch.UIKit;` 

Override `ViewDidLoad()` and set background color on the view.

```
 public class WeatherViewController : UIViewController {
        
        public WeatherViewController() { }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.DarkGray;
        }

    }
```
In your `AppDelegate`s `FinishedLaunching`-method set the `UIWindow`s `RootViewController` to our `WeatherViewController`

```
public override bool FinishedLaunching(UIApplication app, NSDictionary options) {

    window = new UIWindow(UIScreen.MainScreen.Bounds);
    window.RootViewController = new WeatherViewController();
    window.MakeKeyAndVisible();

    return true;
}
```

#####Create labels
We need two labels to show the weather description and tempature in. Make two private `UILabel` properties. And create them in `ViewDidLoad()`. Add `using System.Drawing;` to create `RectangleF` for frames

```
  public class WeatherViewController : UIViewController {
        
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

    }

}

```
#####Add Controller to view controller

To use things from our shared project we need to improt it in to our view controller. Like this:

```
using weatherxamarinshared;
```

Add a readonly private `Controller` property named `_controller`.

```
private readonly Controller _controller = new Controller();
```

Add a method that updates our labels with the data from a `Weather` object.

```
private void UpdateWeather(Weather weather) {
	double absoluteZeroInCelsius = 273.15;// 1
	_descriptionLabel.Text = weather.WeatherObjects[0].Description; // 2
	_temperatureLabel.Text = String.Format("{0:0.0}°C", weather.Main.Temp - absoluteZeroInCelsius); // 3
}

```

1. because OpenWeatherMap uses kelvin
2. Set description text. `weather.WeatherObjects` is an array so we use `[0]` to get the first object.
3. Set tempature text. With string fromat.

To fetch data add this to `ViewDidLoad`

```
_controller.FetchWeather(UpdateWeather);
```
This will run `UpdateWeather` function when the controller fires the callback.

Full `WeatherViewController`

```
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
            _temperatureLabel.Text = String.Format("{0:0.0}°C", weather.Main.Temp - absoluteZeroInCelsius); // 3
        }

    }
}

```


##### Run the app and BOOM!

![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/IMG_1537.PNG =0x420)

###Android
Set your Android project as start up project.

#####Create TextViews

Go to `weather-xamarin-android/Resources/layout/Main.axml`add two labels to this layout. One for description and one for temperature. Your layout should look like this:

```
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="fill_parent"
    android:layout_height="fill_parent">
    <TextView
        android:text="Text"
        android:layout_width="match_parent"
        android:layout_height="50dp"
        android:gravity="center"
        android:id="@+id/txt_description" />
    <TextView
        android:text="Text"
        android:layout_width="match_parent"
        android:layout_height="30dp"
        android:gravity="center"
        android:id="@+id/txt_temperature" />
</LinearLayout>
```

Go to your `MainActivity`
Add two private `TextView` properties.`_descriptionTextView` and `_temperatureTextView`. And use `FindViewById` in `OnCreate`-method to link with the `.axml`.

```
private TextView _descriptionTextView;
private TextView _temperatureTextView;

protected override void OnCreate(Bundle bundle) {
    base.OnCreate(bundle);

    SetContentView(Resource.Layout.Main);

    _descriptionTextView = FindViewById<TextView>(Resource.Id.txt_description);
    _temperatureTextView = FindViewById<TextView>(Resource.Id.txt_temperature);

}
```

#####Add Controller to Activity

To use things from our shared project we need to improt it in to our view controller. Like this:

```cs
using weatherxamarinshared;
```

Add a readonly private `Controller` property named `_controller`.

```
private readonly Controller _controller = new Controller();
```

Add a method that updates our labels with the data from a `Weather` object.

```
private void UpdateWeather(Weather weather) {
	double absoluteZeroInCelsius = 273.15;// 1
	_descriptionTextView.Text = weather.WeatherObjects[0].Description; // 2
	_descriptionTextView.Text = String.Format("{0:0.0}°C", weather.Main.Temp - absoluteZeroInCelsius); // 3
}

```

1. Because OpenWeatherMap uses kelvin
2. Set description text. `weather.WeatherObjects` is an array so we use `[0]` to get the first object.
3. Set tempature text. With string fromat.

To fetch data add this to `ViewDidLoad`

```
_controller.FetchWeather(UpdateWeather);
```
This will run `UpdateWeather` function when the controller fires the callback.

Full `MainActivity.cs`

```
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
```

##### Run the app and BOOM!
![IMAGE](http://hosting.monterosa.se//fmtk/tutorial/Screenshot_2015-03-19-09-44-46.png =0x420)