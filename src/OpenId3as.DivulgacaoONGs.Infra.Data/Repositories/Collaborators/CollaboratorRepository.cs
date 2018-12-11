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
    public class CollaboratorRepository : PostgresRepository<Collaborator>, ICollaboratorRepository
    {
        public CollaboratorRepository(NOGContext context)
            : base(context)
        {

        }

        public override IEnumerable<Collaborator> GetAll()
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            st_first_name AS FirstName,
                            st_first_name AS LastName,
                            st_email AS Email,
                            dt_birth AS Birth,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.collaborator";
            return cn.Query<Collaborator>(sql);
        }

        public override Collaborator GetById(long id)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            st_first_name AS FirstName,
                            st_first_name AS LastName,
                            st_email AS Email,
                            dt_birth AS Birth,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.collaborator WHERE in_id = @id";
            var result = cn.Query<Collaborator>(sql, new { id = id });
            return result.FirstOrDefault();
        }
    }
}
