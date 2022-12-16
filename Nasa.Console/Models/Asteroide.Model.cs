﻿namespace Nasa.Data.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Asteroide
    {
        [JsonProperty("links")]
        public Links Links { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("neo_reference_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NeoReferenceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nasa_jpl_url")]
        public Uri NasaJplUrl { get; set; }

        [JsonProperty("absolute_magnitude_h")]
        public double AbsoluteMagnitudeH { get; set; }

        [JsonProperty("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; }

        [JsonProperty("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; }

        [JsonProperty("close_approach_data")]
        public List<CloseApproachDatum> CloseApproachData { get; set; }

        [JsonProperty("is_sentry_object")]
        public bool IsSentryObject { get; set; }
    }

    public partial class CloseApproachDatum
    {
        [JsonProperty("close_approach_date")]
        public DateTimeOffset CloseApproachDate { get; set; }

        [JsonProperty("close_approach_date_full")]
        public string CloseApproachDateFull { get; set; }

        [JsonProperty("epoch_date_close_approach")]
        public long EpochDateCloseApproach { get; set; }

        [JsonProperty("relative_velocity")]
        public RelativeVelocity RelativeVelocity { get; set; }

        [JsonProperty("miss_distance")]
        public MissDistance MissDistance { get; set; }

        [JsonProperty("orbiting_body")]
        public string OrbitingBody { get; set; }
    }

    public partial class MissDistance
    {
        [JsonProperty("astronomical")]
        public string Astronomical { get; set; }

        [JsonProperty("lunar")]
        public string Lunar { get; set; }

        [JsonProperty("kilometers")]
        public string Kilometers { get; set; }

        [JsonProperty("miles")]
        public string Miles { get; set; }
    }

    public partial class RelativeVelocity
    {
        [JsonProperty("kilometers_per_second")]
        public string KilometersPerSecond { get; set; }

        [JsonProperty("kilometers_per_hour")]
        public string KilometersPerHour { get; set; }

        [JsonProperty("miles_per_hour")]
        public string MilesPerHour { get; set; }
    }

    public partial class EstimatedDiameter
    {
        [JsonProperty("kilometers")]
        public Feet Kilometers { get; set; }

        [JsonProperty("meters")]
        public Feet Meters { get; set; }

        [JsonProperty("miles")]
        public Feet Miles { get; set; }

        [JsonProperty("feet")]
        public Feet Feet { get; set; }
    }

    public partial class Feet
    {
        [JsonProperty("estimated_diameter_min")]
        public double EstimatedDiameterMin { get; set; }

        [JsonProperty("estimated_diameter_max")]
        public double EstimatedDiameterMax { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Nasa.Data.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Nasa.Data.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
