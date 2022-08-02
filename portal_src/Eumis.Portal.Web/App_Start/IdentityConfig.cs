using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Eumis.Portal.Web.Models;
using Eumis.Portal.Web.App_Start;
using Microsoft.Owin.Security.Cookies;

namespace Eumis.Portal.Web
{
    public static class ClaimKeys
    {
        // common
        public const string Email = "user/email";
        public const string AccessToken = "user/access_token";
        public const string FirstName = "user/first_name";
        public const string LastName = "user/last_name";
        public const string Phone = "user/phone";
        public const string HasMessages = "user/has_messages";
        public const string HasNewMessages = "user/has_new_messages";
        public const string HasNewCommunications = "user/has_new_communications";

        // private
        public const string IsPrivate = "user/is_private";

        //report
        public const string UserType = "user/user_type";
        public const string Permissions = "user/permissions";
    }

    public class EumisUser : IdentityUser
    {
        public string AccessToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsPrivate { get; set; }

        public ReportUserType UserType { get; set; }
        public string Permissions { get; set; }

        public ReportUserPermissions ReadPermissions
        {
            get
            {
                return ReportUserPermissions.Read(this.Permissions);
            }
        }

        public bool HasMessages
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[ClaimKeys.HasMessages] == null)
                {
                    return false;
                }

