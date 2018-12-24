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
	                        st_login AS Login,
	                        st_password AS Password,
	                        st_first_name AS FirstName,
	                        st_last_name AS LastName,
	                        st_email AS Email,
	                        bo_first_access AS FirstAccess,
	                        ts_created AS Created,
	                        ts_updated AS Updated,
	                        bo_active AS Active
                        FROM nog.user";
            return cn.Query<User>(sql);
        }

        public override User GetById(long id)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
	                        in_id AS Id,
	                        st_login AS Login,
	                        st_password AS Password,
	                        st_first_name AS FirstName,
	                        st_last_name AS LastName,
	                        st_email AS Email,
	                        bo_first_access AS FirstAccess,
	                        ts_created AS Created,
	                        ts_updated AS Updated,
	                        bo_active AS Active
                        FROM nog.user WHERE in_id = @id";
            var result = cn.Query<User>(sql, new { id = id });
            return result.FirstOrDefault();
        }

        public User GetByLogin(string login)
        {
            var cn = Db.Database.GetDbConnection();
            var sql = @"SELECT
	                        in_id AS Id,
	                        st_login AS Login,
	                        st_password AS Password,
	                        st_first_name AS FirstName,
	                        st_last_name AS LastName,
	                        st_email AS Email,
	                        bo_first_access AS FirstAccess,
	                        ts_created AS Created,
	                        ts_updated AS Updated,
	                        bo_active AS Active
                        FROM nog.user WHERE st_login = @login";
            var result = cn.Query<User>(sql, new { login = login });
            return result.FirstOrDefault();
        }
    }
}