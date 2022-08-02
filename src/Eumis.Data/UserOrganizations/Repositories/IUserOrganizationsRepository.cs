using Eumis.Data.UserOrganizations.ViewObjects;
using Eumis.Domain.UserOrganizations;
using System.Collections.Generic;

namespace Eumis.Data.UserOrganizations.Repositories
{
    public interface IUserOrganizationsRepository : IAggregateRepository<UserOrganization>
    {
        IList<UserOrganizationsVO> GetUserOrganizations();

        IList<string> CanDeleteUserOrganization(int userOrganizationId);
    }
}
