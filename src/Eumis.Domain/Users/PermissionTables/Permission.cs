using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Users.PermissionTables
{
    public class Permission
    {
        public Permission()
        {
        }

        public Permission(object permission)
        {
            this.Key = char.ToLowerInvariant(permission.ToString()[0]) + permission.ToString().Substring(1);
            this.DisplayName = permission;
        }

        // TODO rename the property
        [JsonProperty("Permission")]
        public string Key { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public object DisplayName { get; set; }
    }
}
