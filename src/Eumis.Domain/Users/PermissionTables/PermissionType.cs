using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Users.PermissionTables
{
    public class PermissionType
    {
        public PermissionType()
        {
            this.Permissions = new List<Permission>();
        }

        public PermissionType(Type permissionType)
            : this()
        {
            this.Key = char.ToLowerInvariant(permissionType.Name[0]) + permissionType.Name.Substring(1);
            this.DisplayName = permissionType;

            foreach (Enum permission in Enum.GetValues(permissionType))
            {
                this.Permissions.Add(new Permission(permission));
            }
        }

        public PermissionType(Type permissionType, int orderNum)
            : this(permissionType)
        {
            this.OrderNum = orderNum;
        }

        // Not serializable
        public int OrderNum { get; set; }

        // TODO rename the property
        [JsonProperty("PermissionType")]
        public string Key { get; set; }

        [JsonConverter(typeof(TypeDescriptionConverter))]
        public Type DisplayName { get; set; }

        public IList<Permission> Permissions { get; set; }
    }
}
