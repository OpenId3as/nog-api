using Dapper;
using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Institutions
{
    public class InstitutionAddressRepository : PostgresRepository<InstitutionAddress>, IInstitutionAddressRepository
    {
        public InstitutionAddressRepository(NOGContext context)
            : base(context)
        {

        }

        public override IEnumerable<InstitutionAddress> GetAll()
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_postal_code AS PostalCode,
                            st_name AS Name,
                            in_number AS Number,
                            st_neighborhood AS Neighborhood,
                            st_city AS City,
                            st_federated_state AS FederatedState,
                            st_complement AS Complement,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.institution_address";
            return cn.Query<InstitutionAddress>(sql);
        }

        public override InstitutionAddress GetById(long id)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_postal_code AS PostalCode,
                            st_name AS Name,
                            in_number AS Number,
                            st_neighborhood AS Neighborhood,
                            st_city AS City,
                            st_federated_state AS FederatedState,
                            st_complement AS Complement,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.institution_address WHERE in_id = @id";
            var result = cn.Query<InstitutionAddress>(sql, new { id = id });
            return result.FirstOrDefault();
        }
    }
}
