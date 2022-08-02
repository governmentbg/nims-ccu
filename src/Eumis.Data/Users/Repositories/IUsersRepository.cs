using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eumis.Data.Declarations.ViewObjects;
using Eumis.Data.Users.ViewObjects;
using Eumis.Domain.Users;

namespace Eumis.Data.Users.Repositories
{
    public interface IUsersRepository : IAggregateRepository<User>
    {
        User Find(string username);

        User Find(Guid gid);

        Task<User> FindAsync(string username);

        Task<User> FindAsync(Guid gid);

        IList<User> FindAll(int[] ids);

        IDictionary<int, (string fullname, int userTypeId)> GetAllUserInfo(int requestPackageId);

        IList<User> FindAllByPermissionTemplate(int permissionTemplateId);

        IList<User> FindAllControllingByOrganization(int userOrganizationId);

        IList<User> FindAllAdministrators();

        IList<User> FindAllByProgrammePermission<TEnum>(int programmeId, TEnum permission);

        User FindByPasswordRecoveryCode(string passwordRecoveryCode);

        User FindByNewPasswordCode(string newPasswordCode);

        IList<UserVO> GetUsers(
            string username = null,
            string fullname = null,
            int? userOrganizationId = null,
            bool? active = null,
            bool? deleted = null,
            bool? locked = null,
            bool? hasAcceptedGDPRDeclaration = null,
            bool exact = false);

        IList<string> GetActiveUsersEmails();

        bool IsUniqueUsername(string username, int? userId = null);

        UserRequestsWrapperVO GetUserRequests(int userId);

        int GetUserTypeId(int userId);

        int GetUserOrganizationId(int userId);

        string GetUserFullname(int userId);

        string GetUserUsername(int userId);

        string GetUserEmail(int userId);

        UserGDPRDeclarationInfoVO GetGDPRDeclarationInfo(int userId);

        IList<UserDeclarationVO> GetUserDeclarations(int userId);

        IList<UserUnacceptedDeclarationVO> GetUserUnacceptedDeclarations(int userId);

        bool UserExists(string uin);

        bool OtherUserExists(int currentUserId, string uin);
    }
}
