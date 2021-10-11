using ssa_database.Models.Bookmark_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;

namespace user_stuff_share_app.Repository_Interfaces.IBookmark_Repository
{
    public interface IBookmarkItemRepository
    {
        Task<bool> AddBookmark(BookmarkItem bookmarkItem);
        Task<bool> RemoveBookmark(ReqIdUserId reqIdUserId);
        Task<bool> Save();
    }
}
