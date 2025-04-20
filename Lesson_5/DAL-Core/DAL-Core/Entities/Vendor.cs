using DAL_Core.Enums;

namespace DAL_Core.Entities
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? SignedAt { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public ContractType ContractType { get; set; }

        public ICollection<HealthCare> HealthCares { get; set; }
    }
}
