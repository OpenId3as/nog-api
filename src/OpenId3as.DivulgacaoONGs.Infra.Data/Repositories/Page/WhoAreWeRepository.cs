﻿using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class WhoAreWeRepository : MongoRepository<WhoAreWe>, IWhoAreWeRepository
    {
        public WhoAreWeRepository(NOGContext context)
            : base(context)
        {

        }
    }
}
