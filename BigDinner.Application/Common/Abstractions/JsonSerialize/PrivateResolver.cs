using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace BigDinner.Application.Common.Abstractions.JsonSerialize;

public class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty prop=base.CreateProperty(member, memberSerialization);

        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            bool hasPrivateSetter = property.GetGetMethod(true) != null;
            prop.Writable = hasPrivateSetter;
        }

        return prop;
    }
}
