namespace mvcEntities.Entities
{
    public partial class OrderHistory
    {
        public long OrderId { get; set; }
        public long CategoryId { get; set; }
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public string TransactionId { get; set; }

        public virtual Category Category { get; set; }
        public virtual MenuList Item { get; set; }
        public virtual Registration User { get; set; }
    }
}
