using Newtonsoft.Json;

namespace AksStartupDotnetApp.Tooling
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object obj, bool useIndentedFomatting = true)
        {
            return JsonConvert.SerializeObject(
                obj,
                useIndentedFomatting ? Formatting.Indented : Formatting.None, JsonSettings);
        }

        public static T FromJsonString<T>(this string input)
        {
            return JsonConvert.DeserializeObject<T>(input, JsonSettings);
        }

        private static JsonSerializerSettings JsonSettings
            => new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };
    }
}
