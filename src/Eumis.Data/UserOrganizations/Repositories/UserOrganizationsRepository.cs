using Eumis.Common.Db;
using Eumis.Data.UserOrganizations.ViewObjects;
using Eumis.Domain.UserOrganizations;
using Eumis.Domain.UserTypes;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.UserOrganizations.Repositories
{
    internal class UserOrganizationsRepository : AggregateRepository<UserOrganization>, IUserOrganizationsRepository
    {
        public UserOrganizationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<UserOrganizationsVO> GetUserOrganizations()
        {
            return (from uo in this.unitOfWork.DbContext.Set<UserOrganization>()
                    select new UserOrganizationsVO
                    {
                        UserOrganizationId = uo.UserOrganizationId,
                        Name = uo.Name,
                        Version = uo.Version,
                    })
                    .ToList();
        }

        public IList<string> CanDeleteUserOrganization(int userOrganizationId)
        {
            var errors = new List<string>();

            if ((from uo in this.unitOfWork.DbContext.Set<UserOrganization>()

                 join ut in this.unitOfWork.DbContext.Set<UserType>() on uo.UserOrganizationId equals ut.UserOrganizationId into g1
                 from ut in g1.DefaultIfEmpty()

                 where uo.UserOrganizationId == userOrganizationId && ut != null
                 select uo.UserOrganizationId).Any())
            {
                errors.Add("Организацията не може да бъде изтрита, защото има група/и потребители, свързани към нея");
            }

            return errors;
        }
    }
}
