namespace mvcEntities.Entities
{
    public partial class Payment
    {
        public long PaymentId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long ModeId { get; set; }
        public long MenuId { get; set; }
        public virtual MenuList Menu { get; set; }
    }
}
