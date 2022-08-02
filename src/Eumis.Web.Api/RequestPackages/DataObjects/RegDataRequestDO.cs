using Eumis.Domain.RequestPackages;
using Eumis.Domain.Users;

namespace Eumis.Web.Api.RequestPackages.DataObjects
{
    public class RegDataRequestDO
    {
        public RegDataRequestDO()
        {
        }

        public RegDataRequestDO(int requestPackageId, User user)
        {
            this.RequestPackageId = requestPackageId;
            this.UserId = user.UserId;
            this.Uin = user.Uin;
            this.Fullname = user.Fullname;
            this.Email = user.Email;
            this.Phone = user.Phone;
            this.Address = user.Address;
            this.Position = user.Position;
            this.UserOrganizationId = user.UserOrganizationId;
            this.UserTypeId = user.UserTypeId;
        }

        public RegDataRequestDO(RegDataRequest regDataRequest)
        {
            this.RequestPackageId = regDataRequest.RequestPackageId;
            this.UserId = regDataRequest.UserId;
            this.Uin = regDataRequest.Uin;
            this.Fullname = regDataRequest.Fullname;
            this.Email = regDataRequest.Email;
            this.Phone = regDataRequest.Phone;
            this.Address = regDataRequest.Address;
            this.Position = regDataRequest.Position;
            this.UserOrganizationId = regDataRequest.UserOrganizationId;
            this.UserTypeId = regDataRequest.UserTypeId;
        }

        public int RequestPackageId { get; set; }

        public int UserId { get; set; }

        public string Uin { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public int UserOrganizationId { get; set; }

        public int UserTypeId { get; set; }
    }
}
