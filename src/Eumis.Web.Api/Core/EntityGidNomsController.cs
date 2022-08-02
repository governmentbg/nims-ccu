﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.Core
{
    public class EntityGidNomsController<TEntity, TNomVO> : ApiController
    {
        private IEntityGidNomsRepository<TEntity, TNomVO> nomsRepository;

        public EntityGidNomsController(IEntityGidNomsRepository<TEntity, TNomVO> nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("{id:int}")]
        public virtual TNomVO GetNom(int id)
        {
            return this.nomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<TNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            return this.nomsRepository.GetNoms(term, offset, limit);
        }
    }
}
