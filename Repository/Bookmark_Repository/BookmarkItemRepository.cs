using Microsoft.EntityFrameworkCore;
using ssa_database.Models.Bookmark_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Repository_Interfaces.IBookmark_Repository;

namespace user_stuff_share_app.Repository.Bookmark_Repository
{
    public class BookmarkItemRepository:IBookmarkItemRepository
    {
        private readonly ApplicationDbContext _db;

        public BookmarkItemRepository(ApplicationDbContext db)
        {
            _db = db;

        }

        public async Task<bool> AddBookmark(BookmarkItem bookmarkItem)
        {
            await _db.BookmarkItem.AddAsync(bookmarkItem);
            return await Save();
        }

        public async Task<bool> RemoveBookmark(ReqIdUserId reqIdUserId)
        {
            BookmarkItem bookmarkItem = await _db.BookmarkItem.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == reqIdUserId.UserId && c.ItemId == reqIdUserId.Id);
            _db.BookmarkItem.Remove(bookmarkItem);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false; ;
        }
    }
}
