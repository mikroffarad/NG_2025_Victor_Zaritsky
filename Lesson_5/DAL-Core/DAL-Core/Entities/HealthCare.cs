namespace DAL_Core.Entities
{
    public class HealthCare : BaseEntity
    {
        public string TreatmentName { get; set; }

        public DateTime InjectedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid PetId { get; set; }

        public Guid VendorId { get; set; }

        public virtual Pet Pet { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
