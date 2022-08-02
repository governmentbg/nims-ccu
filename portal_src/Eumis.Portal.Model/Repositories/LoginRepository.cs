using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Portal.Model.Entities;
using Eumis.Common.Data;

namespace Eumis.Portal.Model.Repositories
{
    public interface ILoginRepository
    {
        LoginCertificate CreateLoginCertificate();
    }

    public class LoginRepository : ILoginRepository
    {
        private IUnitOfWork _unitOfWork;

        public LoginRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LoginCertificate CreateLoginCertificate()
        {
            LoginCertificate login = new LoginCertificate()
            {
                LoginDate = DateTime.Now
            };

            _unitOfWork.DbContext.Set<LoginCertificate>().Add(login);

            return login;
        }
    }
}
