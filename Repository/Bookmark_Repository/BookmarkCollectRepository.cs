using AutoMapper;
using ssa_database.Models.Bookmark_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Repository_Interfaces.IBookmark_Repository;

namespace user_stuff_share_app.Repository.Bookmark_Repository
{

    public class BookmarkCollectRepository : IBookmarkCollectRepository
        {
            private readonly ApplicationDbContext _dbBookmark;
            private readonly IMapper _mapper;

            public BookmarkCollectRepository(ApplicationDbContext dbBookmark, IMapper mapper)
            {
                _dbBookmark = dbBookmark;
                _mapper = mapper;
            }

            public async Task<bool> AddBookmark(BookmarkCollect bookmarkCollect)
            {
                await _dbBookmark.BookmarkCollect.AddAsync(bookmarkCollect);
                return await Save();
            }

            public async Task<bool> RemoveBookmark(BookmarkCollect bookmarkCollect)
            {
                _dbBookmark.BookmarkCollect.Remove(bookmarkCollect);
                return await Save();
            }

            public async Task<bool> Save()
            {
                return await _dbBookmark.SaveChangesAsync() >= 0 ? true : false; ;
            }
        }
}
