using Dapper;
using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
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

        public int GetCountPagedSearch(string firstName = "", string lastName = "", string email = "", bool? active = null)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT COUNT(*) FROM nog.collaborator WHERE 1 = 1";
            if (!string.IsNullOrEmpty(firstName)) sql = string.Concat(sql, $" AND LOWER(st_first_name) LIKE '%{firstName.ToLower()}%'");
            if (!string.IsNullOrEmpty(lastName)) sql = string.Concat(sql, $" AND LOWERst_last_name) LIKE '%{lastName.ToLower()}%'");
            if (!string.IsNullOrEmpty(email)) sql = string.Concat(sql, $" AND LOWERst_email) LIKE '%{email.ToLower()}%'");
            if (active.HasValue) sql = string.Concat(sql, $" AND bo_active = {active}");
            return cn.Query<int>(sql).FirstOrDefault();
        }

        public IEnumerable<Collaborator> FindWithPagedSearch(string sort = "", int limitRows = 50, int page = 0, string firstName = "", string lastName = "", string email = "", bool? active = null)
        {
            var cn = Db.Database.GetDbConnection();

            var sql = @"SELECT in_id AS Id, 
                            st_first_name AS FirstName,
                            st_last_name AS LastName,
                            st_email AS Email,
                            dt_birth AS Birth,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.collaborator WHERE 1 = 1";

            if (!string.IsNullOrEmpty(firstName))
                sql = string.Concat(sql, $" AND LOWER(st_first_name) LIKE '%{firstName.ToLower()}%'");

            if (!string.IsNullOrEmpty(lastName))
                sql = string.Concat(sql, $" AND LOWER(st_last_name) LIKE '%{lastName.ToLower()}%'");

            if (!string.IsNullOrEmpty(email))
                sql = string.Concat(sql, $" AND LOWER(st_email) LIKE '%{email.ToLower()}%'");

            if (active.HasValue)
                sql = string.Concat(sql, $" AND bo_active = {active}");

            if (!string.IsNullOrEmpty(sort))
                sql = string.Concat(sql, $" ORDER BY {sort}");

            sql = string.Concat(sql, $" LIMIT {limitRows} OFFSET {page}");

            return cn.Query<Collaborator>(sql);
        }

        public override IEnumerable<Collaborator> GetAll()
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            st_first_name AS FirstName,
                            st_last_name AS LastName,
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
                            st_last_name AS LastName,
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
