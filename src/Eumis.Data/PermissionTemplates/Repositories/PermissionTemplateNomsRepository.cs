using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.PermissionTemplates;

namespace Eumis.Data.PermissionTemplates.Repositories.Noms
{
    internal class PermissionTemplateNomsRepository : EntityNomsRepository<PermissionTemplate, EntityNomVO>
    {
        public PermissionTemplateNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.PermissionTemplateId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.PermissionTemplateId,
                    Name = t.Name,
                })
        {
        }
    }
}
