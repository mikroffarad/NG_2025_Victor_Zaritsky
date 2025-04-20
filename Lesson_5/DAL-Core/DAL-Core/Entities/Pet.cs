using DAL_Core.Enums;

namespace DAL_Core.Entities
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }

        public string Breed { get; set; }

        public PetTypes Type { get; set; }

        public Guid StoreId { get; set; }

        public virtual Store Store { get; set; }

        public virtual ICollection<HealthCare> HealthCares { get; set; }
    }
}
