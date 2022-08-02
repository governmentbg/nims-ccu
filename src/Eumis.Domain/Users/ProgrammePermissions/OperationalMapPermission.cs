using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.OperationalMapPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum OperationalMapPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.OperationalMapPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.OperationalMapPermissions_CanWrite), ResourceType = typeof(DomainEnumTexts))]
        CanWrite,
    }

    internal class OperationalMapPermission : ProgrammePermission
    {
        private OperationalMapPermission()
        {
        }

        public OperationalMapPermission(int programmeId, OperationalMapPermissions permission)
            : base(permission.ToString(), programmeId)
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
                return Enum.Parse(typeof(OperationalMapPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class OperationalMapPermissionMap : EntityTypeConfiguration<OperationalMapPermission>
    {
        public OperationalMapPermissionMap()
        {
        }
    }
}
