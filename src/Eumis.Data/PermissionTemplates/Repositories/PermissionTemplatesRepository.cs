using Eumis.Common.Db;
using Eumis.Data.PermissionTemplates.ViewObjects;
using Eumis.Domain.PermissionTemplates;
using Eumis.Domain.UserOrganizations;
using Eumis.Domain.UserTypes;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.PermissionTemplates.Repositories
{
    internal class PermissionTemplatesRepository : AggregateRepository<PermissionTemplate>, IPermissionTemplatesRepository
    {
        public PermissionTemplatesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public PermissionTemplate FindByUserType(int userTypeId)
        {
            return (from ut in this.unitOfWork.DbContext.Set<UserType>()
                    join pt in this.Set() on ut.PermissionTemplateId equals pt.PermissionTemplateId
                    where ut.UserTypeId == userTypeId
                    select pt)
                    .SingleOrDefault();
        }

        public PermissionTemplateUserInfoVO GetUserInfo(int userTypeId)
        {
            return (from ut in this.unitOfWork.DbContext.Set<UserType>()
                    join uo in this.unitOfWork.DbContext.Set<UserOrganization>() on ut.UserOrganizationId equals uo.UserOrganizationId
                    where ut.UserTypeId == userTypeId
                    select new PermissionTemplateUserInfoVO
                    {
                        UserOrganizationName = uo.Name,
                        UserTypeName = ut.Name,
                    })
                    .SingleOrDefault();
        }

        public IList<PermissionTemplateVO> GetPermissionTemplates()
        {
            return (from permissionTemplate in this.unitOfWork.DbContext.Set<PermissionTemplate>()
                    select new PermissionTemplateVO
                    {
                        PermissionTemplateId = permissionTemplate.PermissionTemplateId,
                        Name = permissionTemplate.Name,
                    })
                .ToList();
        }

        public bool IsNameExist(string name)
        {
            return this.unitOfWork.DbContext.Set<PermissionTemplate>().Any(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
        }
    }
}
