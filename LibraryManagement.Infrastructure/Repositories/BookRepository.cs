using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
