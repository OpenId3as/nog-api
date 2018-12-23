using Dapper;
using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Domain.Entities;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories
{
    public class UserRepository : PostgresRepository<User>, IUserRepository
    {
        public UserRepository(NOGContext context)
            : base(context)
        {

        }

        public override IEnumerable<User> GetAll()
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            st_legal_name AS LegalName,
                            st_trade_name AS TradeName,
                            st_email AS Email,
                            dt_foundation AS Foundation,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.institution";
            return cn.Query<User>(sql);
        }

        public override User GetById(long id)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
                            in_id AS Id,
                            st_legal_name AS LegalName,
                            st_trade_name AS TradeName,
                            st_email AS Email,
                            dt_foundation AS Foundation,
                            ts_created AS Created,
                            ts_updated AS Updated,
                            bo_active AS Active
                        FROM nog.institution WHERE in_id = @id";
            var result = cn.Query<User>(sql, new { id = id });
            return result.FirstOrDefault();
        }
    }
}