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

        public string GetAllTables()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                var x = conn.Query<string>("SELECT table_schema || '.' || table_name FROM information_schema.tables");

                var y = conn.Query("select column_name, data_type, character_maximum_length from INFORMATION_SCHEMA.COLUMNS where table_name = 'posts'");

                return x.FirstOrDefault();
            }
        }
    }
}
