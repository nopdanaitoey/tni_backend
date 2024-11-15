using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tni_back.Entities.Common;

namespace tni_back.Entities
{
    public class UserCart : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        [ForeignKey("ProductId")]
        public MasterProducts MasterProducts { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}