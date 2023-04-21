using System.Text.Json.Serialization;

namespace ProjectEcommerce.src.utilities
{
        [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeUser
    {
            REGULAR,
            VULNERABILITY,
            ADMINISTRATOR
    }
}
