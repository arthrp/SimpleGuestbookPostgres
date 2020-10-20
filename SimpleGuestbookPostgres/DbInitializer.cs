using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGuestbookPostgres
{
    public class DbInitializer
    {
        private readonly string _connectionString;
        public DbInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Init()
        {
            var sql = File.ReadAllText("Sql/InitDb.sql");

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                var rowCount = conn.Execute(sql);
            }
        }
    }
}
