using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Eumis.Public.Common.Json;

namespace Eumis.Public.Domain.Entities.Umis.Users.PermissionTables
{
    public class PermissionType
    {
        //TODO rename the property
        [JsonProperty("PermissionType")]
        public string Key { get; set; }

        [JsonConverter(typeof(TypeDescriptionConverter))]
        public Type DisplayName { get; set; }

        public IList<Permission> Permissions { get; set; }

        public PermissionType()
        {
            this.Permissions = new List<Permission>();
        }

        public PermissionType(Type permissionType)
            :this()
        {
            this.Key = Char.ToLowerInvariant(permissionType.Name[0]) + permissionType.Name.Substring(1);
            this.DisplayName = permissionType;

            foreach (Enum permission in Enum.GetValues(permissionType))
            {
                this.Permissions.Add(new Permission(permission));
            }
        }
    }
}
