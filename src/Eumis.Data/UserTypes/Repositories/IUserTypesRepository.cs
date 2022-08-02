using Eumis.Data.UserTypes.ViewObjects;
using Eumis.Domain.UserTypes;
using System.Collections.Generic;

namespace Eumis.Data.UserTypes.Repositories
{
    public interface IUserTypesRepository : IAggregateRepository<UserType>
    {
        IList<UserTypesVO> GetUserTypes();

        IDictionary<int, (string userTypeName, string permissionTemplateString)> GetUserTypePermissions();

        IList<string> CanDeleteUserType(int userTypeId);

        int GetUserOrganizationId(int userTypeId);
    }
}
