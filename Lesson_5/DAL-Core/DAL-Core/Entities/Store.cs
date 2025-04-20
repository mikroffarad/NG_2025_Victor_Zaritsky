namespace DAL_Core.Entities
{
    public class Store : BaseEntity
    {
        public Store()
        {
            Location = string.Concat(City, Address);
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; }

        public string Address { get; set; }

        public string City { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
