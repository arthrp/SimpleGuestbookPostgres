﻿using Dapper.Contrib.Extensions;
using Npgsql;
using SimpleGuestbookPostgres.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGuestbookPostgres.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly string _connectionString;

        public PostsRepository(AppSettings appSettings)
        {
            _connectionString = appSettings.ConnectionString;
        }

        public void Add(GuestbookPostDto post)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                conn.Insert<GuestbookPostDto>(post);

                conn.Close();
            }
        }

        public List<GuestbookPostDto> GetAll()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                var result = conn.GetAll<GuestbookPostDto>();

                conn.Close();

                return result.ToList();
            }
        }
    }
}
