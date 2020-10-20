using SimpleGuestbookPostgres.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGuestbookPostgres.Models
{
    public class GuestbookViewModel
    {
        public List<GuestbookPostDto> AllPosts { get; set; }
    }
}
