using Dapper;
using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Volunteers
{
    public class VolunteerPhoneRepository : PostgresRepository<VolunteerPhone>, IVolunteerPhoneRepository
    {
        public VolunteerPhoneRepository(NOGContext context)
            : base(context)
        {

        }

        public override IEnumerable<VolunteerPhone> GetAll()
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_type AS Type,
                            st_number AS Number,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.volunteer_phone";
            return cn.Query<VolunteerPhone>(sql);
        }

        public override VolunteerPhone GetById(Int64 id)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_type AS Type,
                            st_number AS Number,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.volunteer_phone WHERE in_id = @id";
            var result = cn.Query<VolunteerPhone>(sql, new { id = id });
            return result.FirstOrDefault();
        }
    }
}
