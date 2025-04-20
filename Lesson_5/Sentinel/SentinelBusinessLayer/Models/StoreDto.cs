namespace SentinelBusinessLayer.Models
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public int PetCount { get; set; }
    }
}