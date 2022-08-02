namespace Eumis.ApplicationServices.Services.Company
{
    public interface ICompanyCreationService
    {
        Eumis.Domain.Companies.Company CreateFromRioCompany(Rio.Company company);

        Rio.Company CreateRioCompany(Eumis.Domain.Companies.Company company);
    }
}
