﻿using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface IVolunteerAddressRepository : IPostgresRepository<VolunteerAddress>, IDisposable
    {
    }
}