                return (bool)System.Web.HttpContext.Current.Session[ClaimKeys.HasMessages];
            }
            set
            {
                System.Web.HttpContext.Current.Session[ClaimKeys.HasMessages] = value;
            }
        }

        public bool HasNewMessages
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[ClaimKeys.HasNewMessages] == null)
                {
                    return false;
                }

                return (bool)System.Web.HttpContext.Current.Session[ClaimKeys.HasNewMessages];
            }
            set
            {
                System.Web.HttpContext.Current.Session[ClaimKeys.HasNewMessages] = value;
            }
        }

        public bool HasNewCommunications
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[ClaimKeys.HasNewCommunications] == null)
                {
                    return false;
                }

                return (bool)System.Web.HttpContext.Current.Session[ClaimKeys.HasNewCommunications];
            }
            set
            {
                System.Web.HttpContext.Current.Session[ClaimKeys.HasNewCommunications] = value;
            }
        }

        public ClaimsIdentity GenerateUserIdentityAsync(EumisUserManager manager, string authenticationType)
        {
            var userIdentity = new ClaimsIdentity(authenticationType);

            userIdentity.AddClaim(new Claim(ClaimKeys.Email, this.Email));
            userIdentity.AddClaim(new Claim(ClaimKeys.FirstName, this.FirstName ?? string.Empty));
            userIdentity.AddClaim(new Claim(ClaimKeys.LastName, this.LastName ?? string.Empty));
            userIdentity.AddClaim(new Claim(ClaimKeys.Phone, this.Phone ?? string.Empty));
            userIdentity.AddClaim(new Claim(ClaimKeys.AccessToken, this.AccessToken));
            userIdentity.AddClaim(new Claim(ClaimKeys.IsPrivate, this.IsPrivate.ToString()));
            userIdentity.AddClaim(new Claim(ClaimKeys.UserType, this.UserType.ToString()));
            userIdentity.AddClaim(new Claim(ClaimKeys.Permissions, this.Permissions ?? string.Empty));

            return userIdentity;
        }
    }

    public enum ReportUserType
    {
        Parent,
        Child
    }

    [Serializable]
    public class ReportUserPermissions
    {
        public bool canReadContracts { get; set; }

        public bool canReadProcurements { get; set; }
        public bool canWriteProcurements { get; set; }

        public bool canReadSpendingPlan { get; set; }
        public bool canWriteSpendingPlan { get; set; }

        public bool canReadTechnicalPlan { get; set; }
        public bool canWriteTechnicalPlan { get; set; }

        public bool canReadFinancialPlan { get; set; }
        public bool canWriteFinancialPlan { get; set; }

        public bool canReadPaymentRequest { get; set; }
        public bool canWritePaymentRequest { get; set; }

        public bool canReadCommunication { get; set; }

        public bool canReadMicrodata { get; set; }
        public bool canWriteMicrodata { get; set; }

        public static string LoadPermissions(
            Eumis.Documents.Contracts.AccessCodePermissionPVO permissions)
        {
            return ReportUserPermissions.Write(
                new ReportUserPermissions()
                {
                    canReadContracts = permissions.canReadContracts,

                    canReadProcurements = permissions.canReadProcurements,
                    canWriteProcurements = permissions.canWriteProcurements,

                    canReadSpendingPlan = permissions.canReadSpendingPlan,
                    canWriteSpendingPlan = permissions.canWriteSpendingPlan,

                    canReadTechnicalPlan = permissions.canReadTechnicalPlan,
                    canWriteTechnicalPlan = permissions.canWriteTechnicalPlan,

                    canReadFinancialPlan = permissions.canReadFinancialPlan,
                    canWriteFinancialPlan = permissions.canWriteFinancialPlan,

                    canReadPaymentRequest = permissions.canReadPaymentRequest,
                    canWritePaymentRequest = permissions.canWritePaymentRequest,

                    canReadCommunication = permissions.canReadCommunication,

                    canReadMicrodata = permissions.canReadMicrodata,
                    canWriteMicrodata = permissions.canWriteMicrodata,
                });
        }
        public static string GrandFullPermissions()
        {
            return ReportUserPermissions.Write(
                new ReportUserPermissions()
                {
                    canReadContracts = true,

                    canReadProcurements = true,
                    canWriteProcurements = true,

                    canReadSpendingPlan = true,
                    canWriteSpendingPlan = true,

                    canReadTechnicalPlan = true,
                    canWriteTechnicalPlan = true,

                    canReadFinancialPlan = true,
                    canWriteFinancialPlan = true,

                    canReadPaymentRequest = true,
                    canWritePaymentRequest = true,

                    canReadCommunication = true,

                    canReadMicrodata = true,
                    canWriteMicrodata = true
                });
        }

        public static string Write(ReportUserPermissions permissions)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(ms, permissions);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static ReportUserPermissions Read(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (ReportUserPermissions)
                    new System.Runtime.Serialization.Formatters.Binary
                        .BinaryFormatter().Deserialize(ms);
            }
        }
    }

    //public class ReportEumisUser : EumisUser
    //{
    //    public ReportUserType UserType { get; set; }

    //    public override Task<ClaimsIdentity> GenerateUserIdentityAsync(EumisUserManager manager, string authenticationType)
    //    {
    //        return base.GenerateUserIdentityAsync(manager, authenticationType)
    //            .ContinueWith(task =>
    //            {
    //                var userIdentity = task.Result;

    //                userIdentity.AddClaim(new Claim(ClaimKeys.UserType, this.UserType.ToString()));

    //                return userIdentity;
    //            });
    //    }
    //}

    public class EumisUserManager : UserManager<EumisUser>
    {
        public EumisUserManager(IUserStore<EumisUser> store) : base(store) { }

        public static EumisUserManager Create(IdentityFactoryOptions<EumisUserManager> options, IOwinContext context)
        {
            var manager = new EumisUserManager(new UserStore<EumisUser>());
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<EumisUser>(dataProtectionProvider.Create("__eumis__identity__"));
            }

            return manager;
        }

        public static EumisUser LoadUser(ClaimsIdentity identity)
        {
            return new EumisUser()
            {
                Email = identity.FindFirstValue(ClaimKeys.Email),
                FirstName = identity.FindFirstValue(ClaimKeys.FirstName),
                LastName = identity.FindFirstValue(ClaimKeys.LastName),
                Phone = identity.FindFirstValue(ClaimKeys.Phone),
                AccessToken = identity.FindFirstValue(ClaimKeys.AccessToken),
                IsPrivate = !string.IsNullOrEmpty(identity.FindFirstValue(ClaimKeys.IsPrivate)) ? bool.Parse(identity.FindFirstValue(ClaimKeys.IsPrivate)) : false,
                UserType = identity.FindFirstValue(ClaimKeys.UserType) != null ? (ReportUserType)Enum.Parse(typeof(ReportUserType), identity.FindFirstValue(ClaimKeys.UserType), true) : ReportUserType.Parent,
                Permissions = identity.FindFirstValue(ClaimKeys.Permissions)
            };
        }

        //public static ReportEumisUser LoadReportUser(ClaimsIdentity identity)
        //{
        //    var baseUser = EumisUserManager.LoadUser(identity);

        //    return new ReportEumisUser()
        //    {
        //        Email = baseUser.Email,
        //        FirstName = baseUser.FirstName,
        //        LastName = baseUser.LastName,
        //        Phone = baseUser.Phone,
        //        AccessToken = baseUser.AccessToken,
        //        IsPrivate = baseUser.IsPrivate,
        //        UserType = (ReportUserType)Enum.Parse(typeof(ReportUserType), identity.FindFirstValue(ClaimKeys.UserType), true)
        //    };
        //}
    }

    public class PublicSignInManager : IDisposable
    {
        private EumisUserManager userManager;
        private IAuthenticationManager authenticationManager;
        private bool disposed;

        public PublicSignInManager(EumisUserManager userManager, IAuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.authenticationManager = authenticationManager;
        }

        public static PublicSignInManager Create(IdentityFactoryOptions<PublicSignInManager> options, IOwinContext context)
        {
            return new PublicSignInManager(context.GetUserManager<EumisUserManager>(), context.Authentication);
        }

        public ClaimsIdentity CreateUserIdentity(EumisUser user)
        {
            return user.GenerateUserIdentityAsync((EumisUserManager)this.userManager, EumisAuthenticationTypes.Public);
        }

        public void SignIn(EumisUser user, bool isPersistent, bool rememberBrowser)
        {
            var userIdentity = CreateUserIdentity(user);

            this.authenticationManager.SignOut();

            var authenticationProperties = new AuthenticationProperties() { IsPersistent = isPersistent };

            if (rememberBrowser)
            {
                var rememberBrowserIdentity = this.authenticationManager
                    .CreateTwoFactorRememberBrowserIdentity(user.Email);

                this.authenticationManager.SignIn(
                    authenticationProperties,
                    userIdentity,
                    rememberBrowserIdentity);
            }
            else
            {
                this.authenticationManager.SignIn(
                    authenticationProperties,
                    userIdentity);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    this.userManager.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }

    public class PrivateSignInManager : IDisposable
    {
        private EumisUserManager userManager;
        private IAuthenticationManager authenticationManager;
        private bool disposed;

        public PrivateSignInManager(EumisUserManager userManager, IAuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.authenticationManager = authenticationManager;
        }

        public static PrivateSignInManager Create(IdentityFactoryOptions<PrivateSignInManager> options, IOwinContext context)
        {
            return new PrivateSignInManager(context.GetUserManager<EumisUserManager>(), context.Authentication);
        }

        public ClaimsIdentity CreateUserIdentity(EumisUser user)
        {
            return user.GenerateUserIdentityAsync((EumisUserManager)this.userManager, EumisAuthenticationTypes.Private);
        }

        public void SignIn(EumisUser user, bool isPersistent, bool rememberBrowser)
        {
            var userIdentity = CreateUserIdentity(user);

            this.authenticationManager.SignOut();

            var authenticationProperties = new AuthenticationProperties() { IsPersistent = isPersistent };

            if (rememberBrowser)
            {
                var rememberBrowserIdentity = this.authenticationManager
                    .CreateTwoFactorRememberBrowserIdentity(user.Email);

                this.authenticationManager.SignIn(
                    authenticationProperties,
                    userIdentity,
                    rememberBrowserIdentity);
            }
            else
            {
                this.authenticationManager.SignIn(
                    authenticationProperties,
                    userIdentity);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    this.userManager.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }

    public class ReportSignInManager : IDisposable
    {
        private EumisUserManager userManager;
        private IAuthenticationManager authenticationManager;
        private bool disposed;

        public ReportSignInManager(EumisUserManager userManager, IAuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.authenticationManager = authenticationManager;
        }

        public static ReportSignInManager Create(IdentityFactoryOptions<ReportSignInManager> options, IOwinContext context)
        {
            return new ReportSignInManager(context.GetUserManager<EumisUserManager>(), context.Authentication);
        }

        public ClaimsIdentity CreateUserIdentity(EumisUser user)
        {
            return user.GenerateUserIdentityAsync((EumisUserManager)this.userManager, EumisAuthenticationTypes.Report);
        }

        public void SignIn(EumisUser user, bool isPersistent, bool rememberBrowser)
        {
            var userIdentity = CreateUserIdentity(user);

            this.authenticationManager.SignOut();

            var authenticationProperties = new AuthenticationProperties() { IsPersistent = isPersistent };

            if (rememberBrowser)
            {
                var rememberBrowserIdentity = this.authenticationManager
                    .CreateTwoFactorRememberBrowserIdentity(user.Email);

                this.authenticationManager.SignIn(
                    authenticationProperties,
                    userIdentity,
                    rememberBrowserIdentity);
            }
            else
            {
                this.authenticationManager.SignIn(
                    authenticationProperties,
                    userIdentity);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    this.userManager.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
