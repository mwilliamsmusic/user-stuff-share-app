﻿using System.Collections.Generic;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Tag_Collect_Dtos;

namespace user_stuff_share_app.Repository_Interfaces.ITag_Repository
{
    public interface ITagCollectRepository
    {
        Task<IEnumerable<ResTagId>> GetCollectTags(ReqCollectId reqCollectId);
        Task<bool> AddTagCollectHandler(ReqAddTagCollectHandler reqAddTagCollectHandler);
        Task<bool> RemoveTagCollectJoin(ReqId reqId);
    }
}
