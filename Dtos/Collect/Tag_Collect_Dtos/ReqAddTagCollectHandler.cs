using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Dtos.Collect.Tag_Collect_Dtos
{
    public class ReqAddTagCollectHandler
    {
        public string TagId { get; set; }
        public long CollectId { get; set; }
    }
}
