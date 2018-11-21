using Dapper;
using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Collaborators
{
    public class CollaboratorPhoneRepository : PostgresRepository<CollaboratorPhone>, ICollaboratorPhoneRepository
    {
        public CollaboratorPhoneRepository(NOGContext context)
            : base(context)
        {

        }

        public override IEnumerable<CollaboratorPhone> GetAll()
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_type AS Type,
                            st_number AS Number,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.collaborator_phone";
            return cn.Query<CollaboratorPhone>(sql);
        }

        public override CollaboratorPhone GetById(Int64 id)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            in_type AS Type,
                            st_number AS Number,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.collaborator_phone WHERE in_id = @id";
            var result = cn.Query<CollaboratorPhone>(sql, new { id = id });
            return result.FirstOrDefault();
        }
    }
}
