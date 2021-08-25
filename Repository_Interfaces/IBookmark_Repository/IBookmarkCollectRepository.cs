using ssa_database.Models.Bookmark_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Repository_Interfaces.IBookmark_Repository
{
    
    public interface IBookmarkCollectRepository
    {
        Task<bool> AddBookmark(BookmarkCollect bookmarkCollect);
        Task<bool> RemoveBookmark(BookmarkCollect bookmarkCollect);
        Task<bool> Save();
    }
}
