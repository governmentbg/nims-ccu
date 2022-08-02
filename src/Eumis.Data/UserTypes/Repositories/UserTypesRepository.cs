using Eumis.Common.Db;
using Eumis.Data.UserTypes.ViewObjects;
using Eumis.Domain.PermissionTemplates;
using Eumis.Domain.Users;
using Eumis.Domain.UserTypes;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.UserTypes.Repositories
{
    internal class UserTypesRepository : AggregateRepository<UserType>, IUserTypesRepository
    {
        public UserTypesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<UserTypesVO> GetUserTypes()
        {
            return (from ut in this.unitOfWork.DbContext.Set<UserType>()
                    select new UserTypesVO
                    {
                        UserTypeId = ut.UserTypeId,
                        Name = ut.Name,
                        IsSuperUser = ut.IsSuperUser,
                    })
                    .ToList();
        }

        public IDictionary<int, (string userTypeName, string permissionTemplateString)> GetUserTypePermissions()
        {
            return (from ut in this.unitOfWork.DbContext.Set<UserType>()
                    join pt in this.unitOfWork.DbContext.Set<PermissionTemplate>() on ut.PermissionTemplateId equals pt.PermissionTemplateId
                    select new
                    {
                        UserTypeId = ut.UserTypeId,
                        UserTypeName = ut.Name,
                        PermissionTemplateString = pt.PermissionsString,
                    })
                    .ToDictionary(
                        row => row.UserTypeId,
                        row => (userTypeName: row.UserTypeName, permissionTemplateString: row.PermissionTemplateString));
        }

        public IList<string> CanDeleteUserType(int userTypeId)
        {
            var errors = new List<string>();

            if ((from ut in this.unitOfWork.DbContext.Set<UserType>()

                 join u in this.unitOfWork.DbContext.Set<User>() on ut.UserTypeId equals u.UserTypeId into g1
                 from u in g1.DefaultIfEmpty()

                 where ut.UserTypeId == userTypeId && u != null
                 select ut.UserTypeId).Any())
            {
                errors.Add("Групата потребители не може да бъде изтрита, защото е свързана с потребител/и");
            }

            return errors;
        }

        public int GetUserOrganizationId(int userTypeId)
        {
            return this.FindWithoutIncludes(userTypeId).UserOrganizationId;
        }
    }
}
