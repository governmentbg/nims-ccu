using System.Collections.Generic;
using Eumis.Data.PermissionTemplates.ViewObjects;
using Eumis.Domain.PermissionTemplates;

namespace Eumis.Data.PermissionTemplates.Repositories
{
    public interface IPermissionTemplatesRepository : IAggregateRepository<PermissionTemplate>
    {
        PermissionTemplate FindByUserType(int userTypeId);

        PermissionTemplateUserInfoVO GetUserInfo(int userTypeId);

        IList<PermissionTemplateVO> GetPermissionTemplates();

        bool IsNameExist(string name);
    }
}
