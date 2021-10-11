using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Bookmark_Models;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Repository_Interfaces.IBookmark_Repository;

namespace user_stuff_share_app.Repository.Bookmark_Repository
{

    public class BookmarkCollectRepository : IBookmarkCollectRepository
        {
            private readonly ApplicationDbContext _db;
        
            public BookmarkCollectRepository(ApplicationDbContext db)
            {
                _db = db;
              
            }

            public async Task<bool> AddBookmark(BookmarkCollect bookmarkCollect)
            {
                await _db.BookmarkCollect.AddAsync(bookmarkCollect);
                return await Save();
            }

            public async Task<bool> RemoveBookmark(ReqIdUserId reqIdUserId)
            {
            BookmarkCollect bookmarkCollect = await _db.BookmarkCollect.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == reqIdUserId.UserId && c.CollectId == reqIdUserId.Id);
            _db.BookmarkCollect.Remove(bookmarkCollect);
            return await Save();
            }

            public async Task<bool> Save()
            {
                return await _db.SaveChangesAsync() >= 0 ? true : false; ;
            }
        }
}
