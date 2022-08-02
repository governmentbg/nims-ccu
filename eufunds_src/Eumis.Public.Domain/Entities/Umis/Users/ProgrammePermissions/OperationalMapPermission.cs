using System.Data.Entity.ModelConfiguration;
using System;
using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Оперативна карта")]
    public enum OperationalMapPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite,
    }

    internal class OperationalMapPermission : ProgrammePermission
    {
        private OperationalMapPermission()
        {
        }

        public OperationalMapPermission(int programmeId, OperationalMapPermissions permission)
            : base (permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(OperationalMapPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(OperationalMapPermissions), base.PermissionString);
            }
        }
    }

    internal class OperationalMapPermissionMap : EntityTypeConfiguration<OperationalMapPermission>
    {
        public OperationalMapPermissionMap()
        {
        }
    }
}
