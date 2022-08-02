using System;
using Newtonsoft.Json;
using Eumis.Public.Common.Json;

namespace Eumis.Public.Domain.Entities.Umis.Users.PermissionTables
{
    public class Permission
    {
        //TODO rename the property
        [JsonProperty("Permission")]
        public string Key { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public object DisplayName { get; set; }

        public Permission()
        {
        }

        public Permission(object permission)
        {
            this.Key = Char.ToLowerInvariant(permission.ToString()[0]) + permission.ToString().Substring(1);
            this.DisplayName = permission;
        }
    }
}
