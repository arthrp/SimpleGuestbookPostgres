using SimpleGuestbookPostgres.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGuestbookPostgres.Repositories
{
    public interface IPostsRepository
    {
        List<GuesbookPostDto> GetAll();
        void Add(GuesbookPostDto post);
    }
}
