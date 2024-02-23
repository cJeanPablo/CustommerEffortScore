using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Ces.Api.Configurations
{
    public class JsonConfiguration
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private static readonly TextEncoderSettings textEncoderSettings = new TextEncoderSettings();

        public JsonConfiguration()
        {
            jsonSerializerOptions = new JsonSerializerOptions();
            TranscreverConfiguracoes(jsonSerializerOptions);
        }

        public JsonSerializerOptions Get() => jsonSerializerOptions;

        public static void TranscreverConfiguracoes(JsonSerializerOptions jsonSerializerOptions)
        {
            if (jsonSerializerOptions == null)
                jsonSerializerOptions = new JsonSerializerOptions();
            textEncoderSettings.AllowRanges(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement);
            jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.WriteIndented = false;
            jsonSerializerOptions.Encoder = JavaScriptEncoder.Create(textEncoderSettings);
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
