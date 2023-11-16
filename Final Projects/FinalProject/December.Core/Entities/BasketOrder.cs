using December.Core.Entities.Areas;

namespace December.Core.Entities;


    public class BasketOrder : BaseEntity
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }

