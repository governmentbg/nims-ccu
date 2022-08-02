using Eumis.Common.Db;
using Eumis.Data.Declarations.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Data.RequestPackages.ViewObjects;
using Eumis.Data.Users.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.RequestPackages;
using Eumis.Domain.UserOrganizations;
using Eumis.Domain.Users;
using Eumis.Domain.Users.CommonPermissions;
using Eumis.Domain.UserTypes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eumis.Data.Users.Repositories
{
    internal class UsersRepository : AggregateRepository<User>, IUsersRepository
    {
        public UsersRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<User, object>>[] Includes
        {
            get
            {
                return new Expression<Func<User, object>>[]
                {
                    u => u.UserPermissions,
                };
            }
        }

        public User Find(string username)
        {
            return this.Set()
                .Where(u => u.Username == username)
                .SingleOrDefault();
        }

        public User Find(Guid gid)
        {
            return this.Set()
                .Where(u => u.Gid == gid)
                .SingleOrDefault();
        }

        public Task<User> FindAsync(string username)
        {
            return this.Set()
                .Where(u => u.Username == username)
                .SingleOrDefaultAsync();
        }

        public Task<User> FindAsync(Guid gid)
        {
            return this.Set()
                .Where(u => u.Gid == gid)
                .SingleOrDefaultAsync();
        }

        public IList<User> FindAll(int[] ids)
        {
            return this.Set()
                .Where(t => ids.Contains(t.UserId))
                .ToList();
        }

        public IDictionary<int, (string fullname, int userTypeId)> GetAllUserInfo(int requestPackageId)
        {
            return (from u in this.unitOfWork.DbContext.Set<User>()
                    join rpu in this.unitOfWork.DbContext.Set<RequestPackageUser>() on u.UserId equals rpu.UserId
                    where rpu.RequestPackageId == requestPackageId
                    select u)
                    .ToDictionary(
                        row => row.UserId,
                        row => (fullname: row.Fullname, userTypeId: row.UserTypeId));
        }

        public IList<User> FindAllByPermissionTemplate(int permissionTemplateId)
        {
            (from u in this.unitOfWork.DbContext.Set<User>()
             join ut in this.unitOfWork.DbContext.Set<UserType>() on u.UserTypeId equals ut.UserTypeId
             join up in this.unitOfWork.DbContext.Set<UserPermission>() on u.UserId equals up.UserId
             where ut.PermissionTemplateId == permissionTemplateId
             select up).Load();

            return (from u in this.unitOfWork.DbContext.Set<User>()
                    join ut in this.unitOfWork.DbContext.Set<UserType>() on u.UserTypeId equals ut.UserTypeId
                    where ut.PermissionTemplateId == permissionTemplateId
                    select u)
                    .ToList();
        }

        public IList<User> FindAllControllingByOrganization(int userOrganizationId)
        {
            var ccPermission = UserAdminPermissions.CanControl.ToString();

            return (from u in this.unitOfWork.DbContext.Set<User>()
                    join up in this.unitOfWork.DbContext.Set(UserPermission.GetPermissionEntityType(typeof(UserAdminPermissions))).OfType<UserPermission>() on u.UserId equals up.UserId
                    where !u.IsSystem &&
                          u.UserOrganizationId == userOrganizationId &&
                          up.PermissionString == ccPermission
                    select u)
                    .Include(this.Includes)
                    .ToList();
        }

        public IList<User> FindAllAdministrators()
        {
            var ccPermission = UserAdminPermissions.CanControl.ToString();
            var caPermission = UserAdminPermissions.CanAdministrate.ToString();

            return (from u in this.unitOfWork.DbContext.Set<User>()
                    join up1 in this.unitOfWork.DbContext.Set(UserPermission.GetPermissionEntityType(typeof(UserAdminPermissions))).OfType<UserPermission>() on u.UserId equals up1.UserId
                    join up2 in this.unitOfWork.DbContext.Set(UserPermission.GetPermissionEntityType(typeof(UserAdminPermissions))).OfType<UserPermission>() on u.UserId equals up2.UserId
                    join ut in this.unitOfWork.DbContext.Set<UserType>() on u.UserTypeId equals ut.UserTypeId
                    where !u.IsSystem &&
                          ut.IsSuperUser == true &&
                          up1.PermissionString == ccPermission &&
                          up2.PermissionString == caPermission
                    select u)
                    .Include(this.Includes)
                    .ToList();
        }

        public IList<User> FindAllByProgrammePermission<TEnum>(int programmeId, TEnum permission)
        {
            return (from u in this.unitOfWork.DbContext.Set<User>()
                    join up in this.unitOfWork.DbContext.Set(UserPermission.GetPermissionEntityType(typeof(TEnum))).OfType<ProgrammePermission>() on u.UserId equals up.UserId
                    where up.ProgrammeId == programmeId && up.PermissionString == permission.ToString()
                    select u)
                    .Include(this.Includes)
                    .ToList();
        }

        public User FindByPasswordRecoveryCode(string passwordRecoveryCode)
        {
            return this.Set()
                .Where(u => u.PasswordRecoveryCode == passwordRecoveryCode)
                .SingleOrDefault();
        }

        public User FindByNewPasswordCode(string newPasswordCode)
        {
            return this.Set()
                .Where(u => u.NewPasswordCode == newPasswordCode)
                .SingleOrDefault();
        }

        public IList<UserVO> GetUsers(
            string username,
            string fullname,
            int? userOrganizationId,
            bool? active,
            bool? deleted,
            bool? locked,
            bool? hasAcceptedGDPRDeclaration,
            bool exact)
        {
            var predicate = PredicateBuilder.True<User>();

            predicate = predicate
                .AndStringMatches(p => p.Username, username, exact)
                .AndStringMatches(p => p.Fullname, fullname, exact)
                .AndEquals(p => p.UserOrganizationId, userOrganizationId)
                .AndPropertyEquals(p => p.IsSystem, false);

            if (active.HasValue)
            {
                predicate = predicate.And(p => p.IsActive == active);
            }

            if (deleted.HasValue)
            {
                predicate = predicate.And(p => p.IsDeleted == deleted);
            }

            if (locked.HasValue)
            {
                predicate = predicate.And(p => p.IsLocked == locked);
            }

            if (hasAcceptedGDPRDeclaration.HasValue)
            {
                if (hasAcceptedGDPRDeclaration == true)
                {
                    predicate = predicate.And(p => p.GDPRDeclarationAcceptDate != null);
                }
                else
                {
                    predicate = predicate.And(p => p.GDPRDeclarationAcceptDate == null);
                }
            }

            return (from user in this.unitOfWork.DbContext.Set<User>().Where(predicate)
                    join ut in this.unitOfWork.DbContext.Set<UserType>() on user.UserTypeId equals ut.UserTypeId
                    join uo in this.unitOfWork.DbContext.Set<UserOrganization>() on ut.UserOrganizationId equals uo.UserOrganizationId
                    select new UserVO
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Fullname = user.Fullname,
                        UserType = ut.Name,
                        UserOrganization = uo.Name,
                        IsActive = user.IsActive,
                        IsDeleted = user.IsDeleted,
                        IsLocked = user.IsLocked,
                        Email = user.Email,
                        HasAcceptedGDPRDeclaration = user.GDPRDeclarationAcceptDate != null,
                    })
                .ToList();
        }

        public IList<string> GetActiveUsersEmails()
        {
            return (from user in this.unitOfWork.DbContext.Set<User>()
                    where user.IsActive && !user.IsDeleted && !user.IsLocked
                    select user.Email).Distinct().ToList();
        }

        public bool IsUniqueUsername(string username, int? userId = null)
        {
            if (userId.HasValue)
            {
                return !this.unitOfWork.DbContext.Set<User>()
                    .Where(p => p.Username == username && p.UserId != userId.Value)
                    .Any();
            }
            else
            {
                return !this.unitOfWork.DbContext.Set<User>()
                    .Where(p => p.Username == username)
                    .Any();
            }
        }

        public UserRequestsWrapperVO GetUserRequests(int userId)
        {
            var results =
                   (from rp in this.unitOfWork.DbContext.Set<RequestPackage>()
                    join rpu in this.unitOfWork.DbContext.Set<RequestPackageUser>() on rp.RequestPackageId equals rpu.RequestPackageId
                    join u in this.unitOfWork.DbContext.Set<User>() on rpu.UserId equals u.UserId
                    join ut in this.unitOfWork.DbContext.Set<UserType>() on u.UserTypeId equals ut.UserTypeId
                    join uo in this.unitOfWork.DbContext.Set<UserOrganization>() on ut.UserOrganizationId equals uo.UserOrganizationId

                    join pr in this.unitOfWork.DbContext.Set<PermissionRequest>() on new { rpu.RequestPackageId, rpu.UserId } equals new { pr.RequestPackageId, pr.UserId } into g1
                    from pr in g1.DefaultIfEmpty()

                    join rdr in this.unitOfWork.DbContext.Set<RegDataRequest>() on new { rpu.RequestPackageId, rpu.UserId } equals new { rdr.RequestPackageId, rdr.UserId } into g2
                    from rdr in g2.DefaultIfEmpty()

                    join entu in this.unitOfWork.DbContext.Set<User>() on rp.EnteredByUserId equals entu.UserId into g3
                    from entu in g3.DefaultIfEmpty()

                    join checku in this.unitOfWork.DbContext.Set<User>() on rp.CheckedByUserId equals checku.UserId into g4
                    from checku in g4.DefaultIfEmpty()

                    join endu in this.unitOfWork.DbContext.Set<User>() on rp.EndedByUserId equals endu.UserId into g5
                    from endu in g5.DefaultIfEmpty()

                    where rpu.UserId == userId
                    select new { rp, rpu, u, ut, uo, pr, rdr, entu, checku, endu })
            .ToList();

            var rpus = results.Select(t => new RequestPackageUserVO
            {
                RequestPackageId = t.rp.RequestPackageId,
                UserId = t.rpu.UserId,
                PackageType = t.rp.Type,
                PackageNumber = t.rp.Code,
                PackageCreateDate = t.rp.CreateDate,
                PackageModifyDate = t.rp.ModifyDate,
                Status = t.rpu.Status,
                StatusName = t.rpu.Status,
                Username = t.u.Username,
                Fullname = t.u.Fullname,
                UserType = t.ut.Name,
                UserOrganization = t.uo.Name,
                HasPermissionRequest = t.pr != null,
                HasRegDataRequest = t.rdr != null,
                RejectionMessage = t.rpu.RejectionMessage,
                EnteredByUser = t.entu != null ? t.entu.Fullname + "(" + t.entu.Username + ")" : null,
                CheckedByUser = t.checku != null ? t.checku.Fullname + "(" + t.checku.Username + ")" : null,
                EndedByUser = t.endu != null ? t.endu.Fullname + "(" + t.endu.Username + ")" : null,
            })
            .ToList();

            var permissionRequests = rpus.Where(t => t.HasPermissionRequest).OrderBy(t => t.PackageModifyDate).ToList();
            var regDataRequests = rpus.Where(t => t.HasRegDataRequest).OrderBy(t => t.PackageModifyDate).ToList();

            return new UserRequestsWrapperVO
            {
                PermissionRequests = permissionRequests,
                RegDataRequests = regDataRequests,
            };
        }

        public int GetUserTypeId(int userId)
        {
            return this.unitOfWork.DbContext.Set<User>()
                .Where(u => u.UserId == userId)
                .Select(u => u.UserTypeId)
                .Single();
        }

        public int GetUserOrganizationId(int userId)
        {
            return this.unitOfWork.DbContext.Set<User>()
                .Where(u => u.UserId == userId)
                .Select(u => u.UserOrganizationId)
                .Single();
        }

        public string GetUserFullname(int userId)
        {
            return this.unitOfWork.DbContext.Set<User>()
                .Where(u => u.UserId == userId)
                .Select(u => u.Fullname)
                .Single();
        }

        public string GetUserUsername(int userId)
        {
            return this.unitOfWork.DbContext.Set<User>()
                .Where(u => u.UserId == userId)
                .Select(u => u.Username)
                .Single();
        }

        public string GetUserEmail(int userId)
        {
            return (from u in this.unitOfWork.DbContext.Set<User>()
                    where u.UserId == userId
                    select u.Email).Single();
        }

        public UserGDPRDeclarationInfoVO GetGDPRDeclarationInfo(int userId)
        {
            return (from u in this.unitOfWork.DbContext.Set<User>()
                    where u.UserId == userId
                    select new UserGDPRDeclarationInfoVO
                    {
                        Username = u.Username,
                        Fullname = u.Fullname,
                        Email = u.Email,
                        HasAcceptedGDPRDeclaration = u.GDPRDeclarationAcceptDate != null,
                    })
                    .Single();
        }

        public IList<UserDeclarationVO> GetUserDeclarations(int userId)
        {
            DateTime gdprActivationDate = DateTime.Parse("25.05.2018");

            var result = (from declaration in this.unitOfWork.DbContext.Set<Declaration>().Where(d => d.Status != DeclarationStatus.Draft)
                          join userDeclaration in this.unitOfWork.DbContext.Set<UserDeclaration>().Where(ud => ud.UserId == userId)
                          on declaration.DeclarationId equals userDeclaration.DeclarationId
                          select new UserDeclarationVO
                          {
                              DeclarationId = declaration.DeclarationId,
                              NameBg = declaration.Name,
                              NameEn = declaration.NameAlt,
                              Status = declaration.Status,
                              ActivationDate = declaration.ActivationDate,
                              IsAccepted = true,
                              AcceptDate = userDeclaration.AcceptDate,
                          })
                    .ToList();

            var userGDPRDeclatation = (from u in this.unitOfWork.DbContext.Set<User>()
                                       where u.UserId == userId && u.GDPRDeclarationAcceptDate.HasValue
                                       select new
                                       {
                                           DeclarationId = (int?)null,
                                           NameBg = "Декларация - лични данни",
                                           NameEn = "GDPR compliance declaration",
                                           Status = DeclarationStatus.Published,
                                           ActivationDate = gdprActivationDate,
                                           IsAccepted = true,
                                           AcceptedDate = u.GDPRDeclarationAcceptDate,
                                       })
                                       .SingleOrDefault();

            if (userGDPRDeclatation != null)
            {
                result.Add(
                new UserDeclarationVO
                {
                    NameBg = userGDPRDeclatation.NameBg,
                    NameEn = userGDPRDeclatation.NameEn,
                    Status = userGDPRDeclatation.Status,
                    ActivationDate = userGDPRDeclatation.ActivationDate,
                    IsAccepted = userGDPRDeclatation.IsAccepted,
                    AcceptDate = userGDPRDeclatation.AcceptedDate,
                });
            }

            return result;
        }

        public IList<UserUnacceptedDeclarationVO> GetUserUnacceptedDeclarations(int userId)
        {
            var acceptedDeclarations = this.unitOfWork
                .DbContext.Set<UserDeclaration>()
                .Where(ud => ud.UserId == userId);

            var declarations = this.unitOfWork.DbContext
                .Set<Declaration>()
                .Where(d => d.Status == DeclarationStatus.Published && d.ActivationDate <= DateTime.Now)
                .OrderBy(d => d.CreateDate)
                .Select(d => new UserUnacceptedDeclarationVO()
                {
                    DeclarationId = d.DeclarationId,
                    NameBg = d.Name,
                    NameEn = d.NameAlt,
                    ContentBg = d.Content,
                    ContentEn = d.ContentAlt,
                    DeclarationFiles = d.DeclarationFiles.Select(df => new DeclarationFileVO()
                    {
                        Name = df.Name,
                        Description = df.Description,
                        Key = df.BlobKey,
                    })
                    .ToList(),
                });

            return declarations
                .Where(d => !acceptedDeclarations.Any(ad => ad.DeclarationId == d.DeclarationId))
                .ToList();
        }

        public bool UserExists(string uin)
        {
            return this.unitOfWork.DbContext.Set<User>().Where(u => !u.IsDeleted && u.Uin == uin).Any();
        }

        public bool OtherUserExists(int currentUserId, string uin)
        {
            return this.unitOfWork.DbContext.Set<User>().Where(u => !u.IsDeleted && u.UserId != currentUserId && u.Uin == uin).Any();
        }
    }
}
