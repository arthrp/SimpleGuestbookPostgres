﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGuestbookPostgres.Dto
{
    [Table("public.posts")]
    public class GuestbookPostDto
    {
        //Weird postgres crap
        public int Id { get; set; }
        public string Posttext { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
