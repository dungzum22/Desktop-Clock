using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Configuration;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace DesktopClock
{
    public partial class MainWindow : Window
    {
        #region Fields
        private readonly string OpenWeatherApiKey = ConfigurationManager.AppSettings["OpenWeatherApiKey"];
        private string _city = "";
        private string _country = "";
        private double _lat = 0;
        private double _lon = 0;
        private DateTime _lastApiTime;
        private TimeSpan _timeOffset;
        private DispatcherTimer _retryTimer;
        private bool _isRetrying = false;
        #endregion

        #region Win32 API
        const int GWL_EXSTYLE = -20;
        const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            SetupRetryTimer();
        }
        #endregion

        #region Window Setup
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            exStyle |= WS_EX_TOOLWINDOW;
            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle);
        }
        #endregion

        #region Timer Setup
        private void SetupRetryTimer()
        {
            _retryTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _retryTimer.Tick += async (s, e) => await RetryConnection();
        }

        private void SetupTimeUpdateTimer()
        {
            DispatcherTimer timeUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromHours(1)
            };
            timeUpdateTimer.Tick += async (s, e) => await GetTimeFromApiAsync();
            timeUpdateTimer.Start();
        }

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += UpdateClock;
            timer.Start();
        }
        #endregion

        #region Event Handlers
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await GetTimeFromApiAsync();
            await GetLocationAndWeatherAsync();
            StartClock();
            SetupTimeUpdateTimer();
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now - _timeOffset;
            HourTextBlock.Text = now.ToString("HH");
            MinuteTextBlock.Text = now.ToString("mm");
            SecondTextBlock.Text = now.ToString("ss");
            DateTextBlock.Text = now.ToString("dddd, d MMMM yyyy");
        }
        #endregion

        #region API Methods
        private async Task GetTimeFromApiAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync("http://worldtimeapi.org/api/ip");
                    var timeInfo = JsonDocument.Parse(response).RootElement;
                    string dateTimeStr = timeInfo.GetProperty("datetime").GetString();
                    _lastApiTime = DateTime.Parse(dateTimeStr);
                    _timeOffset = DateTime.Now - _lastApiTime;
                }
            }
            catch (Exception)
            {
                _lastApiTime = DateTime.Now;
                _timeOffset = TimeSpan.Zero;
            }
        }

        private async Task GetLocationAndWeatherAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var ipApiResp = await client.GetStringAsync("http://ip-api.com/json/");
                    var ipInfo = JsonDocument.Parse(ipApiResp).RootElement;
                    _city = ipInfo.GetProperty("city").GetString();
                    _country = ipInfo.GetProperty("country").GetString();
                    _lat = ipInfo.GetProperty("lat").GetDouble();
                    _lon = ipInfo.GetProperty("lon").GetDouble();

                    string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={_lat}&lon={_lon}&appid={OpenWeatherApiKey}&units=metric&lang=vi";
                    var weatherResp = await client.GetStringAsync(weatherUrl);
                    var weatherInfo = JsonDocument.Parse(weatherResp).RootElement;
                    double temp = weatherInfo.GetProperty("main").GetProperty("temp").GetDouble();

                    LocationTempTextBlock.Text = $"{_city}, {_country}  |  {temp}°C";
                }
            }
            catch (Exception)
            {
                LocationTempTextBlock.Text = "Waiting for connection...";
                _retryTimer.Start();
            }
        }

        private async Task RetryConnection()
        {
            if (_isRetrying) return;
            _isRetrying = true;

            try
            {
                await GetLocationAndWeatherAsync();
                if (LocationTempTextBlock.Text.Contains("Error."))
                    _retryTimer.Start();
                else
                    _retryTimer.Stop();
            }
            finally
            {
                _isRetrying = false;
            }
        }
        #endregion
    }
}
