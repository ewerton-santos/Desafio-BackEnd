using RentBike.Domain;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RentBikeUsers.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter<DriversLicenceType>))]
    public enum DriversLicenceType
    {
        [EnumMember(Value = "A")]
        A = 1,
        [EnumMember(Value = "B")]
        B = 2,
        [EnumMember(Value = "AB")]
        AB = 3
    }
}
