

namespace user_stuff_share_app.Dtos.Item.Item_Dtos.Get_Item
{
    public class ResGetItem
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CollectId { get; set; }
        public string Title { get; set; }
        public string ItemForm { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
    }
}
