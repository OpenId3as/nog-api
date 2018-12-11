using Dapper;
using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Institutions
{
    public class InstitutionPhoneRepository : PostgresRepository<InstitutionPhone>, IInstitutionPhoneRepository
    {
        public InstitutionPhoneRepository(NOGContext context)
            : base(context)
        {

        }

        public override IEnumerable<InstitutionPhone> GetAll()
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_type AS Type,
                            st_number AS Number,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.institution_phone";
            return cn.Query<InstitutionPhone>(sql);
        }

        public override InstitutionPhone GetById(long id)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_type AS Type,
                            st_number AS Number,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.institution_phone WHERE in_id = @id";
            var result = cn.Query<InstitutionPhone>(sql, new { id = id });
            return result.FirstOrDefault();
        }
    }
}
    