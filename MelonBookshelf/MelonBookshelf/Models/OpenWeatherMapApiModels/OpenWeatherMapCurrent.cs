using System.Text.Json.Serialization;

namespace MelonBookshelf.Models.OpenWeatherMapApiModels
{
    public class WeatherData
    {
        public string Cod { get; set; }
        public float Message { get; set; }
        public int Cnt { get; set; }
        public WeatherItem[] List { get; set; }
        public City City { get; set; }
    }

    public class WeatherItem
    {
        public long Dt { get; set; }
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public int Visibility { get; set; }
        public float Pop { get; set; }
        public Sys Sys { get; set; }
        [JsonPropertyName("dt_txt")]
        public string DtTxt { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        [JsonPropertyName("feels_like")]
        public float FeelsLike { get; set; }
        [JsonPropertyName("temp_min")]
        public float TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public float TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonPropertyName("sea_level")]
        public int SeaLevel { get; set; }
        [JsonPropertyName("grnd_level")]
        public int GroundLevel { get; set; }
        [JsonPropertyName("temp_kf")]
        public float TempKf { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Wind
    {
        public float Speed { get; set; }
        public int Deg { get; set; }
        public float Gust { get; set; }
    }

    public class Sys
    {
        public string Pod { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public int Timezone { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }

    public class Coord
    {
        public float Lat { get; set; }
        public float Lon { get; set; }
    }
}

