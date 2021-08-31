using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ssa_database.Models.Collect_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Req;
using user_stuff_share_app.Dtos.Basic_Req_Res_Dtos.Res;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Collect_Form;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Create_Collect;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Get_Collect;
using user_stuff_share_app.Dtos.Collect.Collect_Dtos.Update_Collect;
using user_stuff_share_app.Repository_Interfaces.ICollect_Repository;

namespace user_stuff_share_app.Repository.Collect_Repository
{
    public class CollectRepository : ICollectRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CollectRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> CollectExists(long id)
        {
            return await _db.Collect.AsNoTracking().AnyAsync(c => c.Id == id);
        }

        public async Task<ResGetCollect> GetCollect(ReqIdUserId reqIdUserId)
        {
            Collect getCollect = await _db.Collect.AsNoTracking().FirstOrDefaultAsync(c =>c.Id == reqIdUserId.Id && c.UserId == reqIdUserId.UserId);
            ResGetCollect res = _mapper.Map<ResGetCollect>(getCollect);
            return res;
        }
    
        public async Task<IEnumerable<ResGetCollect>> GetAllCollects(ReqUserId reqUserId)
        {
            IEnumerable<Collect> getCollects = await _db.Collect.AsNoTracking().Where(c => c.UserId == reqUserId.UserId)
                .OrderBy(c => c.Title).ToListAsync();
            // .SelectMany(c => c.C)
            IEnumerable<ResGetCollect> res = _mapper.Map<IEnumerable<ResGetCollect>>(getCollects);
            return res;
        }
        
        public async Task<ResId> CreateCollect(ReqCreateCollect req)
        {
            Collect newCollect = CreateNewCollect(req);
            _db.Collect.Add(newCollect);
            await Save();
            ResId resCollect= new ResId() { Id = newCollect.Id };

            return resCollect;
        }
        
        public async Task<bool> UpdateCollect(ReqUpdateCollect reqPatch)
        {
            Collect collectQuery = await _db.Collect.AsNoTracking().FirstOrDefaultAsync(c =>  c.UserId == reqPatch.UserId && c.Id == reqPatch.Id);
            collectQuery = MergeObjects(collectQuery, reqPatch);

            _db.Entry(collectQuery).State = EntityState.Modified;
         

            return await Save();
        }

        
        public async Task<bool> DeleteCollect(ReqIdUserId reqIdUserId)
        {
            Collect collectQuery = await _db.Collect.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == reqIdUserId.UserId && c.Id == reqIdUserId.Id);
            _db.Collect.Remove(collectQuery);
            return await Save();
        }
     

       public async Task<bool> UpdateCollectForm(ReqUpdateCF updateForm)
       {
            Collect collectQuery = await _db.Collect.FirstOrDefaultAsync(c => c.Id == updateForm.Id && c.UserId == updateForm.UserId);
             collectQuery.CollectForm = updateForm.CollectForm;
            return await Save();
        }


        private async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false; 
        }
       
        private Collect CreateNewCollect(ReqCreateCollect req)
        {
            DateTimeOffset time = DateTimeOffset.UtcNow;
            Collect createCollect = _mapper.Map<Collect>(req);
            createCollect.Created = time;
            createCollect.Updated = time;
            createCollect.Status = "none";
            createCollect.ImagePath = "/stock/ssa.jpg";
            return createCollect;
        }

        private Collect MergeObjects(Collect collectQuery, ReqUpdateCollect reqPatch)
        {
            string patchJson = JsonConvert.SerializeObject(reqPatch, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            string collectJson = JsonConvert.SerializeObject(collectQuery, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            JObject collectPatch = JObject.Parse(collectJson);
            JObject parsePatch = JObject.Parse(patchJson);
            collectPatch.Merge(parsePatch, new JsonMergeSettings
            {
                // union array values together to avoid duplicates
                MergeArrayHandling = MergeArrayHandling.Union
            });

            Collect mergedCollect = collectPatch.ToObject<Collect>();
            
            mergedCollect.Updated = DateTimeOffset.UtcNow;
            mergedCollect.Created = collectQuery.Created;
            return mergedCollect;
        }
    }
}


