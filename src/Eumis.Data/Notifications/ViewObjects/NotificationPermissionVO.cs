namespace Eumis.Data.Notifications.ViewObjects
{
    internal class NotificationPermissionVO
    {
        public NotificationPermissionVO(string permission, string type)
        {
            this.Permission = permission;
            this.PermissionType = type;
        }

        public string Permission { get; set; }

        public string PermissionType { get; set; }
    }
}
