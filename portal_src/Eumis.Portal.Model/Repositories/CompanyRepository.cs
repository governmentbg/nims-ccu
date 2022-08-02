using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Portal.Model.Entities;
using Eumis.Common.Data;

namespace Eumis.Portal.Model.Repositories
{
    public interface ICompanyRepository
    {
        IEnumerable<KidCode> GetAllKidCodes();
        IEnumerable<CompanyType> GetAllCompanyTypes();
        IEnumerable<CompanyLegalType> GetAllCompanyLegalTypes();
        IEnumerable<CompanySizeType> GetAllCompanySizeTypes();
    }

    public class CompanyRepository : ICompanyRepository
    {
        private IUnitOfWork _unitOfWork;

        public CompanyRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<KidCode> GetAllKidCodes()
        {
            return _unitOfWork.DbContext.Set<KidCode>().AsEnumerable();
        }

        public IEnumerable<CompanyType> GetAllCompanyTypes()
        {
            return _unitOfWork.DbContext.Set<CompanyType>().AsEnumerable();
        }

        public IEnumerable<CompanyLegalType> GetAllCompanyLegalTypes()
        {
            return _unitOfWork.DbContext.Set<CompanyLegalType>().Include("CompanyType").AsEnumerable();
        }

        public IEnumerable<CompanySizeType> GetAllCompanySizeTypes()
        {
            return _unitOfWork.DbContext.Set<CompanySizeType>().AsEnumerable();
        }
    }
}
