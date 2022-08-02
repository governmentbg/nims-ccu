using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.Procedures;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.Repositories
{
    internal class ProgrammePriorityCompanyNomsRepository : Repository, IProgrammePriorityCompanyNomsRepository
    {
        public ProgrammePriorityCompanyNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<EntityNomVO> GetProgrammePriorityCompanyNoms(ProgrammePriorityType type, string term, bool higherOrderCompany = false, int offset = 0, int? limit = null)
        {
            var availableType = this.GetAvailableProgrammePriorityTypes(type, higherOrderCompany);

            var predicate = PredicateBuilder.True<Company>()
                .And(x => availableType.Contains(x.ProgrammePriorityType))
                .AndAnyStringContains(pp => pp.Name, pp => pp.NameAlt, term);

            var result = (from c in this.unitOfWork.DbContext.Set<Company>().Where(predicate)
                          select new EntityNomVO
                          {
                              NomValueId = c.CompanyId,
                              Name = c.Name,
                              NameAlt = c.NameAlt,
                          })
                          .OrderBy(t => t.Name)
                          .WithOffsetAndLimit(offset, limit)
                          .ToList();

            return result;
        }

        private IList<ProgrammePriorityType> GetAvailableProgrammePriorityTypes(ProgrammePriorityType programmePriorityType, bool higherOrderCompany)
        {
            var listTypes = new List<ProgrammePriorityType>();
            if (!higherOrderCompany)
            {
                listTypes.Add(programmePriorityType);
            }
            else
            {
                if (programmePriorityType == ProgrammePriorityType.ThirdClass)
                {
                    listTypes.Add(ProgrammePriorityType.SecondClass);
                }

                if (programmePriorityType == ProgrammePriorityType.SecondClass)
                {
                    listTypes.Add(ProgrammePriorityType.FirstClass);
                }

                if (programmePriorityType == ProgrammePriorityType.FirstClass)
                {
                    throw new DomainValidationException($"Invalid {nameof(programmePriorityType)} request");
                }
            }

            return listTypes;
        }

        private EntityNomVO Selector(int id, string name, string nameAlt)
        {
            return new EntityNomVO
            {
                NomValueId = id,
                Name = name,
                NameAlt = !string.IsNullOrWhiteSpace(nameAlt) ? nameAlt : null,
            };
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from pp in this.unitOfWork.DbContext.Set<Company>()
                    where pp.CompanyId == nomValueId
                    select new
                    {
                        pp.CompanyId,
                        pp.Name,
                        pp.NameAlt,
                    })
                    .ToList() // enumerate to enable complex expressions
                    .Select(pp => this.Selector(pp.CompanyId, pp.Name, pp.NameAlt))
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return new List<EntityNomVO>();
        }
    }
}
