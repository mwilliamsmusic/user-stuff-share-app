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
using user_stuff_share_app.Dtos.Item.Item_Dtos.Create_Item;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Get_Item;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Item_Form;
using user_stuff_share_app.Dtos.Item.Item_Dtos.Update_Item;
using user_stuff_share_app.Repository_Interfaces.ICollect_Repository;

namespace user_stuff_share_app.Repository.Collect_Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ItemRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }



        public async Task<ResGetItem> GetItem(ReqId reqId)
        {
            Item getItem = await _db.Item.AsNoTracking().FirstOrDefaultAsync(c => c.Id == reqId.Id);
            ResGetItem res = _mapper.Map<ResGetItem>(getItem);
            return res;
        }
    
        public async Task<IEnumerable<ResGetItem>> GetAllItems(ReqCollectId reqCollectId)
        {
            IEnumerable<Item> getItems = await _db.Item.AsNoTracking().Where(c => c.CollectId == reqCollectId.CollectId)
                .OrderBy(c => c.Created)
                .ToListAsync();
            IEnumerable<ResGetItem> res = _mapper.Map<IEnumerable<ResGetItem>>(getItems);
            return res;
        }

        public async Task<ResId> CreateItem(ReqCreateItem reqCreateItem)
        {
            Item newItem = CreateNewItem(reqCreateItem);
            _db.Item.Add(newItem);
            await Save();
            ResId resItem = new ResId()
            {
                Id = newItem.Id
            };
            return resItem;
        }

        public async Task<bool> UpdateItemCollect(ReqUpdateItem reqUpdate)
        {

            Item itemQuery = await _db.Item.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == reqUpdate.UserId && c.Id == reqUpdate.Id);
            itemQuery = MergeObjects(itemQuery, reqUpdate);

            _db.Entry(itemQuery).State = EntityState.Modified;

            return await Save();
        }

        public async Task<bool> DeleteItemCollect(ReqIdUserId reqIdUserId)
        {
            Item deleteItem = await  _db.Item.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == reqIdUserId.UserId && c.Id == reqIdUserId.Id);
            _db.Item.Remove(deleteItem);
            return await Save();
        }

   

        public async Task<bool> UpdateItemForm(ReqUpdateIF reqUpdateIF)
        {
            Item itemQuery = await _db.Item.FirstOrDefaultAsync(c => c.Id == reqUpdateIF.Id);
            itemQuery.ItemForm = reqUpdateIF.ItemForm;
            return await Save();
        }


             public async Task<bool> Save()
        {
            return await  _db.SaveChangesAsync() >= 0 ? true : false; ;
        }

        private Item CreateNewItem(ReqCreateItem req)
        {
            DateTimeOffset time = DateTimeOffset.UtcNow;
            Item createItem = _mapper.Map<Item>(req);
            createItem.Status = "none";
            createItem.ImagePath = "/stock/ssa.jpg";
            createItem.Created = time;
            createItem.Updated = time;

            return createItem;
        }


        private Item MergeObjects(Item itemQuery, ReqUpdateItem reqUpdate)
        {
            string itemJson = JsonConvert.SerializeObject(itemQuery, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            string patchJson = JsonConvert.SerializeObject(reqUpdate, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
          
            JObject itemPatch = JObject.Parse(itemJson);
            JObject parsePatch = JObject.Parse(patchJson);
            itemPatch.Merge(parsePatch, new JsonMergeSettings
            {
                // union array values together to avoid duplicates
                MergeArrayHandling = MergeArrayHandling.Union
            });

            Item mergedItem = itemPatch.ToObject<Item>();

            mergedItem.Updated = DateTimeOffset.UtcNow;
            mergedItem.Created = itemQuery.Created;
            return mergedItem;
        }

        private List<Form> ReturnDeleted(IList<Form> collectForm, string deleteField)
        {
            List<Form> updatedList = new List<Form>();
            if (collectForm.Where(d => d.Field != deleteField).ToList() != null)
            {
                updatedList = collectForm.Where(d => d.Field != deleteField).ToList();
                return updatedList;
            }
            else
            {
                updatedList.Add(new Form() { Field = "", Value = "" });
                return updatedList;
            }
        }


    }
}
